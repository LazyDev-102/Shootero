using DG.Tweening;
using Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticSatelliteBullet : BulletBase {
    [SerializeField] private Rigidbody2D myRigi;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private GameObject normalBall;
    private float speed;
    private Transform target;
    private System.Action onComplete;
    private bool isFireBall;
    protected override void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(GameTag.Respawn)) {
            Destroy();
        }
        if (IsBlockHit()) {
            return;
        }
        foreach (var target in targetTypes) {
            if (collision.CompareTag(target.ToString())) {
                Hit(collision);
                return;
            }
        }
    }

    protected override void Hit(Collider2D collision) {
        isHitted = true;
        GetComponent<Collider2D>().enabled = false;
        IHitbox victim = collision.GetComponent<IHitbox>();
        if (victim != null) {
            victim.TakeHit(HitInfor, transform.position);
        }
        DestroyWithEffect();
    }
    protected virtual void Update() {
        if (target != null && target.gameObject.activeInHierarchy) {
            var smoothedPosition = Vector3.Lerp(transform.position, target.position, 0.01f * speed);
            transform.position = smoothedPosition;
        }
        else {
            if (gameObject.activeInHierarchy) {
                transform.position += transform.up * Time.deltaTime * speed;
            }
        }
    }
    public void SetData(Transform target, float speed, System.Action onComplete, bool isFireBall) {
        this.target = target;
        this.speed = speed;
        this.onComplete = onComplete;
        this.isFireBall = isFireBall;
        UpdateUI();
    }
    protected override void RemoveMe() {
        base.RemoveMe();
        onComplete?.Invoke();
    }
    private void UpdateUI() {
        normalBall.SetActive(!isFireBall);
        fireBall.SetActive(isFireBall);
        MyTrail.gameObject.SetActive(!isFireBall);
    }
}
