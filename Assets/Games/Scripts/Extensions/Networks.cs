using UnityEngine;

public static class Networks {
    public static bool IsInternetAvaiable => Application.internetReachability != NetworkReachability.NotReachable;
}