using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Gemmob;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : ProgressBarBase {
    #region Variables
    [SerializeField] private TextMeshProUGUI txtPlayerHealth;
    [SerializeField] private Transform barContainer;
    [SerializeField] private GameObject bar;
    [SerializeField] float hpWeakAlphaUnit = 0.2f;
    [SerializeField] float hpWeak = 0.2f;
    [SerializeField] Color normalColor;
    [SerializeField] Color invunerableColor;
    [SerializeField] LineRenderer line;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Transform posOrigin;
    [SerializeField] private GameObject regenerationMod;
    [SerializeField] private Image energyShieldProgressBG;
    [SerializeField] private Image energyShieldProgress;

    private bool hasWeak;
    private PlayerTakeHitEffect playerTakeHitEffect;
    private Vector2 sizeDeltaOrigin;
    private ShipBase ship;
    private Vector3 smoothedPosition;
    private bool active;
    private Transform target;
    [Header("Effect")]
    [SerializeField] private Image effect;
    [SerializeField] private GameObject highlight;
    private float offsetExpVirtualTime = 0.7f;
    private float oldPct = 1;
    private float onePiece = 0.05f;
    private TweenerCore<Color, Color, ColorOptions> tween;
    #endregion

    #region Init 
    private void Awake() {
        EventDispatcher.Instance.AddListener<EventKey.OnEnergyShieldHitDamage>(EnergyOnHPChanged);
    }
    private void OnDestroy() {
        EventDispatcher.Instance.RemoveListener<EventKey.OnEnergyShieldHitDamage>(EnergyOnHPChanged);
    }
    public void Assign(PlayerTakeHitEffect playerTakeHitEffect) {
        this.playerTakeHitEffect = playerTakeHitEffect;
    }
    protected override void Start() {
        base.Start();
        active = true;
        sizeDeltaOrigin = transform.rectTransform().sizeDelta;
        ship = GameManager.Instance.GameLoader.Ship;
    }
    public void SelfDestroy() {
        active = false;
        StopAllCoroutines();
    }
    public void SetTarget(Transform target) {
        this.target = target;
    }
    public void SetActive(bool status) {
        gameObject.SetActive(status);
        if (status) {
            transform.position = ship.ShipTopTrans.position;
        }
    }
    #endregion

    #region Follow Ship
    private void Update() {
        if (active) {
            if (ship.ShipMove.IsShipMoving) {
                FollowShip();
            }
        }
    }

    private void FollowShip() {
        smoothedPosition = Vector3.Lerp(transform.position, ship.ShipTopTrans.position, smoothSpeed);
        transform.position = smoothedPosition;
    }

    #endregion

    #region HPBar Action, Funcition
    public void AddListenerHealthChanged(ShipBase player) {
        player.ShipHealth.AddOnHpChanged(HandlePlayerHealthChanged);
    }

    public void RemoveListenerHealthChanged(ShipBase player) {
        player.ShipHealth.RemoveOnHpChanged(HandlePlayerHealthChanged);
    }

    private void HandlePlayerHealthChanged(int health, float pct) {
        txtPlayerHealth.text = health.ToString();
        HandleBarChanged(pct);
        OnHitDameEffect();
        if (!hasWeak && pct < hpWeak) {
            hasWeak = true;
            TurnOnHealthWeak();
        }
        else if (pct > hpWeak)
            TurnOffHealthWeak();
    }

    private void OnHitDameEffect() {
        tween?.Kill(false);
        imgCurrentValueLerp.color = Color.white;
        imgCurrentValueLerp.SetAlpha(0);
        tween = imgCurrentValueLerp.DOFade(1, 0.1f).SetEase(Ease.OutBack).OnComplete(() => {
            imgCurrentValueLerp.color = normalColor;
            imgCurrentValueLerp.SetAlpha(0);
            imgCurrentValueLerp.DOFade(1, 0.1f).SetEase(Ease.OutBack);
        });

    }
    private void PlayEffect(float pct) {
        highlight.SetActive(false);
        highlight.SetActive(true);
        int length = (int)((oldPct - pct) / onePiece);
        if (length < 1)
            length = 1;
        for (int i = 0; i < length; i++) {
            var exp = effect.Spawn(transform, effect.transform.position + i * Vector3.right * 0.1f);
            exp.gameObject.SetActive(true);
            exp.SetAlpha(1);
            exp.DOFade(0, offsetExpVirtualTime * 1.2f).SetEase(Ease.InQuint);
            exp.transform.DOMoveY(exp.transform.position.y - 1f, offsetExpVirtualTime - i * onePiece).SetEase(Ease.InQuint).OnComplete(() => {
                DOVirtual.DelayedCall(2f, () => exp.Recycle());
                highlight.SetActive(false);
            });
        }
    }
    public void SetContentShipHealText(string content, bool show) {
        if (txtPlayerHealth) {
            txtPlayerHealth.gameObject.SetActive(show);
            if (show) {
                txtPlayerHealth.text = content;
            }
        }
    }
    public void ShakeBody(float duration) {
        transform.DOShakePosition(duration, 10, 20);
    }
    private void TurnOnHealthWeak() {
        playerTakeHitEffect.ShowFade(0, hpWeakAlphaUnit);
    }
    private void TurnOffHealthWeak() {
        playerTakeHitEffect.StopShowFadeConfig();
    }

    public void ChangeColorState(float time) {
        imgCurrentValueReal.color = invunerableColor;
        DOVirtual.DelayedCall(time, () => imgCurrentValueReal.color = normalColor);
    }
    public void FadeToDisable() {
        imgCurrentValueLerp.DOFade(0, 1f).SetEase(Ease.Linear).SetUpdate(true);
        imgCurrentValueReal.DOFade(0, 1f).SetEase(Ease.Linear).SetUpdate(true);
        gameObject.GetComponent<Image>().DOFade(0, 1f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() => {
            gameObject.SetActive(false);
        });
    }
    public void FadeToEnable() {
        gameObject.SetActive(true);
        imgCurrentValueLerp.DOFade(1, 1f).SetEase(Ease.Linear).SetUpdate(true);
        imgCurrentValueReal.DOFade(0.3f, 1f).SetEase(Ease.Linear).SetUpdate(true);
        gameObject.GetComponent<Image>().DOFade(1, 1f).SetEase(Ease.Linear).SetUpdate(true);
    }

    [ContextMenu("Add Bar")]
    public void ChangeBars() {
        var sizeDelta = transform.rectTransform().sizeDelta;
        transform.rectTransform().sizeDelta = new Vector2(sizeDelta.x + sizeDeltaOrigin.x * .1f, sizeDelta.y);
        ChangeMaxWidth(sizeDeltaOrigin.x * .1f);
        var clone = bar.Spawn(barContainer);
        clone.transform.localScale = Vector3.one;
        clone.transform.localPosition = Vector3.zero;
        var length = barContainer.childCount;
        for (int i = 0; i < length; i++) {
            RectTransform rect = barContainer.GetChild(i).transform as RectTransform;
            var posX = (i + 1f) / (length + 1);
            rect.anchorMin = new Vector2(posX, 0);
            rect.anchorMax = new Vector2(posX, 1);
            rect.anchoredPosition = Vector2.zero;
        }
    }
    #endregion

    #region Mod Regeneraton
    public void TurnOnRegenerationMod(bool active) {
        regenerationMod.SetActive(active);
    }
    #endregion

    #region Energy Shield HPBar
    private int energyMaxHP;

    public void TurnOnEnergyHpBar(int maxHP) {
        energyMaxHP = maxHP;
        energyShieldProgressBG.gameObject.SetActive(true);
        energyShieldProgress.gameObject.SetActive(true);
        energyShieldProgressBG.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxWidth);
        energyShieldProgress.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxWidth);
    }
    public void TurnOffEnergyHpBar() {
        energyShieldProgressBG.gameObject.SetActive(false);
        energyShieldProgress.gameObject.SetActive(false);
    }
    private void EnergyOnHPChanged(EventKey.OnEnergyShieldHitDamage shieldInfor) {
        if (shieldInfor.Target != target)
            return;
        float ratio = (float)((float)shieldInfor.CurrentHP / (float)energyMaxHP);
        if (ratio > 1)
            ratio = 1;
        EnergyShieldHPBarFill(ratio);
    }
    private void EnergyShieldHPBarFill(float ratio) {
        energyShieldProgress.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ratio * maxWidth);
    }
    #endregion
}
