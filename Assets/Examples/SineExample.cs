using Modules.Charting;
using Modules.Charting.Entities.Monitors;
using System;
using UnityEngine;

public class SineExample : MonoBehaviour
{
    private MonitorSample monitor;

    void Start()
    {
        this.monitor = FindObjectOfType<MonitorSample>();
    }

    void FixedUpdate()
    {
        var val = Math.Sin(Time.realtimeSinceStartup * 10) + 1;
        this.monitor.AddSample((short)(val * short.MaxValue / 2));
    }
}
