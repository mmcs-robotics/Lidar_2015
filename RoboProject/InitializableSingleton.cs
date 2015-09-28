using System;

namespace RoboProject
{
    public static class InitializableSingleton<T> where T : class
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException(string.Format("Initialize the singleton instance with InitializableSingleton<{0}>.Init() before use", typeof(T).Name));
                }

                return _instance;
            }
        }

        public static void Init(T instance)
        {
            if (instance == null) throw new ArgumentNullException("instance");
            if (_instance != null)
            {
                throw new InvalidOperationException("Already initialized");
            }

            _instance = instance;
        }
    }
}