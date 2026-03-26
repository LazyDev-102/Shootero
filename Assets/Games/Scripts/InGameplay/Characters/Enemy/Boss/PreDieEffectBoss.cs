using System;
using UnityEngine;

public class PreDieEffectBoss : MonoBehaviour {
    [SerializeField] private DotweenAnimation dotweenAnim;
    [SerializeField] private ParticleSystem preDieParticle;
    [SerializeField] private ParticleSystem icePreDieEffect;
    [SerializeField] private Transform fireBossTrans;
    [SerializeField] private Transform iceBossTrans;
    public void StartEffect(Action onComplete) {
        if (preDieParticle) {
            if (fireBossTrans)
                preDieParticle.transform.position = fireBossTrans.position;
            preDieParticle.Play();
        }
        if (icePreDieEffect) {
            if (icePreDieEffect)
                icePreDieEffect.transform.position = iceBossTrans.position;
            icePreDieEffect.Play();
        }
        gameObject.SetActive(true);
        dotweenAnim.Play(onComplete, true);
    }

    public void StopEffect() {
        gameObject.SetActive(false);

    }
}
