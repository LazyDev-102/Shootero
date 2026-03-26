using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Gemmob;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DroneHeathBar : ProgressBarBase {
    private Transform target;
    private Transform targetFollow;
    private bool active;
    private TweenerCore<Color, Color, ColorOptions> tween;
    [SerializeField] private Color normalColor;

    private void Awake() {
        EventDispatcher.Instance.AddListener<EventKey.OnEnergyShieldHitDamage>(EnergyOnHPChanged);
    }
    private void OnDestroy() {
        EventDispatcher.Instance.RemoveListener<EventKey.OnEnergyShieldHitDamage>(EnergyOnHPChanged);
    }
    private void OnEnable() {
        active = true;
        FadeToEnable();
    }
    private void Update() {
        if (active) {
            FollowShip();
        }
        else if (target == null) {
            this.Recycle();
        }
    }

    public DroneHeathBar SetTarget(Transform target) {
        this.target = target;
        return this;
    }

    public DroneHeathBar SetFollowTarget(Transform targetFollow) {
        this.targetFollow = targetFollow;
        return this;
    }

    private void FollowShip() {
        transform.position = targetFollow.position;
    }


    public DroneHeathBar AddListenerHealthChanged(DroneBase player) {
        player.DroneHealth.AddOnHpChanged(HandlePlayerHealthChanged);
        return this;
    }

    public DroneHeathBar RemoveListenerHealthChanged(DroneBase player) {
        player.DroneHealth.RemoveOnHpChanged(HandlePlayerHealthChanged);
        return this;
    }

    private void HandlePlayerHealthChanged(int health, float pct) {
        //if (gameObject.activeInHierarchy)
        //    StartCoroutine(UpdateProgressHPBar(health, pct));
        HandleBarChanged(pct);
        OnHitDameEffect();
    }
    private void OnHitDameEffect() {
        try {
            tween?.Kill(false);
            imgCurrentValueLerp.color = Color.white;
            imgCurrentValueLerp.SetAlpha(0);
            tween = imgCurrentValueLerp.DOFade(1, 0.1f)
                                       .SetEase(Ease.OutBack)
                                       .OnComplete(() => {
                                           imgCurrentValueLerp.color = normalColor;
                                           imgCurrentValueLerp.SetAlpha(0);
                                           imgCurrentValueLerp.DOFade(1, 0.1f)
                                                              .SetEase(Ease.OutBack);
                                       });
        }
        catch {

        }
    }
    private IEnumerator UpdateProgressHPBar(int hp, float pct) {
        if (imgCurrentValueLerp.fillAmount < pct) {
            while (pct != 0 && imgCurrentValueLerp.fillAmount < pct) {
                imgCurrentValueLerp.fillAmount += Time.deltaTime;
                yield return null;
            }
        }
        else {
            while (pct != 0 && imgCurrentValueLerp.fillAmount > pct) {
                imgCurrentValueLerp.fillAmount -= Time.deltaTime;
                yield return null;
            }
        }
    }
    public void FadeToDisable() {
        imgCurrentValueLerp.DOFade(0, 1f)
                           .SetEase(Ease.Linear)
                           .SetUpdate(true);

        imgCurrentValueReal.DOFade(0, 1f)
                           .SetEase(Ease.Linear)
                           .SetUpdate(true);

        gameObject.GetComponent<Image>()
                  .DOFade(0, 1f)
                  .SetEase(Ease.Linear)
                  .SetUpdate(true)
                  .OnComplete(() => {
                      gameObject.SetActive(false);
                  });
    }
    public void FadeToEnable() {
        gameObject.SetActive(true);
        imgCurrentValueLerp.DOFade(1, 1f)
                           .SetEase(Ease.Linear)
                           .SetUpdate(true);

        imgCurrentValueReal.DOFade(0.3f, 1f)
                           .SetEase(Ease.Linear)
                           .SetUpdate(true);

        gameObject.GetComponent<Image>()
                  .DOFade(1, 1f)
                  .SetEase(Ease.Linear)
                  .SetUpdate(true);
    }

    public void SelfDestroy() {
        active = false;
        this.Recycle();
        StopAllCoroutines();
    }
    public DroneHeathBar FillFull() {
        imgCurrentValueReal.fillAmount = 1;
        imgCurrentValueLerp.fillAmount = 1;
        imgCurrentValueLerp.rectTransform.sizeDelta = Vector2.zero;
        return this;
    }


    #region Energy Shield HPBar
    private int energyMaxHP;
    private float energyMaxWidth;
    [SerializeField] private Image energyShieldProgressBG;
    [SerializeField] private Image energyShieldProgress;

    public void TurnOnEnergyHpBar(int maxHP) {
        energyMaxHP = maxHP;
        energyShieldProgressBG.gameObject.SetActive(true);
        energyShieldProgress.gameObject.SetActive(true);
        energyMaxWidth = energyShieldProgressBG.rectTransform.rect.width;
        //energyShieldProgressBG.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, energyMaxWidth);
        energyShieldProgress.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, energyMaxWidth);
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
        energyShieldProgress.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ratio * energyMaxWidth);
    }
    #endregion
}
