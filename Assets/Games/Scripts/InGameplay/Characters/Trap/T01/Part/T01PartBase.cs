using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(T01PartHitbox))]
public class T01PartBase : ObjectBase {
    #region References
    private T01PartHitbox t01PartHitbox;
    public T01PartHitbox T01PartHitbox {
        get {
            if (t01PartHitbox == null) {
                t01PartHitbox = ObjectHitbox as T01PartHitbox;
            }
            return t01PartHitbox;
        }
    }

    private T01PartAttack t01PartAttack;
    public T01PartAttack T01PartAttack {
        get {
            if (t01PartAttack == null) {
                t01PartAttack = ObjectAttack as T01PartAttack;
            }
            return t01PartAttack;
        }
    }

    private T01PartStat t01PartStat;
    public T01PartStat T01PartStat {
        get {
            if (t01PartStat == null) {
                t01PartStat = ObjectStat as T01PartStat;
            }
            return t01PartStat;
        }
    }

    #endregion

    [SerializeField] private SpriteRenderer mySpriteRenderer;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private DOTweenAnimation whiteEffect;
    [SerializeField] private SpriteRenderer whiteSpriteRenderer;
    [SerializeField] private float deltaEffect;

    private Countdowner deltaCountdowner = new Countdowner();
    private T01Base myParent;
    public T01Base MyParent { get => myParent; }
    public void StartEffect(Sprite sprite = null) {
        if (whiteEffect != null) {
            whiteEffect.gameObject.SetActive(true);
            whiteEffect.DORestart();
        }
    }
    public override void Initialize() {
        isInitialized = true;
        mySpriteRenderer.sprite = Helper.RandomHelper.RandomInCollection(sprites);
        whiteSpriteRenderer.sprite = mySpriteRenderer.sprite;
        deltaCountdowner.StartCountdown(deltaEffect);
    }
    public void SetParent(T01Base parent) {
        this.myParent = parent;
    }
    public override void Updating() {
        deltaCountdowner.Countdowning(Time.deltaTime);
        if (deltaCountdowner.IsTimeOut()) {
            deltaCountdowner.StartCountdown(deltaEffect);
            StartEffect(mySpriteRenderer.sprite);
        }
    }
}

