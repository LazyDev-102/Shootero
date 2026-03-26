using System;
using TMPro;
using UnityEngine;

public class InfinityInputInfo : MonoBehaviour {
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI maxCharacterText;
    [SerializeField] private ButtonExplorer confirmButton;
    [SerializeField] private ButtonExplorer closeButton;
    private Action onClose;
    private void Awake() {
        confirmButton.AddEvent(OnConfirm);
        closeButton.AddEvent(OnClose);
    }
    private void OnEnable() {
        closeButton.gameObject.SetActive(GameResources.Instance.UserProfile.GetIngameName() != "");
        HeadHUD.Instance.Hide<HeadPanel>();
    }
    private void OnDisable() {
        HeadHUD.Instance.Show<HeadPanel>();
    }
    private void OnConfirm() {
        if (inputField.text.Length > 12 || inputField.text.Length == 0) {
            maxCharacterText.color = Color.red;
            maxCharacterText.alpha = 1;
            return;
        }
        GameResources.Instance.UserProfile.SetMyInfo(new UserProfileInfo() { PlayerName = inputField.text.Trim() });
        gameObject.SetActive(false);
        onClose?.Invoke();
        onClose = null;
    }
    public void AddOnClose(Action onClose) {
        this.onClose = onClose;
    }
    private void OnClose() {
        gameObject.SetActive(false);
    }
}
