using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;
using Gemmob;

public class AbilityItemView : View<AbilityData> {
    [SerializeField] private Image imgIcon;
    [SerializeField] private TextMeshProUGUI txtLevel;
    [SerializeField] private TextMeshProUGUI abilityName;
    [SerializeField] private Transform lockGraphic;
    [SerializeField] private ButtonBase btnSelect;
    [SerializeField] private GameObject unlockGraphic;
    [SerializeField] private Image frameSelect;
    [SerializeField] private Image whiteFrame;

    private Action<AbilityItemView> onSelect;
    private void Start() {
        btnSelect?.AddEvent(OnSelectButtonClicked);
    }
    public override void Show() {
        if (Model == null) {
            return;
        }
        string textLevel = Model.IsMaxLevel ? "MAX" : $"Lv.{Model.CurrentLevel + 1}";
        SetIcon(Model.Icon, Model.IsUnlocked);
        SetContentLevelText(textLevel, Model.IsUnlocked);
        SetAbilityName(Model.AbilityName, Model.IsUnlocked);
        SetStateLockGraphic(!Model.IsUnlocked);
        SetButtonStatus(Model.IsUnlocked);
    }

    public AbilityItemView AddOnSelect(Action<AbilityItemView> onSelect) {
        this.onSelect = onSelect;
        return this;
    }

    private void OnSelectButtonClicked() {
        onSelect?.Invoke(this);
    }

    public AbilityItemView SetIcon(Sprite icon, bool show) {
        if (imgIcon) {
            unlockGraphic.SetActive(show);
            if (show) {
                imgIcon.sprite = icon;
                imgIcon.SetNativeSize();
            }
        }
        return this;
    }

    public void SetContentLevelText(string content, bool show) {
        if (txtLevel) {
            txtLevel.gameObject.SetActive(show);
            if (show) {
                txtLevel.text = content;
            }
        }
    }
    public void SetAbilityName(string content, bool show) {
        if (abilityName) {
            abilityName.gameObject.SetActive(show);
            if (show) {
                abilityName.text = content;
            }
        }
    }

    public void SetStateLockGraphic(bool show) {
        if (lockGraphic) {
            lockGraphic.gameObject.SetActive(show);
        }
    }
    public void SetButtonStatus(bool interactable) {
        if (btnSelect) {
            btnSelect.interactable = interactable;
        }
    }

    public void SetStateSelectButton(bool interaction, bool show) {
        if (btnSelect) {
            btnSelect.gameObject.SetActive(show);
            if (show) {
                btnSelect.SetState(interaction);
            }
        }
    }
    public void InvokeOnSelect() {
        if (onSelect != null) {
            onSelect.Invoke(this);
        }
    }
    public System.Collections.IEnumerator PlayEffect(float deltaTime) {
        transform.DOKill(true);
        frameSelect.SetAlpha(1);
        frameSelect.transform.localScale = Vector3.one;
        frameSelect.gameObject.SetActive(true);
        transform.DOScale(1.1f, deltaTime).SetLoops(2, LoopType.Yoyo);
        yield return Yielder.Wait(deltaTime);
        frameSelect.gameObject.SetActive(false);
    }
    public void PlayChooseEffect(float deltaTime, Action<AbilityItemView> onComplete) {
        transform.DOKill(true);
        whiteFrame.gameObject.SetActive(true);
        whiteFrame.SetAlpha(1);
        whiteFrame.transform.DOScale(Vector3.one * 2, deltaTime).SetLoops(2, LoopType.Yoyo).OnComplete(() => {
            frameSelect.gameObject.SetActive(true);
            frameSelect.transform.DOScale(Vector3.one * 1.2f, deltaTime).SetUpdate(true).OnComplete(() => {
                whiteFrame.DOFade(0, deltaTime * 2).SetUpdate(true).OnComplete(() => {
                    onComplete?.Invoke(this);
                });
                frameSelect.DOFade(0, deltaTime).SetUpdate(true);

            });
        });
    }

}
