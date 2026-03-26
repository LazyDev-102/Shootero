using UnityEngine;

public class Config {
    public static bool IsEditor {
        get {
#if UNITY_EDITOR
            return true;
#else
                return false;
#endif
        }
    }

    public static bool IsAndroid {
        get {
#if UNITY_ANDROID
            return true;
#else
            return false;
#endif
        }
    }

    public static bool IsIos {
        get {
#if UNITY_IOS
            return true;
#else
            return false;
#endif
        }
    }

    public static bool IsDebug {
        get {
#if PRODUCTION_BUILD
				return false;
#else
            return true;
#endif
        }
    }

    public static bool IsRelease {
        get {
#if PRODUCTION_BUILD
				return true;

#else
            return false;
#endif
        }
    }

    public static bool IsAdsEnable {
        get {
#if ADS_ENABLE
            return true;
#else
                return false;
#endif
        }
    }

    public static bool IsIapEnable {
        get {
#if IAP_ENABLE
                return true;
#else
            return false;
#endif
        }
    }

    public static bool IsFirebaseEnable {
        get {
#if FIREBASE_ENABLE
				return true;
#else
            return false;
#endif
        }
    }
}