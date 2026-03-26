using UnityEngine;
using System;
using System.Collections.Generic;
using Gemmob;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class MB12Skill02AttackComponent : MinibossAttackComponent<MB12Attack> {
    [SerializeField] private float delayAttack;
    [SerializeField] private float timeRevoke = 1f;
    [SerializeField] private float speedRevoke = 2f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private RotateFrontBullet bulletPrefab;
    [SerializeField] private float damagePercent;
    [SerializeField, Tooltip("Size vòng tròn tỏa ra: 3->10")] private float radiusRoundSize;
    [SerializeField, Tooltip("Value small == Speed up")] private float bulletSpeed;
    [SerializeField] private int dartCount;
    [SerializeField, Tooltip("Value small == Speed up")] private float timeRotation;
    [SerializeField] private float attack1Time;
    [SerializeField] private float attack2Time;
    [SerializeField] private int numberPreload;

    private Transform bulletParent;

    private List<RotateFrontBullet> bullets = new List<RotateFrontBullet>();
    private bool hasAttack1;
    private bool hasAttack2;
    private bool hasAttack3;

    private bool attacking;
    private Countdowner attack1Countdowner = new Countdowner();
    private Countdowner attack2Countdowner = new Countdowner();
    private Countdowner delayAttackCountdowner = new Countdowner();

    public override void PreloadIngame() {
        if (bulletPrefab) {
            bulletPrefab.PreloadIngame();
            bulletPrefab.RegisterPool(numberPreload);
        }

    }

    private void ClearBullet() {
        if (bullets != null) {
            foreach (var item in bullets) {
                if (item != null) {
                    item.Recycle();
                }
            }
            bullets.Clear();
        }
    }
    private void SpawnBulletParrent() {
        if (bulletParent == null) {
            bulletParent = new GameObject("MB12BulletParent").transform;
            bulletParent.SetParent(GameLoader.transform);
            bulletParent.localPosition = minibossAttack.transform.localPosition;
        }
        else {
            bulletParent.localPosition = minibossAttack.transform.localPosition;
        }
    }
    public override void StartAttack() {
        hasAttack1 = false;
        hasAttack2 = false;
        hasAttack3 = false;
        attacking = false;
        ClearBullet();
        SpawnBulletParrent();
        SpawnDarts();
        minibossAttack.MB12Base.MB12Move.StartMoveAfterAttackMB12(new Vector2(0.5f, 0.5f));
    }
    public override void Attacking() {
        delayAttackCountdowner.StartCountdown(delayAttack);
        attack1Countdowner.StartCountdown(attack1Time);
        attack2Countdowner.StartCountdown(attack2Time);
    }
    private void SpawnDarts() {
        for (int i = 0; i < dartCount; i++) {
            var item = bulletPrefab.Spawn(bulletParent);
            item.Initalize();
            item.gameObject.SetActive(false);
            item.transform.localPosition = firePoint.localPosition;
            item.transform.localScale = Vector3.one * 2;
            item.SetHitInfor((int)damagePercent * minibossAttack.MB12Base.MB12Stat.Atk.Value, null, minibossAttack.MB12Base);
            bullets.Add(item);
        }
    }
    private void Attack1() {//Move out
        if (hasAttack1 || minibossAttack == null || bulletParent == null)
            return;
        bulletParent.transform.position = minibossAttack.transform.position;
        hasAttack1 = true;
        int per = 360 / bullets.Count;
        for (int i = 0; i < bullets.Count; i++) {
            var radians = (Math.PI / 180) * per * i;
            bullets[i].gameObject.SetActive(true);
            bullets[i].transform.DOMove(bulletParent.position + new Vector3((float)Math.Cos(radians), (float)Math.Sin(radians)) * radiusRoundSize, bulletSpeed);
        }
    }
    TweenerCore<Quaternion, Vector3, QuaternionOptions> rotateTween;
    private void Attack2() {//Rotation
        if (hasAttack2)
            return;
        hasAttack2 = true;
        if (rotateTween != null)
            rotateTween.Kill();
        rotateTween = bulletParent.DOLocalRotate(new Vector3(0, 0, -180), timeRotation).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }
    private void Attack3() {//Move in
        if (hasAttack3 || minibossAttack == null || bulletParent == null || minibossAttack.MB12Base == null)
            return;
        minibossAttack.MB12Base.MB12Move.EndMoveIdle();
        hasAttack3 = true;
        bulletParent.transform.position = minibossAttack.transform.position;
        foreach (var item in bullets) {
            item.transform.DOMove(bulletParent.transform.position, timeRevoke).OnComplete(() => {
                item.Recycle();
                EndAttack();
            });
        }

    }

    public override void EndAttack() {
        ClearBullet();
        base.EndAttack();
    }

    public override void Updating() {
        if (!attacking) {
            attacking = minibossAttack.MB12Base.MB12Move.CompleteMoveToTarget();
        }
        else {
            minibossAttack.MB12Base.LookTarget();
            if (delayAttackCountdowner.IsCountdowning()) {
                delayAttackCountdowner.Countdowning(Time.deltaTime);
            }
            else {
                attack1Countdowner.Countdowning(Time.deltaTime);
                if (attack1Countdowner.IsCountdowning()) {
                    Attack1();
                }
                else {
                    attack2Countdowner.Countdowning(Time.deltaTime);
                    if (attack2Countdowner.IsCountdowning()) {
                        Attack2();
                    }
                    else {
                        Attack3();
                    }
                }
            }
        }
    }


    public override void StopAttack() {
        ClearBullet();
        base.StopAttack();
    }
}
