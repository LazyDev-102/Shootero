using System;
using TMPro;
using UnityEngine;

public class TestChooseMiniBossItemDisplayer : View<MinibossBase> {
    [SerializeField] private TextMeshProUGUI txtId;
    [SerializeField] private ButtonBase btnSelect;

    private Action<TestChooseMiniBossItemDisplayer> onSelect;

    private void Start() {
        btnSelect.AddEvent(OnSelectButtonClicked);
    }
    public override void Show() {
        if (Model == null) {
            return;
        }
        txtId.text = $"{Model.MinibossIndex + 1}";
    }

    public TestChooseMiniBossItemDisplayer OnSelect(Action<TestChooseMiniBossItemDisplayer> onSelect) {
        this.onSelect = onSelect;
        return this;
    }

    private void OnSelectButtonClicked() {
        onSelect?.Invoke(this);
    }
}
