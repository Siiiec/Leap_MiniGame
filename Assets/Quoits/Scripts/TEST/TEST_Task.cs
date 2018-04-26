using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Threading;

public class TEST_Task : MonoBehaviour {

    public Button button;

    private void Start()
    {
        LogOutput("MainThreadID");
        button.onClick.AddListener(()=>Task.Run(Func2));
        button.onClick.AddListener(async () => { LogOutput("Debuguugs" + transform.ToString()); await Task.Delay(100); });
        //Task.Run(() => LogOutput("Task" + transform.ToString()));
    }

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

    private async Task Func2()
    {
        LogOutput("---Start---");
        foreach (var i in Enumerable.Range(1, 10))
        {
            await Task.Delay(1000);
            LogOutput($"Count:{i}");    //文字列内で展開する
        }

        LogOutput("---End---");
    }

    private void LogOutput(string message)
    {
        Debug.Log(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "\t" + message + ":" + Thread.CurrentThread.ManagedThreadId);
    }
}
