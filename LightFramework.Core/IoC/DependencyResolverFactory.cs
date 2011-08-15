using System;

namespace LightFramework.Core
{
    public class DependencyResolverFactory : IDependencyResolverFactory
    {
        private readonly Type _resolverType;

        public DependencyResolverFactory(string appSettingKey)
        {
            Check.Argument.IsNotEmpty(appSettingKey, "appSettingKey");

            string resolverTypeName = new ConfigurationManagerWrapper().AppSettings[appSettingKey];
            _resolverType = Type.GetType(resolverTypeName, true, true);
        }

        public DependencyResolverFactory() : this("dependencyResolverTypeName")
        {
        }

        public IDependencyResolver CreateInstance()
        {
            return Activator.CreateInstance(_resolverType) as IDependencyResolver;
        }
    }
}