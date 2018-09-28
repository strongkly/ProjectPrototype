using System;
using UnityEngine;
using System.Collections.Generic;

namespace CrazyBox.Tools
{
    public class DynamicGameObjectPool : DynamicObjectPool<GameObject>
    {
        public GameObject Prefab
        {
            get; private set;
        }

        protected override GameObject Create()
        {
            GameObject go;
            go = GameObject.Instantiate(Prefab);
            InitAction(go);
            return go;
        }

        protected override void Dispose(GameObject disposedObject)
        {
            GameObject.Destroy(disposedObject);
            base.Dispose(disposedObject);
        }

        protected override void InitPool()
        {
            if (Prefab == null)
                return;
            else
                base.InitPool();
        }

        public DynamicGameObjectPool(GameObject Prefab, int initSize, float checkInterval,
            Action<GameObject> InitAction, Action<GameObject> ResetAction)
            : base(initSize, checkInterval, InitAction, ResetAction)
        {
            this.Prefab = Prefab;
            InitPool();
        }
    }
}
