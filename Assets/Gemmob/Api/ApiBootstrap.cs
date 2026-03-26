using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Place this into the first scene
/// </summary>
public partial class ApiBootstrap : MonoBehaviour {
    [SerializeField] private bool preloadAnalytics = true;
    [SerializeField] private bool preloadAds = true;
    [SerializeField] private bool preloadIAP = true;

    void Awake() {
        DontDestroyOnLoad(gameObject);

        if (preloadAnalytics) {
            PreloadAnalytics();
        }

        if (preloadAds) {
            PreloadAds();
        }

        if (preloadIAP) {
            PreloadIAP();
        }

    }

    partial void PreloadAnalytics();
    partial void PreloadAds();
    partial void PreloadIAP();

}
