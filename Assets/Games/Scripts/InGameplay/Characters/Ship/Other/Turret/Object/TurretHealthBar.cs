using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Gemmob;
using Helper;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretHealthBar : ProgressBarBase {
    private Transform target;
    private bool active;

    private void Awake() {
        EventDispatcher.Instance.AddListener<EventKey.OnEnergyShieldHitDamage>(EnergyOnHPChanged);
    }
    private void OnDestroy() {
        EventDispatcher.Instance.RemoveListener<EventKey.OnEnergyShieldHitDamage>(EnergyOnHPChanged);
    }
    private void OnEnable() {
        active = true;
    }
    private void Update() {
        if (active) {
            FollowShip();
        }
    }

    public void SetTarget(Transform target) {
        this.target = target;
    }

    private void FollowShip() {
        transform.position = target.position;
    }


    public void AddListenerHealthChanged(TurretBase player) {
        player.TurretHealth.AddOnHpChanged(HandlePlayerHealthChanged);
    }

    public void RemoveListenerHealthChanged(TurretBase player) {
        player.TurretHealth.RemoveOnHpChanged(HandlePlayerHealthChanged);
    }

    private void HandlePlayerHealthChanged(int health, float pct) {
        UpdateProgressHPBar(pct);
        //if (gameObject.activeInHierarchy)
        //    StartCoroutine(UpdateProgressHPBar(health, pct));
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
    private void UpdateProgressHPBar(float pct) {
        imgCurrentValueLerp.fillAmount = pct;
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

    public void SelfDestroy() {
        active = false;
        StopAllCoroutines();
    }
    public void FillFull() {
        imgCurrentValueReal.fillAmount = 1;
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
