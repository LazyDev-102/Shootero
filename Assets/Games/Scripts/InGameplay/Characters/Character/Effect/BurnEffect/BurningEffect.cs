

using DG.Tweening;
using UnityEngine;

public class BurningEffect : MonoBehaviour {
    [SerializeField] private DOTweenAnimation[] dotweenAnims;

    public void StartEffect(bool status = false) {
        if (!status)
            return;
        foreach (var anim in dotweenAnims) {
            anim.gameObject.SetActive(true);
            anim.DOPlay();
        }
    }

    public void StopEffect(bool status = true) {
        if (!status)
            return;
        foreach (var anim in dotweenAnims) {
            anim.gameObject.SetActive(false);
            anim.DOPause();
        }
    }
}
