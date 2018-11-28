using System.Collections.Generic;
using UnityEngine;

namespace CrazyBox.Components
{
    public class ReusableItemList<TItemView, TItemData>
        where TItemView : ReusableItem<TItemData>
    {
        Queue<TItemView> reusableItems;
        GameObject templateObject;
        Transform parent;

        public ReusableItemList(Transform childrenRoot, GameObject templateObject = null)
        {
            reusableItems = new Queue<TItemView>();
            this.parent = childrenRoot;
            if (templateObject == null)
                templateObject = parent.GetChild(0).gameObject;
            templateObject.AddComponent<TItemView>();
            this.templateObject = templateObject;
            ReuseAll();
        }

        public void RefreshAll(IList<TItemData> value)
        {
            ReuseAll();
            for (int i = 0; i < value.Count; i++)
            {
                TItemView view = GetItem();
                view.transform.SetSiblingIndex(i);
                view.Refresh(value[i], i);
            }
        }

        public void RefreshAll()
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                parent.GetChild(i).GetComponent<TItemView>().Refresh();
            }
        }

        public void ReuseAll()
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                ReuseItem(parent.GetChild(i).GetComponent<TItemView>());
            }
        }

        public bool TryRefreshByIdx(int idx)
        {
            bool result = true;
            if (IsIndexOutOfBound(idx))
            {
                result = false;
            }
            else
            {
                parent.GetChild(idx).GetComponent<TItemView>().Refresh();
            }
            return result;
        }

        protected bool IsIndexOutOfBound(int idx)
        {
            bool result = false;
            if (idx < 0 || idx >= parent.childCount)
            {
                result = true;
            }
            return result;
        }

        protected TItemView GetItem()
        {
            TItemView result;
            if (!TryGetItem(out result))
            {
                result = AddNewItem();
            }
            return result;
        }

        protected bool TryGetItem(out TItemView item)
        {
            bool result = false;
            item = null;
            if (reusableItems.Count > 0)
            {
                item = reusableItems.Dequeue();
                item.gameObject.SetActive(true);
                result = true;
            }
            return result;
        }

        protected TItemView AddNewItem()
        {
            TItemView result;
            GameObject obj = GameObject.Instantiate<GameObject>(templateObject, parent);
            obj.gameObject.SetActive(true);
            result = obj.GetComponent<TItemView>();

            return result;
        }

        protected void ReuseItem(TItemView item)
        {
            if (!reusableItems.Contains(item))
            {
                reusableItems.Enqueue(item);
                item.gameObject.SetActive(false);
            }
        }
    }

    public class ReusableItem<TItemData> : MonoBehaviour
    {
        protected TItemData data;

        public int dataIndex
        {
            get;
            private set;
        }

        public virtual void Refresh(TItemData data, int dataIndex)
        {
            this.data = data;
            this.dataIndex = dataIndex;
        }

        public virtual void Refresh()
        {
            Refresh(this.data, this.dataIndex);
        }
    }
}
