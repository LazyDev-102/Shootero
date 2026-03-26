using UnityEngine;

public class ChipDropController : BaseDropController {
    [SerializeField] private int numberChip;
    [SerializeField] private int score = 1;
    [Header("Move Target")]
    [SerializeField] private RangeFloatValue speedRange;
    [SerializeField] private float speedBase;
    [SerializeField] private float delayMoveToTarget;
    [Header("Rotate")]
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private RangeFloatValue appearRotateSpeedRange;
    [SerializeField] private RangeFloatValue appearRotateAcclerRange;
    [SerializeField] private RangeFloatValue appearRotateLimitRange;
    [SerializeField] private RangeFloatValue moveTargetRotateSpeedRange;
    [SerializeField] private RangeFloatValue moveTargetRotateAccelerRange;


    protected bool moveToTarget;
    protected ShipBase target;
    protected Countdowner delayToTargetCountdowner;

    private float appearRotateSpeed;
    private float appearRotateAcceler;
    private float moveTargetRotateSpeed;
    private float moveTargetRotateAcceler;

    public override void Initalize() {
        base.Initalize();
        moveToTarget = false;
        delayToTargetCountdowner.StartCountdown(delayMoveToTarget);
        appearRotateSpeed = appearRotateSpeedRange.GetRandomValue();
        appearRotateAcceler = appearRotateAcclerRange.GetRandomValue();
        moveTargetRotateSpeed = moveTargetRotateSpeedRange.GetRandomValue();
        moveTargetRotateAcceler = moveTargetRotateAccelerRange.GetRandomValue();
    }

    public void SetChip(int chip) {
        numberChip = chip;
    }
    public override void AddToShip(ShipBase ship) {
        if (isApplied) {
            return;
        }
        isApplied = true;
        AddExp(ship);
        if (!GameManager.Instance.IsTrial) {
            ship.AddChip(numberChip);
            GameResources.Instance.Inventory.Add(ConstantItemID.ChipId, numberChip);
            GameManager.Instance.GameController.AddScore(score);
            GameManager.Instance.AddClaimedItem(ConstantItemID.ChipId, numberChip);
            TextShowupManager.Instance.ShowAddChipText($"+{numberChip}", ship.transform.position);
        }
        SoundManager.Instance.PlayChipTake();
        GameManager.Instance.SetDropStatus(false);
        Destroy();
    }
    private void AddExp(ShipBase ship) {
        if (ship == null)
            return;
        if (GameManager.Instance.GameMode == GameMode.Conqueror) {
            ConquerorController controller = GameManager.Instance.GetGameController<ConquerorController>();
            ConquerorWaveInfo waveInfo = controller.CurrentWaveInfo;
            if (waveInfo.CWaveType != WaveType.Bonus)
                ship.ShipLevel.AddExp(numberChip);
        }
        else {
            ship.ShipLevel.AddExp(numberChip);
        }

    }
    private bool CanMoveToTarget() {
        return moveToTarget && target != null && !target.IsDie();
    }

    public virtual void StartMoveToPlayer() {
        target = GameManager.Instance.GameLoader.Ship;
        moveToTarget = true;
    }

    public virtual void StopMoveToPlayer() {
        moveToTarget = false;
    }
    protected override void Update() {

        base.Update();
        if (delayToTargetCountdowner.IsCountdowning()) {
            delayToTargetCountdowner.Countdowning(Time.deltaTime);
            if (delayToTargetCountdowner.IsTimeOut()) {
                StartMoveToPlayer();
            }
        }
        Vector2 newPosition = myTransform.position;

        if (CanMoveToTarget()) {
            float v = speedBase * 1 / Mathf.Pow(Vector2.Distance(target.transform.position, myTransform.position), 2);
            v = Mathf.Clamp(v, speedRange.startValue, speedRange.endValue);
            newPosition = Vector2.MoveTowards(newPosition, target.transform.position, v * Time.deltaTime);

            sprite.transform.Rotate(Vector3.back, moveTargetRotateSpeed * Time.deltaTime);
            moveTargetRotateSpeed += moveTargetRotateAcceler * Time.deltaTime;
        }
        else {
            sprite.transform.Rotate(Vector3.back, appearRotateSpeed * Time.deltaTime);
            appearRotateSpeed += appearRotateAcceler * Time.deltaTime;
            appearRotateSpeed = Mathf.Clamp(appearRotateSpeed, appearRotateLimitRange.startValue, appearRotateLimitRange.endValue);
        }
        myTransform.position = newPosition;
    }

}
