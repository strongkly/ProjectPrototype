using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyBox.Components;
using UnityEngine.UI;

public class TestReusableItemList : MonoBehaviour
{
    public RectTransform ItemListParent;
    public RectTransform mItemListParent;
    public RectTransform pItemListParent;

    List<string> values = new List<string>() { "1", "2", "3" };
    List<TestMultipleGroupedData> mValues = new List<TestMultipleGroupedData>() {
        new TestMultipleGroupedData(0,"1"),
        new TestMultipleGroupedData(1,"2"),
        new TestMultipleGroupedData(1,"3")};

    ReusableItemList<TestReusableItem, string> itemList;
    MultipleStyleReusableItemList<TestMultipleGroupedData> mItemList;
    ParasiteReusableItemList<TestParasiteItem, string, TestReusableItemList> pItemList;

    public void TestParasite(int dataIdx, string value)
    {
        Debug.LogErrorFormat("value of dataIdx:{0} is:{1}", dataIdx, value);
    }

    private void Start()
    {
        itemList = new ReusableItemList<TestReusableItem, string>(ItemListParent);
        mItemList = MultipleStyleReusableItemList<TestMultipleGroupedData>.
            Create<TestMultipleReusableItem1, TestMultipleReusableItem2>(mItemListParent);
        pItemList = new ParasiteReusableItemList<TestParasiteItem, string, TestReusableItemList>
            (this, pItemListParent);
    }

    private void OnGUI()
    {
        #region ResuableItemList
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
        #endregion

        #region multiple list
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
        #endregion

        #region multiple list
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("pClear"))
        {
            pItemList.ReuseAll();
        }
        if (GUILayout.Button("pShow"))
        {
            pItemList.RefreshAll(values);
        }
        if (GUILayout.Button("pAdd"))
        {
            values.Add("123");
            pItemList.RefreshAll(values);
        }
        if (GUILayout.Button("pRefresh item4"))
        {
            if (!pItemList.TryRefreshByIdx(3))
            {
                Debug.LogError("items less than 4");
            }
        }
        GUILayout.EndHorizontal();
        #endregion

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

    public class TestParasiteItem : ParasiteReusableItem<string, TestReusableItemList>
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
            if (host != null)
                host.TestParasite(dataIndex, data);
        }

        public override void Refresh()
        {
            base.Refresh();
            Debug.LogError("refreshing");
        }
    }
}
