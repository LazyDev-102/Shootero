

using DG.Tweening;
using UnityEngine;

public class ShipPreDieEffect : MonoBehaviour {
    [SerializeField] private DOTweenAnimation[] dotweenAnims;

    public void StartEffect() {
        gameObject.SetActive(true);
        foreach (var anim in dotweenAnims) {
            anim.gameObject.SetActive(true);
            anim.DOPlay();
        }
    }

    public void StopEffect() {
        gameObject.SetActive(false);
        foreach (var anim in dotweenAnims) {
            anim.gameObject.SetActive(false);
            anim.DOPause();
        }
    }
}
