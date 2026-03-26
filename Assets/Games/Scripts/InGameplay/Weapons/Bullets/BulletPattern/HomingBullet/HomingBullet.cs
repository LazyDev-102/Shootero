using Gemmob;
using Helper;
using System.Collections;
using UnityEngine;
using System;

public class HomingBullet : BulletBase {
    [SerializeField] protected float turn;
    [SerializeField] protected float delayHoming;
    [SerializeField] protected float timeHoming;
    [SerializeField] protected Rigidbody2D myRigi;
    [SerializeField] private LayerMask targetMask;
    protected float speed;
    protected bool isHoming;
    protected float countdownHoming;
    protected float acceler;
    protected bool canFindTarget;

    protected Transform myTransform;
    protected Transform target;
    private CharacterBase owner;

    protected override void OnEnable() {
        base.OnEnable();
        myTransform = transform;
    }
    public void SetInfo(ShipBase ship) {
        turn = ship.ShipStat.TurnHoming.Value == 0 ? turn : ship.ShipStat.TurnHoming.Value;
        timeHoming = ship.ShipStat.TimeHoming.Value == 0 ? timeHoming : ship.ShipStat.TimeHoming.Value;
    }
    public void SetOwner(CharacterBase owner) {
        this.owner = owner;
    }
    public void FindNewTarget() {
        //Debug.LogError("TD: " + "1: " + transform.up + " || 2" + Vector2.Reflect(transform.up, transform.up.normalized));
        RaycastHit2D raycastHit2D = Physics2D.CircleCast(transform.position, 15, transform.up.normalized, 20, LayerMask.GetMask(GameLayer.Enemy));
        if (raycastHit2D) {
            Vector2 point = raycastHit2D.point;
            if (!BorderHelper.IsOutBound(point)) {
                if (raycastHit2D.collider != null) {
                    target = raycastHit2D.collider.transform;
                    return;
                }
            }
        }
        canFindTarget = target != null;
    }
    public void Shoot(float speed, Transform target, Vector2 direction, float acceler = 0, bool findNextTarget = false) {
        this.speed = speed + SpeedStat.Value;
        this.target = target;
        this.acceler = acceler;
        isHoming = false;
        canFindTarget = findNextTarget;
        if (Mathf.Abs(Vector2.SignedAngle(direction, Vector2.down)) < 2) {
            direction = UnityHelper.Down;
        }
        myTransform.up = direction;
        myRigi.velocity = myTransform.up * speed;
        ShipBase ship = GameManager.Instance.GameLoader.Ship;
        if (ship && owner == ship)
            SetInfo(ship);
        if (gameObject.activeInHierarchy)
            StartCoroutine(HoldHoming());
    }

    public void SetLifeTimeHoming(float lifeTime) {
        this.timeHoming = lifeTime;
    }

    public void SetDelayHoming(float delayHoming) {
        this.delayHoming = delayHoming;
    }

    protected IEnumerator HoldHoming() {
        yield return Yielder.Wait(delayHoming);
        countdownHoming = timeHoming;
        isHoming = true;
    }

    public void ChangeSpriteSize(float size) {
        transform.localScale *= size;
    }


    protected virtual void FixedUpdate() {
        if (target != null) {
            if (isHoming && countdownHoming > 0) {
                myRigi.velocity = myTransform.up * speed;
                Vector3 targetVector = target.position - myTransform.position;
                float rotatingIndex = Vector3.Cross(targetVector, myTransform.up).z;
                myRigi.angularVelocity = -1 * rotatingIndex * turn;
                speed += acceler * Time.deltaTime;
                countdownHoming -= Time.fixedDeltaTime;
            }
            else {
                myRigi.angularVelocity = 0;
                myTransform.up = myRigi.velocity;
            }
            if (canFindTarget && !target.gameObject.activeInHierarchy) {
                FindNewTarget();
            }
        }
    }
}
