using UnityEngine;

public class MiniShieldMove : CharacterMove {

    private MiniShieldBase miniShieldBase;

    public MiniShieldBase MiniShieldBase {
        get {
            if (miniShieldBase == null) {
                miniShieldBase = CharacterBase as MiniShieldBase;
            }
            return miniShieldBase;
        }
    }

    [SerializeField] private float lengthMove;
    [SerializeField] protected float inMoveSpeed = 5;
    [SerializeField] protected float outMoveSpeed = 5;

    [SerializeField] protected float currentMoveSpeed;

    private Vector2 inPosition;
    private Vector2 outPosition;

    protected Vector2 targetMove;
    protected Vector2 direction;

    private Transform myTransform;

    private void Awake() {
        myTransform = transform;
    }

    public override void Initialize() {
        base.Initialize();
        inPosition = myTransform.localPosition;
        outPosition = myTransform.localPosition + myTransform.up * lengthMove;
    }

    public void SpawnInPosition() {
        myTransform.localPosition = inPosition;
    }

    public void StartMoveIn() {
        currentMoveSpeed = inMoveSpeed;
        targetMove = inPosition;
        direction = (targetMove - (Vector2)myTransform.localPosition).normalized;
    }

    public void StartMoveOut() {
        //MiniShieldBase.LightningLine.SetActive(true);
        currentMoveSpeed = outMoveSpeed;
        targetMove = outPosition;
        direction = (targetMove - (Vector2)myTransform.localPosition).normalized;
    }

    public virtual void MoveDirect() {
        MiniShieldBase.LightningLine.UpdatePosition(myTransform.parent.position, myTransform.position);
        MiniShieldBase.LightningLine.SetActive(true);
        // MyRigi.MovePosition(MyRigi.position + direction * currentMoveSpeed * Time.deltaTime);
        myTransform.localPosition += (Vector3)direction * currentMoveSpeed * Time.deltaTime;
    }

    public bool CompleteMoveToTarget() {
        if (targetMove == null) {
            return false;
        }
        return Vector2.Distance(targetMove, myTransform.localPosition) < 0.3f;//Time.deltaTime * currentMoveSpeed;
    }
}
