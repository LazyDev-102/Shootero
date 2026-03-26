using System.Collections;
using UnityEngine;
using Gemmob;
using DG.Tweening;

public class XMB01Skill01AttackComponent : MinibossAttackComponent<XMB01Attack> {
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform shield;
    [SerializeField] private ParticleSystem burstEffect;
    [SerializeField] private float damagePercent;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackCount;
    [SerializeField] private float aimTime;

    private bool activeAim;
    private bool isMoving;
    private bool playEffect;
    private int numberAttack = 0;

    public override void StartAttack() {
        activeAim = false;
        isMoving = false;
        playEffect = false;
        numberAttack = 0;
        minibossAttack.XMB01Base.XMB01Move.StopMoveIdle();
    }
    public override void Updating() {
        if (activeAim) {
            minibossAttack.XMB01Base.LookTarget();
        }
        if (isMoving) {
            minibossAttack.XMB01Base.XMB01Move.MoveFront();
            if (burstEffect != null && !playEffect) {
                playEffect = true;
                burstEffect.Play();
            }
            if (minibossAttack.XMB01Base.XMB01Move.HasOutBorder()) {
                isMoving = false;
                numberAttack++;
                var ranVector2 = new Vector2(0.5f, 1.1f);
                minibossAttack.transform.position = minibossAttack.XMB01Base.XMB01Move.GetPointMoveXMB01(ranVector2);
                ChangeZ(minibossAttack.transform);
                if (numberAttack < attackCount) {
                    activeAim = true;
                    DOVirtual.DelayedCall(0.1f, () => { activeAim = false; isMoving = true; });
                }
                else {
                    var posDefault = new Vector2(0.5f, 0.8f);
                    minibossAttack.transform.DOMove(minibossAttack.XMB01Base.XMB01Move.GetPointMoveXMB01(posDefault), 2f).OnComplete(() => EndAttack());
                }
            }
        }
    }

    public override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(IDelayAttack());
    }

    public override void EndAttack() {
        base.EndAttack();
    }

    private IEnumerator IDelayAttack() {
        activeAim = true;
        yield return Yielder.Wait(aimTime);
        activeAim = false;
        isMoving = true;
        minibossAttack.XMB01Base.XMB01Move.SetTargetMoveAttack(minibossAttack.Target.position, moveSpeed);
    }

    private void ChangeZ(Transform trans) {
        var temp = trans.rotation;
        temp.z = 180;
        trans.rotation = temp;
    }

    [System.Serializable]
    private class AttackData {
        [SerializeField] private float damagePercent;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float attackCount;
        [SerializeField] private float aimTime;

        public float DamagePercent => damagePercent;
        public float MoveSpeed => moveSpeed;
        public float AttackCount => attackCount;
        public float AimTime => aimTime;
    }
}