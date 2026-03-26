

using System;
using UnityEngine;

public class BossEffect : EnemyEffect {
    [Header("BossRage")]
    [SerializeField] private FlashEffect flashEffect;
    [SerializeField] private ParticleSystem breakParticle;
    [SerializeField] private Vector3 offsetBreakPosition;
    private Vector3 orginEulerBreakParticle = new Vector3(-90, 0, 0);
    [Header("Boss Pre Die")]
    [SerializeField] private PreDieEffectBoss preDieBoss;

    public void StartBreakEffect() {
        if (flashEffect) {
            flashEffect.StartEffect();
        }
        if (breakParticle) {
            breakParticle.transform.position = transform.position + offsetBreakPosition;
            breakParticle.transform.eulerAngles = orginEulerBreakParticle;
            breakParticle.Play();
        }
    }

    public void StartPreDieBoss(Action onComplete) {
        if (preDieBoss) {
            preDieBoss.StartEffect(onComplete);
        }
        else {
            onComplete?.Invoke();
        }
    }
#if UNITY_EDITOR
    [SerializeField] BossEffect reference;
    [UnityEngine.ContextMenu("Convert")]
    protected void Convert() {
        flashEffect = reference.flashEffect;
        breakParticle = reference.breakParticle;
        offsetBreakPosition = reference.offsetBreakPosition;
        preDieBoss = reference.preDieBoss;
        enemyHitEffect = reference.enemyHitEffect;
        burningEffect = reference.burningEffect;
        burningStackPrefab = reference.burningStackPrefab;
        burnStackOffset = reference.burnStackOffset;
    }
#endif
}
