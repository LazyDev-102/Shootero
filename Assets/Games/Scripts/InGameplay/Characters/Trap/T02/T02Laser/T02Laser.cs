
using DG.Tweening;
using Gemmob;
using Helper;
using System.Collections.Generic;
using UnityEngine;

public class T02Laser : MonoBehaviour {

    [SerializeField] private float minDistance = 1;
    [SerializeField] private float offsetOut = 1;
    [SerializeField] private float offsetIn = 1;
    [SerializeField] private float moveInDuration = 1;
    [SerializeField] private float moveOutDuration = 1;
    [SerializeField] private float durationShot;
    [SerializeField] private float deltaAttack;


    [SerializeField] private Transform startLaser;
    [SerializeField] private Transform endLaser;
    [SerializeField] private LineRenderer hintLine;
    [SerializeField] private LineRenderer laserLine;
    [SerializeField, Range(0, 5)] private float laserSize = 0.1f;
    [SerializeField, Range(0, 1)] private float hintSize = 0.1f;
    [SerializeField, Range(0, 1)] private float laserTimeOffLaserPercent = 1;
    [SerializeField, Range(0, 1)] private float hintTimeOffLaserPercent = 1;
    [SerializeField] private RangeFloatValue laserSizeRandom;

    [SerializeField] private EdgeCollider2D edgeCollider;
    [Header("Warning")]
    [SerializeField] private TrapWarning startWarningObj;
    [SerializeField] private TrapWarning endWarningObj;

    [SerializeField] private bool isB12Laser;
    [SerializeField] private float offsetRadius;


    private bool isVertical;
    private Vector2 position;
    private Vector2 startOutPosition;
    private Vector2 endOutPosition;
    private Vector2 startInPosition;
    private Vector2 endInPosition;
    private bool isMoving;
    private bool isAttacking;
    float hintTimeOffPoint;
    float laserTimeOffPoint;


    private Countdowner moveInCountdowner = new Countdowner();
    private Countdowner attackCountdown = new Countdowner();
    private Countdowner deltaAttackCountdown = new Countdowner();

    private T02Base t02Base;

    private HitInfor hitInfo;


    public Vector2 Position { get => position; }
    public bool IsVertical { get => isVertical; }

    public void SetT02Base(T02Base t02) {
        t02Base = t02;
    }

    public void SetHitInfo(int damage, List<IEffectAttackModable> effects, ObjectBase causer) {
        if (hitInfo == null) {
            hitInfo = new HitInfor();
        }
        hitInfo.SetInfor(damage, effects, causer);
    }

    public void SetBaseHitInfo(int damage, List<IEffectAttackModable> effects, ObjectBase causer) {

    }
    private EdgeBorder SpawnEDGEBoss12(float spawnBorderOffset) {
        float w = ConfigIngameData.borderW;
        float h = ConfigIngameData.borderH;
        Vector2 topLeft = new Vector2(-(w / 2.0f + spawnBorderOffset), h / 2.0f + spawnBorderOffset);
        Vector2 botLeft = new Vector2(-(w / 2.0f + spawnBorderOffset), -(h / 2.0f + spawnBorderOffset));
        EdgeBorder edge = new EdgeBorder() { begin = Vector2.Lerp(topLeft, botLeft, 0.5f), end = botLeft };
        return edge;
    }
    public void Spawn(AreaType spawnBorderType, float spawnBorderOffset, bool isTarget = false) {
        bool spawnSucess;
        var loopTime = 0;
        do {
            loopTime++;
            if (loopTime > 50)
                break;
            EdgeBorder edge = isB12Laser ? SpawnEDGEBoss12(spawnBorderOffset) : BorderHelper.GetRandomEdge(spawnBorderType, spawnBorderOffset);
            isVertical = !edge.IsVertical(0.01f);
            Vector2 randomPosition = isTarget ? (Vector2)GameManager.Instance.GameLoader.Ship.transform.position : edge.GetRandomMidPoint();
            if (isVertical) {
                position = new Vector2(randomPosition.x, CameraHelper.Camera.transform.position.y);
            }
            else {
                position = new Vector2(CameraHelper.Camera.transform.position.x, randomPosition.y);
            }
            if (!CameraHelper.ObjectInsideCameraView(position)) {
                spawnSucess = false;
                continue;
            }
            spawnSucess = t02Base.CheckPositionT02(this);
        } while (!spawnSucess);
        t02Base.AddT02(this);

        // set position spawn
        if (isVertical) {
            float halfScreen = CameraHelper.GetHeight / 2;
            startOutPosition = position + new Vector2(0, halfScreen + offsetOut);
            endOutPosition = position - new Vector2(0, halfScreen + offsetOut);

            startInPosition = position + new Vector2(0, halfScreen - offsetIn);
            endInPosition = position - new Vector2(0, halfScreen - offsetIn);

            startLaser.localEulerAngles = new Vector3(0, 0, -180);
            endLaser.localEulerAngles = new Vector3(0, 0, 0);

        }
        else {
            float halfScreen = CameraHelper.GetWidth / 2;
            startOutPosition = position + new Vector2(halfScreen + offsetOut, 0);
            endOutPosition = position - new Vector2(halfScreen + offsetOut, 0);

            startInPosition = position + new Vector2(halfScreen - offsetIn, 0);
            endInPosition = position - new Vector2(halfScreen - offsetIn, 0);


            startLaser.localEulerAngles = new Vector3(0, 0, 90);
            endLaser.localEulerAngles = new Vector3(0, 0, -90);
        }

        hintTimeOffPoint = moveInDuration * (1 - hintTimeOffLaserPercent);
        laserTimeOffPoint = durationShot * (1 - hintTimeOffLaserPercent);
        startLaser.position = startOutPosition;
        endLaser.position = endOutPosition;
        SetStateHint(true);
        StartMoveIn();
    }

