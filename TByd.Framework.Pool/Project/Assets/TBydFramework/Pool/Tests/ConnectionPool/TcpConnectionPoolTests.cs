using NUnit.Framework;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using TBydFramework.Pool.Runtime.Core;

namespace TBydFramework.Pool.Tests
{
    public class TcpConnectionPoolTests
    {
        private TcpListener _server;
        private TcpConnectionPool _pool;
        private const int Port = 12345;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _server = new TcpListener(IPAddress.Loopback, Port);
            _server.Start();
            
            // 启动接受连接的后台任务
            Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        var client = await _server.AcceptTcpClientAsync();
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
            _server?.Stop();
        }

        [SetUp]
        public void Setup()
        {
            _pool = new TcpConnectionPool(
                "localhost",
                Port,
                maxSize: 5,
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
        public void Acquire_ShouldReturnConnectedClient()
        {
            var client = _pool.Acquire();
            Assert.NotNull(client);
            Assert.IsTrue(client.Connected);
            _pool.Release(client);
        }

        [Test]
        public void Release_ShouldStoreConnection()
        {
            var client = _pool.Acquire();
            _pool.Release(client);
            Assert.AreEqual(1, _pool.Count);
        }

        [Test]
        public void IdleTimeout_ShouldCloseConnection()
        {
            var pool = new TcpConnectionPool(
                "localhost",
                Port,
                maxSize: 5,
                idleTimeout: TimeSpan.FromMilliseconds(100)
            );

            var client = pool.Acquire();
            pool.Release(client);
            
            System.Threading.Thread.Sleep(200);
            Assert.AreEqual(0, pool.Count);
            
            pool.Dispose();
        }

        [Test]
        public void ConnectionTimeout_ShouldThrowException()
        {
            var pool = new TcpConnectionPool(
                "invalid-host",
                12346,
                connectionTimeout: TimeSpan.FromMilliseconds(100)
            );

            Assert.Throws<SocketException>(() => pool.Acquire());
            pool.Dispose();
        }
    }
}