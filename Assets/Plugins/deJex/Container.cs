using System;
using UnityEngine;
using System.Collections.Generic;

namespace deJex
{
    [DefaultExecutionOrder(Int32.MinValue)]
    public static class Container
    {
        private static readonly Dictionary<Type, object> _contracts = new Dictionary<Type, object>();

        public static Binding Bind<TGeneric>()
        {
            return new Binding(typeof(TGeneric), _contracts.Add);
        }
        
        public static void BindSingleton<TGeneric>(object singleton)
        {
            var interfaces = singleton.GetType().GetInterfaces();

            if (interfaces.Length <= 0)
                throw new Exception("Singleton doesn't implement an interface.");

            _contracts.Add(typeof(TGeneric), singleton);
        }
        
        public static void BindSingleton(object singleton)
        {
            var interfaces = singleton.GetType().GetInterfaces();

            if (interfaces.Length <= 0)
                throw new Exception("Singleton doesn't implement an interface.");
            
            if (interfaces.Length > 1)
                throw new Exception("Trying to bind singleton that implements multiple interfaces.\n" +
                                    "Use BindSingleton<TGeneric>() instead.");
            
            var interfaceType = singleton.GetType().GetInterfaces()[0];
            _contracts.Add(interfaceType, singleton);
        }

        public static T Resolve<T>()
        {
            try
            {
                return (T) _contracts[typeof(T)];
            }
            catch (Exception e)
            {
                throw new NotImplementedException($"Can't resolve contract for {typeof(T)}");
            }
        }
    }
}