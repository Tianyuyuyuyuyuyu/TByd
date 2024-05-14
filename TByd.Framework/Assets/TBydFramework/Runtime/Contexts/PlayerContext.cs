﻿using IServiceContainer = TBydFramework.Runtime.Services.IServiceContainer;

namespace TBydFramework.Runtime.Contexts
{
    public class PlayerContext : Context
    {
        private string username;
        public string Username { get { return this.username; } }
        public PlayerContext(string username) : this(username, null)
        {
            this.username = username;
        }

        public PlayerContext(string username, IServiceContainer container) : base(container, GetApplicationContext())
        {
            this.username = username;
        }
    }
}
