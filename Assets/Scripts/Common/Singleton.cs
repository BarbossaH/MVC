

using System;

namespace Common
{
    public class Singleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                return _instance ??= (T)Activator.CreateInstance(typeof(T));
            }
        }
        
        public virtual void Init(){}
        public virtual void Update(float dt){}
        public virtual void OnDestroy(){}
    }
}
