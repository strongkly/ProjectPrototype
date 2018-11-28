using System;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyBox.Components
{
    public class MultipleStyleReusableItemList<TItemData>
        where TItemData : IGroupedData
    {
        Dictionary<int, Queue<GroupedReusableItem<TItemData>>> GroupedReusableItems;
        List<Type> viewTypes;
        List<GameObject> templateObjects;
        Transform parent;

        public static MultipleStyleReusableItemList<TItemData> Create<ItemView1, ItemView2>(
            Transform childrenRoot, IList<GameObject> templateObject = null)
            where ItemView1 : GroupedReusableItem<TItemData>
            where ItemView2 : GroupedReusableItem<TItemData>
        {
            MultipleStyleReusableItemList<TItemData> result = new MultipleStyleReusableItemList<TItemData>(
                childrenRoot, new List<Type> { typeof(ItemView1), typeof(ItemView2) }, templateObject);

            return result;
        }

        MultipleStyleReusableItemList(Transform childrenRoot,
            IList<Type> viewTypes, IList<GameObject> templateObjects = null)
        {
            this.parent = childrenRoot;
            if (templateObjects == null)
                InitTemplateObject();
            else
                this.templateObjects = templateObjects as List<GameObject>;
            foreach (GameObject go in this.templateObjects)
            {
                go.SetActive(false);
            }
            this.viewTypes = viewTypes as List<Type>;
            GroupedReusableItems = new Dictionary<int, Queue<GroupedReusableItem<TItemData>>>();
            for (int i = 0; i < viewTypes.Count; i++)
            {
                GroupedReusableItems.Add(i, new Queue<GroupedReusableItem<TItemData>>());
            }
        }

        protected void InitTemplateObject()
        {
            templateObjects = new List<GameObject>();
            for (int i = 0; i < parent.childCount; i++)
            {
                templateObjects.Add(parent.GetChild(i).gameObject);
                templateObjects[i].SetActive(false);
            }
        }

        public void RefreshAll(IList<TItemData> value)
        {
            ReuseAll();
            for (int i = 0; i < value.Count; i++)
            {
                GroupedReusableItem<TItemData> view = GetItem(value[i].GetGroupId());
                view.transform.SetSiblingIndex(i);
                view.Refresh(value[i], i);
            }
        }

        public void RefreshAll()
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                parent.GetChild(i).GetComponent<GroupedReusableItem<TItemData>>().Refresh();
            }
        }

        public void ReuseAll()
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                GroupedReusableItem<TItemData> view = parent.GetChild(i).
                    GetComponent<GroupedReusableItem<TItemData>>();
                ReuseItem(view);
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
                parent.GetChild(idx).GetComponent<GroupedReusableItem<TItemData>>().Refresh();
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

        protected GroupedReusableItem<TItemData> GetItem(int typeIdx)
        {
            GroupedReusableItem<TItemData> result;
            if (!TryGetItem(out result, typeIdx))
            {
                result = AddNewItem(typeIdx);
            }
            return result;
        }

        protected bool TryGetItem(out GroupedReusableItem<TItemData> item, int typeIdx)
        {
            bool result = false;
            item = null;
            if (GroupedReusableItems.Count > 0)
            {
                if (GroupedReusableItems[typeIdx].Count > 0)
                {
                    item = GroupedReusableItems[typeIdx].Dequeue();
                    item.gameObject.SetActive(true);
                    result = true;
                }
            }
            return result;
        }

        protected GroupedReusableItem<TItemData> AddNewItem(int typeIdx)
        {
            GroupedReusableItem<TItemData> result;
            GameObject obj = GameObject.Instantiate<GameObject>(templateObjects[typeIdx], parent);
            obj.gameObject.SetActive(true);
            result = (GroupedReusableItem<TItemData>)obj.AddComponent(viewTypes[typeIdx]);

            return result;
        }

        protected void ReuseItem(GroupedReusableItem<TItemData> item)
        {
            if (item != null)
            {
                if (!GroupedReusableItems[item.GetGroupId()].Contains(item))
                {
                    GroupedReusableItems[item.GetGroupId()].Enqueue(item);
                    item.gameObject.SetActive(false);
                }
            }
        }
    }

    public class GroupedReusableItem<TItemData> : ReusableItem<TItemData>, IGroupedData
        where TItemData : IGroupedData
    {
        public int GetGroupId()
        {
            return (data as IGroupedData).GetGroupId();
        }
    }

    public interface IGroupedData
    {
        int GetGroupId();
    }
}