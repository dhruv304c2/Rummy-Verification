using System;
using UnityEngine;

namespace Game
{
    public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
    {
        private static T _instance = null;

        public static T Instance
        {
            get
            {
                if (FindObjectOfType<T>() == null && _instance == null)
                {
                    _instance = new GameObject(typeof(T).Name).AddComponent<T>();
                }
                else if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                }
                
                return _instance;
            }
        }

        private void Awake()
        {
            if (FindObjectsOfType<T>().Length > 1)
            {
                Destroy(gameObject);
            }
            
            DontDestroyOnLoad(this);
        }
    }
}