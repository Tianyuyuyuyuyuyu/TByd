﻿using TBydFramework.Runtime.Binding.Contexts;
using TBydFramework.Runtime.Binding.Proxy.Sources;
using TBydFramework.Runtime.Binding.Proxy.Targets;

namespace TBydFramework.Runtime.Binding
{

    public class BindingFactory : IBindingFactory
    {
        private ISourceProxyFactory sourceProxyFactory;
        private ITargetProxyFactory targetProxyFactory;

        public ISourceProxyFactory SourceProxyFactory
        {
            get { return this.sourceProxyFactory; }
            set { this.sourceProxyFactory = value; }
        }
        public ITargetProxyFactory TargetProxyFactory
        {
            get { return this.targetProxyFactory; }
            set { this.targetProxyFactory = value; }
        }

        public BindingFactory(ISourceProxyFactory sourceProxyFactory, ITargetProxyFactory targetProxyFactory)
        {
            this.sourceProxyFactory = sourceProxyFactory;
            this.targetProxyFactory = targetProxyFactory;
        }

        public IBinding Create(IBindingContext bindingContext, object source, object target, BindingDescription bindingDescription)
        {
            return new Binding(bindingContext, source, target, bindingDescription, this.sourceProxyFactory, this.targetProxyFactory);
        }
    }
}
