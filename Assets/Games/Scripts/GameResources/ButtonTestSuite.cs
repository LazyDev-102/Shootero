using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gemmob.Lib.Adsv2;

public class ButtonTestSuite : MonoBehaviour
{
    public void OnButtonClicked()
    {
        AdsManager.instance.ShowMediationTestSuite();
        Debug.Log("Open test suite");
    }
}
