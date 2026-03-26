
using System;
using UnityEngine;
public class CountTimer : MonoBehaviour {
    private double timeStart;
    private double cTime { get => DateTime.Now.Subtract(DateTime.MinValue).TotalMinutes; }

    public void StartCount() {
        timeStart = cTime;
    }

    public double GetUseTime() {
        return cTime - timeStart;
    }
}
