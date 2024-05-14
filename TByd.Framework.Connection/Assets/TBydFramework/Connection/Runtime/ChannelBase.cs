﻿using System;
using System.IO;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using TBydFramework.Connection.Runtime.Codec;
using BinaryReader = TBydFramework.Connection.Runtime.IO.BinaryReader;
using BinaryWriter = TBydFramework.Connection.Runtime.IO.BinaryWriter;

namespace TBydFramework.Connection.Runtime
{
    public abstract class ChannelBase : IChannel<IMessage>
    {
        public const int RECEIVE_BUFFER_SIZE = 8192;
        public const int SEND_BUFFER_SIZE = 8192;

        protected bool connected;
        protected BinaryReader reader;
        protected BinaryWriter writer;

        protected ICodecFactory<IMessage> codecFactory;
        protected IMessageDecoder<IMessage> decoder;
        protected IMessageEncoder<IMessage> encoder;

        protected string serverName;
        protected X509Certificate cert;
        protected bool secure;
        protected RemoteCertificateValidationCallback remoteCertificateValidationCallback;
        [Obsolete("The handshake handler moved to Connector")]
        protected IHandshakeHandler handshakeHandler;

        [Obsolete("Please move the handshake handler to the DefaultConnector.")]
        public ChannelBase(ICodecFactory<IMessage> codecFactory, IHandshakeHandler handshakeHandler)
        {
            this.codecFactory = codecFactory ?? throw new ArgumentNullException("codecFactory");
            this.handshakeHandler = handshakeHandler;
        }

        [Obsolete("Please move the handshake handler to the DefaultConnector.")]
        public ChannelBase(IMessageDecoder<IMessage> decoder, IMessageEncoder<IMessage> encoder, IHandshakeHandler handshakeHandler)
        {
            this.decoder = decoder ?? throw new ArgumentNullException("decoder");
            this.encoder = encoder ?? throw new ArgumentNullException("encoder");
            this.handshakeHandler = handshakeHandler;

            this.ReceiveBufferSize = RECEIVE_BUFFER_SIZE;
            this.SendBufferSize = SEND_BUFFER_SIZE;
            this.NoDelay = true;
            this.IsBigEndian = true;
        }

        public ChannelBase(ICodecFactory<IMessage> codecFactory)
        {
            this.codecFactory = codecFactory ?? throw new ArgumentNullException("codecFactory");
        }

        public ChannelBase(IMessageDecoder<IMessage> decoder, IMessageEncoder<IMessage> encoder)
        {
            this.decoder = decoder ?? throw new ArgumentNullException("decoder");
            this.encoder = encoder ?? throw new ArgumentNullException("encoder");

            this.ReceiveBufferSize = RECEIVE_BUFFER_SIZE;
            this.SendBufferSize = SEND_BUFFER_SIZE;
            this.NoDelay = true;
            this.IsBigEndian = true;
        }

        public bool IsBigEndian { get; set; }

        public bool NoDelay { get; set; }

        public int ReceiveBufferSize { get; set; }

        public int SendBufferSize { get; set; }

        public virtual bool Connected { get { return this.connected; } }

        public void Secure(bool secure, string serverName, X509Certificate cert = null)
        {
            Secure(secure, serverName, cert, null);
        }

        public void Secure(bool secure, string serverName, X509Certificate cert, RemoteCertificateValidationCallback remoteCertificateValidationCallback)
        {
            this.secure = secure;
            this.serverName = serverName;
            this.cert = cert;
            this.remoteCertificateValidationCallback = remoteCertificateValidationCallback;
        }

        protected virtual async Task<Stream> WrapStream(Stream stream)
        {
            if (!secure)
                return stream;

            X509CertificateCollection certs = null;
            if (cert != null)
            {
                certs = new X509CertificateCollection();
                certs.Add(cert);
            }

            SslStream sslStream = new SslStream(stream, false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null);
            try
            {
                await sslStream.AuthenticateAsClientAsync(this.serverName, certs, SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12, false);
            }
            catch (Exception)
            {
                sslStream.Close();
                throw;
            }
            return sslStream;
        }

        protected virtual bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (remoteCertificateValidationCallback != null)
                return remoteCertificateValidationCallback(sender, certificate, chain, sslPolicyErrors);

            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            return false;
        }

        public Task Connect(string hostname, int port, int timeoutMilliseconds)
        {
            return Connect(hostname, port, timeoutMilliseconds, CancellationToken.None);
        }

        public abstract Task Connect(string hostname, int port, int timeoutMilliseconds, CancellationToken cancellationToken);

        public virtual async Task<IMessage> ReadAsync()
        {
            if (reader == null)
                throw new IOException("The channel is not connected.");

            return await decoder.Decode(reader).ConfigureAwait(false);
        }

        public virtual async Task WriteAsync(IMessage message)
        {
            if (writer == null)
                throw new IOException("The channel is not connected.");

            await encoder.Encode(message, writer).ConfigureAwait(false);
        }

        public abstract Task Close();
    }
}
