using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestModeController : MonoBehaviour
{
    public GameObject testObject;

    void Awake()
    {
        if(testObject != null)
#if TEST
        testObject.SetActive(true);
#else
        testObject.SetActive(false);
#endif
    }

    
}
