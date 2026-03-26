using DG.Tweening;
using GameSystem.Common.UI;
using Helper;
using UnityEngine;

public class ShipMove : CharacterMove {
    private int touchIndex = -1;
    private Vector3 targetPosition;
    private Vector3 oldMousePosition;
    private Vector2 inputPosition;

    private Camera cam;
    private Ray ray;
    private Plane xy;
    private float distance;
    private Transform Trans;


    [SerializeField] private float RangeMoveX = 3;
    [SerializeField] private float RangeMoveY = 5;
    [SerializeField] private RangeFloatValue stepMoveRange;
    [SerializeField] private RangeFloatValue stepMoveAutoRange;
    [SerializeField] private RangeFloatValue targetMoveOffsetRange;
    [SerializeField] private float distanceConfigSqr;
    [Header("Appear")]
    [SerializeField] private Vector2 appearPos = new Vector2(0, -5);
    [SerializeField] private float appearDurantion;
    [SerializeField] private AnimationCurve appearCurve;

    private bool isTouching;
    private bool lockTouch;
    protected bool isMove;
    private Tweener curTweener;
    private ShipBase shipBase;
    private bool forceTouchDown;

    public ShipBase ShipBase {
        get {
            if (shipBase == null) {
                shipBase = CharacterBase as ShipBase;
            }
            return shipBase;
        }
    }

    public bool IsShipMoving { get => isTouching || isMove; }
    public bool ForceTouchDown { get => forceTouchDown; set => forceTouchDown = value; }

    void Awake() {
        Trans = transform;
        cam = Camera.main;
        xy = new Plane(Vector3.forward, new Vector3(0, 0, 0));
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            touchIndex = 0;

        RangeMoveX = ConfigIngameData.borderW / 2;
        RangeMoveY = ConfigIngameData.borderH / 2;
    }

    private void OnDestroy() {
        if (curTweener != null) {
            curTweener.Kill();
        }
    }

    public void Revive() {
        first = false;
    }
    public void LockTouch(bool status) {
        lockTouch = status;
    }
    public override void Initialize() {
        base.Initialize();
        first = false;
    }
    public bool CanMoveControl() {
        if (lockTouch)
            return false;
        GetInput();
        return isTouching;
    }

    public bool HasMoveControlComplete() {
        GetInput();
        return !isTouching && !isMove;
    }

    private void GetInput() {
        bool a1 = Input.GetMouseButtonDown(0);
        UnityEngine.EventSystems.EventSystem v = UnityEngine.EventSystems.EventSystem.current;
        bool a2 = v != null && !v.IsPointerOverGameObject(touchIndex);
        if ((a1 || forceTouchDown) && a2) {
            oldMousePosition = GetMousePosition();
            if (!isTouching) {
                isTouching = true;
                isMove = true;
                //inputPosition = Input.mousePosition;
                if (!forceTouchDown) {
                    var mousePosition = GetMousePosition();
                    Vector3 offset = new Vector3(0, targetMoveOffsetRange.GetRatioValue(Mathf.Abs(mousePosition.y) / (ConfigIngameData.borderH * 0.5f)), 0);
                    //targetPosition = mousePosition + offset;
                    if (!PrefSaver.MoveFocus) {
                        targetPosition += mousePosition - oldMousePosition;
                    }
                    else
                        targetPosition = mousePosition + offset;
                }
                //EventDispatcher.Instance.Dispatch(EventKey.START_INPUT_TOUCH_DOWN);
            }
            if (forceTouchDown) {
                forceTouchDown = false;
            }
        }
        else if (Input.GetMouseButtonUp(0)) {
            if (isTouching) {
                isTouching = false;
                //EventDispatcher.Instance.Dispatch(EventKey.START_INPUT_TOUCH_UP);
            }
        }
    }
    private bool first;
    public void MoveControl() {
        if (HUDManager.Instance.enabled) {
            if (Input.GetMouseButton(0) && isTouching) {
                var mousePosition = GetMousePosition();
                Vector3 offset = new Vector3(0, targetMoveOffsetRange.GetRatioValue(Mathf.Abs(mousePosition.y) / (ConfigIngameData.borderH * 0.5f)), 0);
                //targetPosition = mousePosition + offset;
                if (!PrefSaver.MoveFocus)
                    targetPosition += mousePosition - oldMousePosition;
                else
                    targetPosition = mousePosition + offset;
                if (targetPosition.x > RangeMoveX) {
                    targetPosition.x = RangeMoveX;
                }
                else if (targetPosition.x < -RangeMoveX) {
                    targetPosition.x = -RangeMoveX;
                }

                if (targetPosition.y > RangeMoveY) {
                    targetPosition.y = RangeMoveY;
                }
                else if (targetPosition.y < -RangeMoveY) {
                    targetPosition.y = -RangeMoveY;
                }
                if (!first && !PrefSaver.MoveFocus) {
                    targetPosition = Trans.position;
                }
                first = true;
                float distanceSpr = Vector2.SqrMagnitude(targetPosition - Trans.position);
                float step = stepMoveRange.GetRatioValue(distanceSpr / distanceConfigSqr);
                Trans.position = Vector2.MoveTowards(Trans.position, targetPosition, step * Time.deltaTime);
                if (distanceSpr < step * step * Time.deltaTime * Time.deltaTime) {
                    isMove = false;
                }
                oldMousePosition = mousePosition;
            }
            else if (isMove && first) {
                //targetPosition = GetWorldPositionOnPlane(inputPosition) + targetMoveOffset;
                float distanceSpr = Vector2.SqrMagnitude(targetPosition - Trans.position);
                float step = stepMoveAutoRange.GetRatioValue(distanceSpr / distanceConfigSqr);
                Trans.position = Vector2.MoveTowards(Trans.position, targetPosition, step * Time.deltaTime);
                if (distanceSpr < step * step * Time.deltaTime * Time.deltaTime) {
                    isMove = false;
                }
            }
        }
    }

