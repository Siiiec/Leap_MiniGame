using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;

public class TEST_ObjectCreator : MonoBehaviour {

    [SerializeField]
    GameObject prefab;

    public float deleteTime;

    public float cps = 1.0f;

    private void Func1()
    {
        LogOutput("---Start---");
        foreach (var i in Enumerable.Range(1, 10))
        {
            Task.Delay(1000);
            LogOutput($"Count:{i}");    //文字列内で展開する
        }

        LogOutput("---End---");
    }

    private void LogOutput(string message)
    {
        Debug.Log(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "\t" + message);
    }

    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        
    }


}
