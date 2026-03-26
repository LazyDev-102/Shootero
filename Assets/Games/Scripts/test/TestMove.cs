using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour {
    float unit = 0f;
    Vector3 temp;
    [SerializeField] float freq = 2;
    void Update() {
        temp = transform.position;
        temp.x = unit * (float)Math.Cos(Time.time * freq);
        temp.y = unit * (float)Math.Sin(Time.time * freq);
        transform.position = temp;
        unit += Time.deltaTime;
    }
}
