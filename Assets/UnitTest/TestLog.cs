using System;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;

public class TestLog : MonoBehaviour
{
    private void Awake()
    {
        GameFrameworkLog.SetLogHelper(new DefaultLogHelper());
        Utility.Text.SetTextHelper(new DefaultTextHelper());
    }

    private void Start()
    {
        Log.Debug("{0}{1}", 3, "family");
        Log.Info("Info");
        Log.Error("Error"); 
        Log.Fatal("Fatal");
    }
}
