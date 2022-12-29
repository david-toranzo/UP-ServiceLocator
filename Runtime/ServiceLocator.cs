using System;
using System.Collections.Generic;
using Patterns.Singleton;

namespace Patterns.ServiceLocator
{
    public class ServiceLocator : Singleton<ServiceLocator> 
    {
        private readonly Dictionary<Type, object> _servicesObject = new Dictionary<Type, object>();

        protected override void AwakeAfterInitializeSingleton()
        {
            DontDestroyWhenLoadScene();
        }

        public void Register<ServiceType>(ServiceType service)
        {
            Type type = typeof(ServiceType);
            _servicesObject.Add(type, service);
        }

        public void Unregister<ServiceType>()
        {
            Type type = typeof(ServiceType);
            ThrowExceptionIfServiceDoesntExist<ServiceType>(type);

            _servicesObject.Remove(type);
        }

        public ServiceType GetService<ServiceType>()
        {
            Type type = typeof(ServiceType);
            ThrowExceptionIfServiceDoesntExist<ServiceType>(type);

            return (ServiceType)_servicesObject[type];
        }

        private void ThrowExceptionIfServiceDoesntExist<ServiceType>(Type type)
        {
            if (!IsServiceTypeExist<ServiceType>())
                throw new Exception($"The service {type.Name} is not in the service locator.");
        }

        public bool IsServiceTypeExist<ServiceType>()
        {
            Type type = typeof(ServiceType);
            return _servicesObject.ContainsKey(type);
        }
    }
}