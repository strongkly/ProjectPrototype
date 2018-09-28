using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyBox.Tools
{
    public class GameObjectPool : ObjectPool<GameObject>
    {
        public GameObject Prefab
        {
            get;
            private set;
        }

        public GameObjectPool(int size, GameObject prefab) : base(size)
        {
            Prefab = prefab;
            Initialize();
        }

        protected override GameObject Create()
        {
            return GameObject.Instantiate(Prefab);
        }

        protected override void Initialize()
        {
            if (Prefab == null)
                return;
            else
                base.Initialize();
        }
    }
}
