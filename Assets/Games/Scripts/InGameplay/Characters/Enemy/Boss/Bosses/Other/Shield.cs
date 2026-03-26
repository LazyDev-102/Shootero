using UnityEngine;

public class Shield : MonoBehaviour {
    [SerializeField] protected Collider2D shieldCollider;
    [SerializeField] protected DotweenAnimation showAnimation;
    [SerializeField] protected DotweenAnimation hideAnimation;
    [SerializeField] protected ParticleSystem showEffect;
    [SerializeField] protected ParticleSystem hideEffect;


    public void Start() {
        if (showAnimation) {
            showAnimation.Initialize();
        }
        if (hideAnimation) {
            hideAnimation.Initialize();
        }
    }

    public virtual void TurnOn() {
        this.gameObject.SetActive(true);
        if (showEffect) {
            showEffect.Play();
        }
        if (shieldCollider != null) {
            shieldCollider.enabled = true;
        }
        if (showAnimation) {
            showAnimation.Play();
        }
    }

    public virtual void TurnOff() {
        if (shieldCollider != null) {
            shieldCollider.enabled = false;
        }
        if (showEffect && showEffect.isPlaying) {
            showEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
        if (hideEffect) {
            hideEffect.Play();
        }
        if (hideAnimation) {
            hideAnimation.Play(() => {
                this.gameObject.SetActive(false);
            }, true);
        }
        else {
            this.gameObject.SetActive(false);
        }

    }
}
