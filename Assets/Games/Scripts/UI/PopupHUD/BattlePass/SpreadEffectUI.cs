using DG.Tweening;
using Gemmob;
using UnityEngine;
using UnityEngine.UI;

public class SpreadEffectUI : MonoBehaviour {
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private Transform parrent;
    [SerializeField] private float duration;
    [SerializeField] private float deltaTime;
    [SerializeField] private float multiSpread;
    [SerializeField] private float maxAlpha = 1f;
    [SerializeField] private float minAlpha = 0f;
    [SerializeField] private bool autoPlay;
    private bool active;
    private Countdowner cd = new Countdowner();
    private void Awake() {
        effectPrefab.RegisterPool(5);
        cd.StartCountdown(deltaTime);
    }
    private void Update() {
        if (autoPlay || active) {
            if (cd.IsTimeOut()) {
                var effectClone = effectPrefab.Spawn(parrent);
                effectClone.gameObject.SetActive(true);
                var image = effectClone.GetComponent<Image>();
                image.SetAlpha(maxAlpha);
                effectClone.transform.localPosition = Vector3.zero;
                effectClone.transform.DOScale(multiSpread, duration);
                image.DOFade(minAlpha, duration);
                cd.StartCountdown(deltaTime);
            }
            cd.Countdowning(Time.deltaTime);
        }
    }
    public void UpdateUI(bool status) {
        gameObject.SetActive(status);
        active = status;
    }
}
