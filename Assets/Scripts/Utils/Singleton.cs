using System;
using System.Reflection;

namespace Assets.Scripts.Utils
{
    public abstract class Singleton<T> where T : Singleton<T>
    {
        private static readonly Lazy<T> _instance;

        static Singleton()
        {
            _instance = new Lazy<T>(() =>
            {
                try
                {
                    var constructor = typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
                    return (T)constructor.Invoke(null);
                }
                catch (Exception exception)
                {
                    throw new Exception(" === Singleton === ", exception);
                }
            });
        }


        public static T Instance
        {
            get
            {
                return _instance.Value;
            }
        }
    }
}