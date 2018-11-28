using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyBox.Components;
using UnityEngine.UI;

public class TestReusableItemList : MonoBehaviour
{
    public RectTransform ItemListParent, mItemListParent;

    List<string> values = new List<string>() { "1", "2", "3" };
    List<TestMultipleGroupedData> mValues = new List<TestMultipleGroupedData>() {
        new TestMultipleGroupedData(0,"1"),
        new TestMultipleGroupedData(1,"2"),
        new TestMultipleGroupedData(1,"3")};

    ReusableItemList<TestReusableItem, string> itemList;

    MultipleStyleReusableItemList<TestMultipleGroupedData> mItemList;

    private void Start()
    {
        itemList = new ReusableItemList<TestReusableItem, string>(ItemListParent);
        mItemList = MultipleStyleReusableItemList<TestMultipleGroupedData>.
            Create<TestMultipleReusableItem1, TestMultipleReusableItem2>(mItemListParent);
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Clear"))
        {
            itemList.ReuseAll();
        }
        if (GUILayout.Button("Show"))
        {
            itemList.RefreshAll(values);
        }
        if (GUILayout.Button("Add"))
        {
            values.Add("4");
            itemList.RefreshAll(values);
        }
        if (GUILayout.Button("Refresh item4"))
        {
            if (!itemList.TryRefreshByIdx(3))
            {
                Debug.LogError("items less than 4");
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("mClear"))
        {
            mItemList.ReuseAll();
        }
        if (GUILayout.Button("mShow"))
        {
            mItemList.RefreshAll(mValues);
        }
        if (GUILayout.Button("mAdd"))
        {
            mValues.Add(new TestMultipleGroupedData(0, "123"));
            mItemList.RefreshAll(mValues);
        }
        if (GUILayout.Button("mRefresh item4"))
        {
            if (!mItemList.TryRefreshByIdx(3))
            {
                Debug.LogError("items less than 4");
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }

    public class TestReusableItem : ReusableItem<string>
    {
        Text text;

        private void Awake()
        {
            text = transform.Find("Text").GetComponent<Text>();
        }

        public override void Refresh(string data, int dataIndex)
        {
            base.Refresh(data, dataIndex);
            text.text = data;
        }

        public override void Refresh()
        {
            base.Refresh();
            Debug.LogError("refreshing");
        }
    }

    public class TestMultipleReusableItem1 : GroupedReusableItem<TestMultipleGroupedData>
    {
        Text text;

        private void Awake()
        {
            text = transform.Find("Text").GetComponent<Text>();
        }

        public override void Refresh(TestMultipleGroupedData data, int dataIndex)
        {
            base.Refresh(data, dataIndex);
            text.text = data.text;
        }

        public override void Refresh()
        {
            base.Refresh();
            Debug.LogError("refreshing");
        }
    }
    public class TestMultipleReusableItem2 : GroupedReusableItem<TestMultipleGroupedData>
    {
        Text text;

        private void Awake()
        {
            text = transform.Find("Text").GetComponent<Text>();
        }

        public override void Refresh(TestMultipleGroupedData data, int dataIndex)
        {
            base.Refresh(data, dataIndex);
            text.text = data.text;
        }

        public override void Refresh()
        {
            base.Refresh();
            Debug.LogError("refreshing");
        }
    }

    public class TestMultipleGroupedData : IGroupedData
    {
        public int idx;
        public string text;
        public TestMultipleGroupedData(int idx, string text)
        {
            this.idx = idx;
            this.text = text;
        }
        public int GetGroupId()
        {
            return idx;
        }
    }
}
