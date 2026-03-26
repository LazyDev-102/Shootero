using UnityEngine;
using System;
using TMPro;
using GameSystem.Common.UI;

public class ConfirmPopup : DOTweenFrame {
    protected Action successAction, failAction;

    [SerializeField] private ButtonExplorer confirmButton, cancelButton, closeButton;
    [SerializeField] private TextMeshProUGUI titleText, contentText, confirmText, cancelText;

    private bool hideOnClickYes = true;
    private bool hideOnClickNo = true;

    protected void Start() {
        confirmButton.AddEvent(OnButtonYesClicked);
        cancelButton.AddEvent(OnButtonNoClicked);
        closeButton.AddEvent(OnClose);
    }
    public void Show() {
        gameObject.SetActive(true);
    }

    public void OnClose() {
        Hide();
        gameObject.SetActive(false);
    }

    public ConfirmPopup Init(Action successAction, Action failAction, string title = "", string content = "", string btnConfirmTitle = "", string btnCancelTitle = "", bool hideOnYes = true, bool hideOnNo = true, bool btnClose = true) {
        confirmText.text = btnConfirmTitle;
        cancelText.text = btnCancelTitle;
        confirmButton.gameObject.SetActive(!string.IsNullOrEmpty(btnConfirmTitle));
        cancelButton.gameObject.SetActive(!string.IsNullOrEmpty(btnCancelTitle));
        titleText.gameObject.SetActive(!string.IsNullOrEmpty(title));
        contentText.text = content;
        titleText.text = title;

        hideOnClickYes = hideOnYes;
        hideOnClickNo = hideOnNo;
        this.closeButton.gameObject.SetActive(btnClose);
        this.successAction = successAction;
        this.failAction = failAction;
        return this;
    }

    private void OnButtonYesClicked() {
        successAction?.Invoke();
        if (hideOnClickYes) {
            OnClose();
        }
    }

    private void OnButtonNoClicked() {
        failAction?.Invoke();
        if (hideOnClickNo)
            OnClose();
    }
}
