using UnityEngine;
using CrazyBox.Tools;

public class TestDynamicGameObjectPool : MonoBehaviour {

    public GameObject prefab;

    DynamicGameObjectPool pool;

	// Use this for initialization
	void Start ()
    {
        pool = new DynamicGameObjectPool(prefab, 10, 1f, InitGo, DisposeGo);
	}
	
	// Update is called once per frame
	void Update ()
    {
	    	
	}

    void InitGo(GameObject go)
    {
        go.transform.position = new Vector3(Random.Range(0f, 10f), Random.Range(0f, 10f), Random.Range(0f, 10f));
        go.SetActive(false);
        go.AddComponent<PeriodDestroy>();
    }

    void DisposeGo(GameObject go)
    {
        go.SetActive(false);
        //go.GetComponent<PeriodDestroy>().Reset();
    }

    void OnGUI()
    {
        GUILayout.Label(string.Format("池中数量：{0}", pool.UsableCount));
        GUILayout.Label(string.Format("工作数量：{0}", pool.WorkingCount));
        GUILayout.Label(string.Format("Timer 数量：{0}", TimerManager.Instance.Count));
        if (GUILayout.Button("新增"))
        {
            GameObject go = pool.Get();
            go.SetActive(true);
            PeriodDestroy pd = go.GetComponent<PeriodDestroy>();
            pd.dyPool = pool;
            pd.SetUp();
        }    
    }
}
