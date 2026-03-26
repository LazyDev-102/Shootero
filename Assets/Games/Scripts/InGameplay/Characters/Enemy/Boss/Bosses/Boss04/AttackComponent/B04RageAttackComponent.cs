using Gemmob;
using System.Collections.Generic;
using UnityEngine;

public class B04RageAttackComponent : BossAttackComponent {
    [SerializeField] private B04Attack bossAttack;
    [SerializeField] private float timeLife;
    [SerializeField] private float startRadius;
    [SerializeField] private float maxRadius;
    [SerializeField] private float minRadius;
    [SerializeField] private float delayMoveBack;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private int maxBall;
    [SerializeField] private BallBullet ball;
    [SerializeField] private Transform ballContainer;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;

    private float currentRadius;
    private Countdowner delayMoveBackCountdowner;
    private Countdowner timeLifeCountdowner;
    private List<BallBullet> balls = new List<BallBullet>();
    private State currentState = State.None;
    private AttackData attackData;

    private AttackData CurAttackData {
        get {
            if (IngameData.currentGameMode != GameMode.EventBoss)
                return attackDatas[bossAttack.B04Base.CurrentPhaseIndex];
            else
                return bossModeAttackDatas[bossAttack.B04Base.CurrentPhaseIndex];
        }
    }

    public override void PreloadIngame() {
        if (ball) {
            ball.PreloadIngame();
            ball.RegisterPool(maxBall);
        }
    }
    public override void Initialize() {
        base.Initialize();
        currentState = State.None;
    }
    public override void StartAttack() {
        attackData = CurAttackData;
        currentRadius = startRadius;
        currentState = State.None;
    }

    public override void Attacking() {
        bossAttack.B04Base.B04Move.StartMoveInRage();
        InitializeCircleBall();
        currentState = State.MoveOut;
        timeLifeCountdowner.StartCountdown(timeLife);
    }

    public override void EndAttack() {
        bossAttack.B04Base.B04Move.EndMoveInRage();
        base.EndAttack();
        DestroyAllBall();
        balls.Clear();
    }


    public override void Updating() {
        switch (currentState) {
            case State.MoveOut: {
                MoveOutCircleBall();
                RotateCircleBall();
                if (HasMaxRadius()) {
                    currentState = State.Max;
                    delayMoveBackCountdowner.StartCountdown(delayMoveBack);
                }
                break;
            }
            case State.MoveIn: {
                MoveInCircleBall();
                RotateCircleBall();
                if (HasMinRadius()) {
                    currentState = State.MoveOut;
                }
                break;
            }

            case State.Max: {
                RotateCircleBall();
                delayMoveBackCountdowner.Countdowning(Time.deltaTime);
                if (delayMoveBackCountdowner.IsTimeOut()) {
                    currentState = State.MoveIn;
                }
                break;
            }
            case State.None: {
                return;
            }
        }
        timeLifeCountdowner.Countdowning(Time.deltaTime);
        if (timeLifeCountdowner.IsTimeOut()) {
            EndAttack();
        }
    }

    private void InitializeCircleBall() {
        for (int i = 0; i < maxBall; ++i) {
            BallBullet newBall = CreateBall();
            newBall.transform.SetParent(ballContainer, false);
            balls.Add(newBall);
        }
        UpdateBall();
    }

    private void RotateCircleBall() {
        ballContainer.Rotate(Vector3.back, rotateSpeed * Time.deltaTime);
    }

    private void MoveOutCircleBall() {
        currentRadius += moveSpeed * Time.deltaTime;
        UpdateBall();
    }

    private void MoveInCircleBall() {
        currentRadius -= moveSpeed * Time.deltaTime;
        UpdateBall();
    }


    private bool HasMaxRadius() {
        return currentRadius > maxRadius;
    }

    private bool HasMinRadius() {
        return currentRadius < minRadius;
    }

    private bool HasOutOfBall() {
        return balls.Count == 0;
    }


    private BallBullet CreateBall() {
        BallBullet newBall = GameManager.Instance.GameLoader.SpawnBullet(ball, new Vector3(100, 1));
        if (newBall) {
            newBall.SetHitInfor((int)(bossAttack.B04Base.B04Stat.Atk.Value * attackData.DamageBallPercent), null, bossAttack.B04Base);
            newBall.AddOnBallDestroy(OnBallDestroy);
        }
        return newBall;
    }

    private void UpdateBall() {
        int numberBall = balls.Count;
        float deltaAngle = 360f / numberBall;
        for (int i = 0; i < numberBall; ++i) {
            float currentAngle = deltaAngle * i;
            float x = currentRadius * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
            float y = currentRadius * Mathf.Sin(currentAngle * Mathf.Deg2Rad);
            balls[i].transform.localPosition = new Vector3(x, y, 0);

        }
    }

    private void DestroyAllBall() {
        foreach (var ball in balls) {
            ball.SelfDestroy();
        }
    }

    private void OnBallDestroy(BallBullet myBall) {
        balls.Remove(myBall);
        if (HasOutOfBall()) {
            currentState = State.None;
            EndAttack();
        }
        else {
            foreach (var b in balls) {
                b.HideMoveTrail();
                b.ShowMoveTrail();
            }
        }

    }


    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }

    private enum State {
        MoveOut, MoveIn, Max, None
    }

    [System.Serializable]
    private class AttackData {
        [SerializeField] private float damageBallPercent;

        public float DamageBallPercent { get => damageBallPercent; }
    }
}
