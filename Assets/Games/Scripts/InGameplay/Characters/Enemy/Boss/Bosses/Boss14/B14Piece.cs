using DG.Tweening;
using System;
using UnityEngine;

public class B14Piece : MonoBehaviour, IHitbox {
    [SerializeField] private int hp;
    [SerializeField] private Collider2D myCollider;
    [SerializeField] private AnimationCurve moveAttackCurve;
    [SerializeField] private EnemyHitEffect eHitEffect;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject trail;

    protected Tweener curMoveTween;
    private Action<int> onHitDamage;
    private int currentHP;
    private B14Base b14Base;
    private bool canHitDamage;

    public int Hp { get => hp; }
    public int CurrentHP { get => currentHP; }

    public void Initialize() {
        ChangeState(true);
        TurnShield(false);
        TurnTrail(false);
        ChangeCanHitDamage(true);
    }

    public B14Piece SetMaxHeath(int value) {
        hp = value;
        currentHP = hp;
        return this;
    }
    public B14Piece SetParent(B14Base b14Base) {
        this.b14Base = b14Base;
        return this;
    }
    public B14Piece OnHitDame(Action<int> onHitDamage) {
        this.onHitDamage = onHitDamage;
        return this;
    }

    public void ChangeCanHitDamage(bool status) {
        canHitDamage = status;
    }
    public void TakeHit(HitInfor hit, Vector2 positionCollider, HitType type = HitType.Normal) {
        if (canHitDamage) {
            currentHP -= hit.Damage.Value;
            if (currentHP < 0) {
                ChangeState(false);
            }
            onHitDamage?.Invoke(hit.Damage.Value);
            eHitEffect.StartEffect();
            TextShowupManager.Instance.ShowHitText(HitType.Normal, $" {hit.Damage.Value}", transform.position);
        }
    }
    private void ChangeState(bool status) {
        gameObject.SetActive(status);
    }
    public Transform Transform() {
        return transform;
    }
    public B14Piece TurnShield(bool turnOn) {
        shield.SetActive(turnOn);
        myCollider.enabled = !turnOn;
        return this;
    }
    public B14Piece TurnTrail(bool turnOn) {
        trail.SetActive(turnOn);
        return this;
    }
    public void MoveAttack(Transform target, float duration) {
        Vector3[] pathPoints = new Vector3[4];
        pathPoints[0] = transform.position;
        pathPoints[1] = transform.position + transform.up.normalized * 1.5f;
        pathPoints[2] = target.position;
        pathPoints[3] = target.position.y > 0 ? target.position + target.up.normalized * 15 : target.position + target.up.normalized * -15;
        curMoveTween?.Kill();
        curMoveTween = transform.DOPath(pathPoints, duration, PathType.CatmullRom, PathMode.TopDown2D, 5).SetLookAt(0.01f, Vector3.forward, Vector3.right).OnComplete(OnEndMove).SetEase(moveAttackCurve);
    }
    private void OnEndMove() {
        TurnTrail(false);
    }
}
