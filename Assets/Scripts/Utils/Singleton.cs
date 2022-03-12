using System;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public abstract class Singleton<T> where T : class, new()
    {
        private static T _instance;

        //non private constructor
        public Singleton()
        {
            //Anti-pattern without factory
        }

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new T();

                return _instance;
            }
        }

    }
}