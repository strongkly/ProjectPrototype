using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyBox.Components;
using UnityEngine.UI;

public class TestReusableItemList : MonoBehaviour
{
    public RectTransform ItemListParent;

    List<string> values = new List<string>() { "1", "2", "3" };

    ReusableItemList<TestReusableItem, string> itemList;

    private void Start()
    {
        itemList = new ReusableItemList<TestReusableItem, string>(ItemListParent);
    }

    private void OnGUI()
    {
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
}
