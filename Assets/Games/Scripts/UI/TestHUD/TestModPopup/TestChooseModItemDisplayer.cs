using System;
using TMPro;
using UnityEngine;

public class TestChooseModItemDisplayer : View<ModData> {
    [SerializeField] private TextMeshProUGUI txtId;
    [SerializeField] private ButtonBase btnSelect;

    private Action<TestChooseModItemDisplayer> onSelect;

    private void Start() {
        btnSelect.AddEvent(OnSelectButtonClicked);
    }
    public override void Show() {
        if (Model == null) {
            return;
        }
        btnSelect.SetState(Model.CanApplyTo(GameManager.Instance.GameLoader.Ship));
        txtId.text = $"{Model.NameMod}";
    }

    public TestChooseModItemDisplayer OnSelect(Action<TestChooseModItemDisplayer> onSelect) {
        this.onSelect = onSelect;
        return this;
    }

    private void OnSelectButtonClicked() {
        onSelect?.Invoke(this);
    }

}
