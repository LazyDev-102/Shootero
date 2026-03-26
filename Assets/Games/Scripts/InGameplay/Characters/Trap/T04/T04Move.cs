

using Helper;
using UnityEngine;

public class T04Move : TrapMove {
    [SerializeField] protected float currentMoveSpeed;
    [SerializeField] private float appearMoveSpeed;
    [SerializeField] protected Area appearArea;
    [Header("Warning")]
    [SerializeField] private LayerMask warningLayer;
    [SerializeField] private TrapWarning warningObj;

    protected Vector2 targetMove;
    protected Vector2 direction;
    protected Vector2 viewPointInArea;
    private bool hasInBound;
    private RaycastHit2D raycastHit;
    private Vector2 curPosition;
    private Transform target;
    private bool isTutorial;

    private T04Base t04Base;
    public T04Base T04Base {
        get {
            if (t04Base == null) {
                t04Base = TrapBase as T04Base;
            }
            return t04Base;
        }
    }

    public override void Initialize() {
        base.Initialize();
        hasInBound = false;
        target = GameManager.Instance.GameLoader.Ship.transform;
        isTutorial = GameResources.Instance.ConquerorData.IsTut;
    }

    private void StartMoveAppearTutorial() {
        transform.position = Vector2.up * 20;
        Vector2 pointAppear = Vector2.up * -20;
        currentMoveSpeed = 5;
        targetMove = pointAppear;
        direction = (pointAppear - (Vector2)transform.position).normalized;
        MyRigi.MoveRotation(Vector2.SignedAngle(Vector2.up, direction));
    }
    public override void Updating() {
        base.Updating();
        //if (isTutorial && target.gameObject.activeInHierarchy) {
        //    if (target.gameObject.activeInHierarchy) {
        //        transform.position = Vector3.Lerp(transform.position, target.position, 0.01f);
        //    }
        //    else {
        //        MoveFront();
        //    }
        //}
        curPosition = transform.position;
        if (!CameraHelper.ObjectInsideCameraView(curPosition)) {
            raycastHit = Physics2D.Raycast(curPosition, transform.up, 100, warningLayer);
            if (raycastHit) {
                warningObj.gameObject.SetActive(true);
                warningObj.Updating(raycastHit.point, raycastHit.normal, raycastHit.distance);
            }
            else {
                warningObj.gameObject.SetActive(false);
            }
        }
        else {
            warningObj.gameObject.SetActive(false);
        }
    }

    public virtual void StartMoveAppear() {
        if (GameResources.Instance.ConquerorData.IsTut) {
            StartMoveAppearTutorial();
        }
        else {
            StartMoveAppearNormal();
        }
    }
    private void StartMoveAppearNormal() {
        Vector2 pointAppear = GetRandomInArea();
        currentMoveSpeed = appearMoveSpeed;
        targetMove = pointAppear;
        direction = (pointAppear - MyRigi.position).normalized;
        MyRigi.MoveRotation(Vector2.SignedAngle(Vector2.up, direction));
    }
    protected virtual Vector2 GetRandomInArea() {
        viewPointInArea = BorderHelper.GetRandomViewPointInsideArea(appearArea);
        return BorderHelper.GetWorldPointInsideArea(viewPointInArea);
    }

    public virtual void MoveDirect() {
        MyRigi.MovePosition(MyRigi.position + direction * currentMoveSpeed * Time.deltaTime);
    }

    public virtual void MovePush() {
        MyRigi.velocity = direction * currentMoveSpeed;
    }

    public virtual void MoveFront() {
        MyRigi.MovePosition(MyRigi.position + (Vector2)transform.up * currentMoveSpeed * Time.deltaTime);
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
