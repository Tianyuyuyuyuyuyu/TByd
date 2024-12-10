using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using TBydFramework.Pool.Runtime.Core;

namespace TBydFramework.Pool.Tests
{
    public class ConnectionPoolStressTests
    {
        private TcpListener _server;
        private TcpConnectionPool _pool;
        private const int Port = 12346;
        private List<TcpClient> _activeConnections;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _server = new TcpListener(IPAddress.Loopback, Port);
            _server.Start();
            _activeConnections = new List<TcpClient>();

            Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        await _server.AcceptTcpClientAsync();
                    }
                    catch
                    {
                        break;
                    }
                }
            });
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            foreach (var conn in _activeConnections)
            {
                conn.Dispose();
            }
            _server?.Stop();
        }

        [SetUp]
        public void Setup()
        {
            _pool = new TcpConnectionPool(
                "localhost",
                Port,
                maxSize: 20,
                connectionTimeout: TimeSpan.FromSeconds(1),
                idleTimeout: TimeSpan.FromSeconds(5)
            );
        }

        [TearDown]
        public void Teardown()
        {
            _pool?.Dispose();
        }

        [Test]
        public async Task MultipleThreads_ShouldHandleConcurrentConnections()
        {
            const int threadCount = 5;
            const int connectionsPerThread = 10;

            var tasks = new Task[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    for (int j = 0; j < connectionsPerThread; j++)
                    {
                        var client = _pool.Acquire();
                        _activeConnections.Add(client);
                        
                        if (j % 2 == 0)
                        {
                            _pool.Release(client);
                            _activeConnections.Remove(client);
                        }
                    }
                });
            }

            await Task.WhenAll(tasks);
            Assert.LessOrEqual(_pool.Count, threadCount * connectionsPerThread / 2);
        }

        [Test]
        public void RapidAcquireRelease_ShouldMaintainPoolIntegrity()
        {
            const int iterations = 100;
            _pool.Prewarm(5);

            for (int i = 0; i < iterations; i++)
            {
                var client = _pool.Acquire();
                _activeConnections.Add(client);

                if (_activeConnections.Count > 10)
                {
                    var connToRelease = _activeConnections[0];
                    _pool.Release(connToRelease);
                    _activeConnections.RemoveAt(0);
                }
            }

            foreach (var conn in _activeConnections.ToArray())
            {
                _pool.Release(conn);
                _activeConnections.Remove(conn);
            }

            Assert.Greater(_pool.Count, 0);
            Assert.LessOrEqual(_pool.Count, 20);
        }
    }
} 