

using Helper;
using UnityEngine;

public class ChestChip01Move : ChestMove {
    [SerializeField] protected float currentMoveSpeed;
    [SerializeField] private float speedRotateLook = 10f;
    [SerializeField] protected Area appearArea;

    protected Vector2 targetMove;
    protected Vector2 direction;
    protected Vector2 viewPointInArea;
    private bool hasInBound;

    private ChestChip01Base chestChip01Base;
    public ChestChip01Base ChestChip01Base {
        get {
            if (chestChip01Base == null) {
                chestChip01Base = ChestBase as ChestChip01Base;
            }
            return chestChip01Base;
        }
    }

    public virtual void StartMoveAppear() {
        Vector2 pointAppear = GetRandomInArea();
        currentMoveSpeed = ChestChip01Base.ChestChip01Stat.MoveSpeed.Value;
        targetMove = pointAppear;
        direction = (pointAppear - (Vector2)transform.position).normalized;
        MyRigi.MoveRotation(Vector2.SignedAngle(Vector2.up, direction));
    }

    public override void Initialize() {
        base.Initialize();
        hasInBound = false;
    }

    public override void Updating() {
        base.Updating();
    }
    protected virtual Vector2 GetRandomInArea() {
        viewPointInArea = BorderHelper.GetRandomViewPointInsideArea(appearArea);
        return BorderHelper.GetWorldPointInsideArea(viewPointInArea);
    }

    public virtual void MoveDirect() {
        MyRigi.MovePosition((Vector2)transform.position + direction * currentMoveSpeed * Time.deltaTime);
    }

    public virtual void MovePush() {
        MyRigi.velocity = direction * currentMoveSpeed;
    }

    public virtual void MoveFront() {
        MyRigi.MovePosition(transform.position + transform.up * currentMoveSpeed * Time.deltaTime);
    }

    public override bool HasOutBorder() {
        if (!hasInBound) {
            hasInBound = !BorderHelper.IsOutBound(MyRigi.position);
            return false;
        }
        return BorderHelper.IsOutBound(MyRigi.position);
    }

    public void SetDirectionMove(Vector2 dir) {
        this.direction = dir.normalized;
    }

    public virtual void LookDirection(Vector2 direction) {
        MyRigi.MoveRotation(Mathf.LerpAngle(MyRigi.rotation, Vector2.SignedAngle(Vector2.up, direction), Time.deltaTime * speedRotateLook));
    }

    public virtual void LookTarget(Vector2 target) {
        LookDirection(target - (Vector2)transform.position);
    }

    public bool CompleteMoveToTarget() {
        if (targetMove == null) {
            return false;
        }
        return Vector2.Distance(targetMove, MyRigi.position) < Time.deltaTime * currentMoveSpeed;
    }

    public virtual bool CanMoveAppear() {
        return true;
    }
}
