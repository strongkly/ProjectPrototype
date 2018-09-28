using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyBox.Tools
{

    public class ScriptSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(T).Name.ToString();
                    GameObject.DontDestroyOnLoad(go);
                    instance = go.AddComponent<T>();
                }
                return instance;
            }
        }
    }
}
