using DG.Tweening;
using UnityEngine;

public class FlashEffect : MonoBehaviour {
    [SerializeField] private DOTweenAnimation[] whiteFlashs;

    public void StartEffect() {
        foreach (var e in whiteFlashs) {
            e.DORestart();
        }
    }
}
