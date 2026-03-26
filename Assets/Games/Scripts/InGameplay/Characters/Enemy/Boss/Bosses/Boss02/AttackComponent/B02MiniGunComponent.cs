

using System;
using UnityEngine;

public class B02MiniGunComponent : MonoBehaviour {
    [SerializeField] private Transform firePoint;
    [SerializeField] private float speedLook;
    [SerializeField] private Rigidbody2D myRigi;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private ParticleSystem showEffect;
    [SerializeField] private ParticleSystem hideEffect;
    [SerializeField] private DotweenAnimation showAnima;
    [SerializeField] private DotweenAnimation hideAnima;

    private bool canLook;

    public void Initialize() {
        if (showAnima) {
            showAnima.Initialize();
        }
        if (hideAnima) {
            hideAnima.Initialize();
        }
    }
    public void LookTarget(Transform target) {
        if (canLook) {
            Vector2 direction = (Vector2)target.position - myRigi.position;
            myRigi.MoveRotation(Mathf.LerpAngle(myRigi.rotation, Vector2.SignedAngle(Vector2.up, direction), Time.deltaTime * speedLook));
        }
    }

    public void Shot(FrontBullet bullet, float speed) {
        if (muzzle) {
            muzzle.Play();
        }
        bullet.transform.position = firePoint.position;
        bullet.Shoot(speed, firePoint.up);
    }

    public void Show() {
        canLook = false;
        if (showEffect) {
            showEffect.Play();
        }
        if (showAnima) {
            showAnima.Play(() => {
                canLook = true;
                myRigi.MoveRotation(180);
            }, true);
        }
    }

    public void Hide(Action onComplete) {
        myRigi.MoveRotation(180);
        if (hideEffect) {
            hideEffect.Play();
        }
        if (hideAnima) {
            hideAnima.Play(onComplete, true);
        }
    }
}
