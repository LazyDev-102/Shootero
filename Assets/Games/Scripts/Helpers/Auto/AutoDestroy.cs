using UnityEngine;
using Gemmob;

public class AutoDestroy : MonoBehaviour {
    [SerializeField] private HideType hideType;
    [SerializeField] private float timeLife;
    [SerializeField] private bool activeOnEnable;

    private Countdowner countdowner;

    private void OnEnable() {
        if (activeOnEnable) {
            countdowner.StartCountdown(timeLife);
        }
    }

    private void OnDisable() {
        countdowner.StartCountdown(timeLife);
    }

    public void StartAutoDestroy(float timeLife, HideType hideType) {
        this.hideType = hideType;
        this.timeLife = timeLife;
        countdowner.StartCountdown(timeLife);
    }

    private void Update() {
        if (countdowner.IsCountdowning()) {
            countdowner.Countdowning(Time.deltaTime);
            if (countdowner.IsTimeOut()) {
                Destroy();
            }
        }
    }

    private void Destroy() {
        if (hideType == HideType.Destroy) {
#if UNITY_EDITOR
            DestroyImmediate(gameObject);
#else
            Destroy(gameObject);
#endif
        }
        else if (hideType == HideType.Disable) {
            gameObject.SetActive(false);
        }
        else if (hideType == HideType.Pool) {
            gameObject.Recycle();
        }
    }

    public enum HideType {
        Destroy, Disable, Pool
    }
}