    Vector3 GetMousePosition() {
#if UNITY_EDITOR
        return GetWorldPositionOnPlane(Input.mousePosition);
#else
        if (Input.touchCount == 0)
            return oldMousePosition;
        else
            return GetWorldPositionOnPlane(Input.touches[0].position);
#endif
    }

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition) {
        ray = cam.ScreenPointToRay(screenPosition);
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    public void StartMoveReivive() {
        transform.position = new Vector3(0, -(ConfigIngameData.borderH / 2 + 5), 0);
        curTweener = transform.DOMove(appearPos, appearDurantion).SetEase(appearCurve).OnComplete(() => {
            isMove = true;
            if (ShipBase.ShipHealth.PlayerHPBar) {
                ShipBase.ShipHealth.PlayerHPBar.transform.position = ShipBase.ShipTopTrans.position;
                ShipBase.ShipHealth.PlayerHPBar.FadeToEnable();
                ShipBase.ShipHealthPoint.gameObject.SetActive(true);
            }
        });
    }

    public void StartMoveAppear() {
        transform.position = new Vector3(0, -(ConfigIngameData.borderH / 2 + 2), 0);
        curTweener = transform.DOMove(appearPos, appearDurantion).SetEase(appearCurve).OnComplete(() => { isMove = true; });
        DOVirtual.DelayedCall(2f, () => {
            AutoMove();
        }).SetAutoKill(true);
    }

    public bool CompleteMoveToTarget() {
        return isMove;
    }
    public void EndMoveAppear() {
        isMove = false;
        ShipBase.ShipAttack.ChangeStateShot(true);
        if (ShipBase.ShipHealth.PlayerHPBar) {
            ShipBase.ShipHealth.PlayerHPBar.transform.position = ShipBase.ShipTopTrans.position;
            ShipBase.ShipHealth.PlayerHPBar.FadeToEnable();
            ShipBase.ShipHealthPoint.gameObject.SetActive(true);
        }
        GameManager.Instance.LoadDrone();
        IngameHUD.Instance.Combat.SkillSystem.Initialize();
    }
    #region Tutorial
    public void TutorialMoveToTarget() {
        curTweener = transform.DOMove(appearPos, 1).SetEase(appearCurve);
    }
    #endregion
    public void AutoMove() {
        if (GameResources.Instance.AutoPlay) {
            var pos = BorderHelper.GetRandomPointBottomBorder(Random.Range(0.01f, 0.99f));
            transform.DOMove(pos, 0.5f).OnComplete(AutoMove);
        }
    }
}