    public void SetStateHint(bool show) {
        if (show) {
            UpdateHintPosition();
            hintLine.widthMultiplier = hintSize;
        }
        hintLine.gameObject.SetActive(show);
    }

    public void UpdateHintPosition() {
        hintLine.SetPosition(0, startLaser.position);
        hintLine.SetPosition(1, endLaser.position);
    }

    public void SetStateLaserLine(bool active) {
        if (active) {
            laserLine.SetPosition(0, startLaser.position);
            laserLine.SetPosition(1, endLaser.position);
            Vector2[] points = new Vector2[2];
            points[0] = transform.InverseTransformPoint(startLaser.position);
            points[1] = transform.InverseTransformPoint(endLaser.position);
            edgeCollider.points = points;
        }
        laserLine.gameObject.SetActive(active);
    }

    public void StartMoveIn() {
        isMoving = true;
        startWarningObj.gameObject.SetActive(true);
        endWarningObj.gameObject.SetActive(true);
        moveInCountdowner.StartCountdown(moveInDuration);
        if (IsVertical) {
            startWarningObj.Updating(startInPosition, Vector3.up, -1);
            endWarningObj.Updating(endInPosition, Vector3.down, -1);

        }
        else {
            startWarningObj.Updating(startInPosition, Vector3.right, -1);
            endWarningObj.Updating(endInPosition, Vector3.left, -1);
        }
        startLaser.DOMove(startInPosition, moveInDuration).SetEase(Ease.Linear).OnComplete(() => {
            isMoving = false;
            StartAttack();
        });
        endLaser.DOMove(endInPosition, moveInDuration).SetEase(Ease.Linear).OnComplete(() => {
            isMoving = false;
        });
    }

    public void StartMoveOut() {
        isMoving = true;

        endLaser.DOMove(endOutPosition, moveOutDuration).SetEase(Ease.Linear).OnComplete(() => {
            isMoving = false;
        });
        startLaser.DOMove(startOutPosition, moveOutDuration).SetEase(Ease.Linear).OnComplete(() => {
            isMoving = false;
            t02Base.RemoveT02Laser(this);
            Despawn();
        });
    }

    public void StartAttack() {
        startWarningObj.gameObject.SetActive(false);
        endWarningObj.gameObject.SetActive(false);
        SetStateHint(false);
        SetStateLaserLine(true);
        attackCountdown.StartCountdown(durationShot);
        deltaAttackCountdown.StartCountdown(deltaAttack);
        isAttacking = true;
        edgeCollider.edgeRadius = laserSize / 2;
        float pos = transform.position.y + 0.1f;
        transform.DOMoveY(pos, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    public void EndAttack() {
        isAttacking = false;
        SetStateLaserLine(false);
        StartMoveOut();
        edgeCollider.enabled = false;
    }

    public void Despawn() {
        this.Recycle();
    }

    public bool CheckConflicPositionSpawn(T02Laser t02) { // conflic return true
        if (IsVertical) {
            return Mathf.Abs(position.x - t02.position.x) < minDistance;
        }
        return Mathf.Abs(position.y - t02.position.y) < minDistance;
    }

    public void Updating() {
        if (isMoving) {
            UpdateHintPosition();
            moveInCountdowner.Countdowning(Time.deltaTime);
            float timeOffElapsed = moveInCountdowner.Countdown;
            if (timeOffElapsed < hintTimeOffPoint) {
                float percentSize = hintTimeOffPoint == 0 ? 1 : timeOffElapsed / hintTimeOffPoint;
                hintLine.widthMultiplier = hintSize * percentSize;
            }
        }
        if (isAttacking) {
            attackCountdown.Countdowning(Time.deltaTime);
            deltaAttackCountdown.Countdowning(Time.deltaTime);
            float timeOffElapsed = attackCountdown.Countdown;
            if (timeOffElapsed < laserTimeOffPoint) {
                float percentSize = laserTimeOffPoint == 0 ? 1 : timeOffElapsed / laserTimeOffPoint;
                laserLine.widthMultiplier = laserSize * percentSize;
            }

            edgeCollider.enabled = false;
            if (deltaAttackCountdown.IsTimeOut()) {
                edgeCollider.enabled = true;
                deltaAttackCountdown.StartCountdown(deltaAttack);
                this.DelayFrame(2, DisableCollider);
                if (timeOffElapsed >= laserTimeOffPoint) {
                    laserLine.widthMultiplier = laserSizeRandom.GetRandomValue() * laserSize;
                }
            }
            if (attackCountdown.IsTimeOut()) {
                EndAttack();
            }
        }
    }

    private void DisableCollider() {
        edgeCollider.enabled = false;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        IHitbox victim = collision.GetComponent<IHitbox>();
        if (victim != null && hitInfo != null) {
            victim.TakeHit(hitInfo, transform.position);
        }
    }
}
