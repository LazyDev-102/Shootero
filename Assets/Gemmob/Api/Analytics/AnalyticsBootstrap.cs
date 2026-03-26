using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ApiBootstrap {
    partial void PreloadAnalytics() {
        Tracking.Instance.Preload();
    }
}