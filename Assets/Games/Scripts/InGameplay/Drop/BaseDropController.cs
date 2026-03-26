using UnityEngine;
using Gemmob;

public abstract class BaseDropController : MonoBehaviour {
    [SerializeField] protected RangeFloatValue appearSpeedRange;
    [SerializeField] protected AnimationCurve appearCuver;
    [SerializeField] protected float appearDuration;



    protected float appearSpeed;
    protected Vector2 appearDirection;
    protected Countdowner appearCountdowner;
    protected Transform myTransform;
    protected bool isAppearCompleted;
    protected bool isApplied;

    private void OnEnable() {
        Initalize();
    }

    private void Awake() {
        myTransform = transform;
    }
    public virtual void Initalize() {
        isAppearCompleted = false;
        isApplied = false;
        appearSpeed = appearSpeedRange.GetRandomValue();
        appearDirection = Random.insideUnitCircle;
        appearDirection = appearDirection.normalized;
        appearCountdowner.StartCountdown(appearDuration);
    }

    public virtual void Destroy() {
        GameManager.Instance.GameLoader.DespawnDropItem(this);
    }


    public abstract void AddToShip(ShipBase ship);


    protected virtual void Update() {
        Vector2 newPosition = myTransform.position;
        if (appearCountdowner.IsCountdowning()) {
            float t = 1 - appearCountdowner.Countdown / appearDuration;
            appearSpeed = appearSpeedRange.GetRatioValue(appearCuver.Evaluate(t));
            newPosition += appearDirection * appearSpeed * Time.deltaTime;
            appearCountdowner.Countdowning(Time.deltaTime);
            if (appearCountdowner.IsTimeOut()) {
                OnAppearCompleted();
            }
        }
        myTransform.position = newPosition;
    }

    protected virtual void OnAppearCompleted() {
        isAppearCompleted = true;
    }
}
