using UnityEngine;

public class MoveFocusGroup : MonoBehaviour {
    [SerializeField] private ButtonBase focusButton;
    [SerializeField] private ButtonBase unfocusButton;
    [SerializeField] private GameObject focusSelect;
    [SerializeField] private GameObject unfocusSelect;
    [SerializeField] private GameObject focusCover;
    [SerializeField] private GameObject unfocusCover;

    private void Awake() {
        focusButton.AddEvent(TurnOnFocus);
        unfocusButton.AddEvent(TurnOffFocus);
        ChangeFocusUI(PrefSaver.MoveFocus);
    }

    private void TurnOnFocus() {
        PrefSaver.MoveFocus = true;
        ChangeFocusUI(true);
    }
    private void TurnOffFocus() {
        PrefSaver.MoveFocus = false;
        ChangeFocusUI(false);
    }
    private void ChangeFocusUI(bool status) {
        focusCover.SetActive(!status);
        focusSelect.SetActive(status);
        unfocusCover.SetActive(status);
        unfocusSelect.SetActive(!status);
    }

}
