using Gemmob;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMove : CharacterMove {
    [SerializeField] protected float speedRotateLook = 10f;
    [SerializeField] protected float timeRotation = 1f;
    [SerializeField] private ParticleSystem appearEffect;
    private Transform enemyTarget;
    private TurretBase turretBase;
    private TweenerCore<Quaternion, Vector3, QuaternionOptions> rotationTweener;
    private bool lockAim;
    public TurretBase TurretBase {
        get {
            if (turretBase == null) {
                turretBase = CharacterBase as TurretBase;
            }
            return turretBase;
        }
    }
    public override void Updating() {
        if (enemyTarget == null)
            return;
        if (!lockAim)
            this.LookTarget(enemyTarget.position);
    }
    public virtual void LookTarget(Vector2 target) {
        LookDirection(target - (Vector2)transform.position);
    }
    public virtual void LookDirection(Vector2 direction) {
        MyRigi.MoveRotation(Mathf.LerpAngle(MyRigi.rotation, Vector2.SignedAngle(Vector2.up, direction), Time.deltaTime * speedRotateLook));
    }

    public void SetEnemyTarget(Transform target) {
        enemyTarget = target;
    }
    public void SetSpawnPos(Vector2 pos) {
        if (gameObject.activeInHierarchy)
            StartCoroutine(Appear(pos));
    }
    private IEnumerator Appear(Vector2 pos) {
        lockAim = true;
        transform.position = pos;
        if (appearEffect != null)
            appearEffect.Play();
        yield return Yielder.Wait(appearEffect.main.duration);
        gameObject.SetActive(true);
        lockAim = false;
    }

    public void Rotation(bool active) {
        rotationTweener?.Kill();
        if (active) {
            lockAim = true;
            rotationTweener = transform.DOLocalRotate(Vector3.forward * -180, timeRotation).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
        }
        else {
            lockAim = false;
        }
    }
}
