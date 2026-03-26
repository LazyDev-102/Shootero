using UnityEngine;

public class EffectAutoDestroy : MonoBehaviour {
    [SerializeField] private float timeLife;
    [SerializeField] private bool activeOnEnable;

    private ParticleSystem effect;
    private Countdowner countdowner;

    private void OnEnable() {
        effect = GetComponent<ParticleSystem>();
        if (activeOnEnable) {
            countdowner.StartCountdown(timeLife);
        }
    }

    private void OnDisable() {
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
        if (GameManager.Initialized && effect != null)
            GameManager.Instance.GameLoader.DeSpawnEffectExplosion(effect);
    }
}
