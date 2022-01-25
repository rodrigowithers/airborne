using System;

namespace deJex
{
    public class Binding
    {
        private Type _generic;
        private object _concrete;
        
        private event Action<Type, object> _bindingAction;

        public Binding(Type generic, Action<Type, object> action)
        {
            _bindingAction = action;
            _generic = generic;
        }

        public void To(object concrete)
        {
            _concrete = concrete;
            
            _bindingAction.Invoke(_generic, _concrete);
        }
    }
}