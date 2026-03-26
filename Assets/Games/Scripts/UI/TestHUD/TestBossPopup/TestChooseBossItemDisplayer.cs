using System;
using TMPro;
using UnityEngine;

public class TestChooseBossItemDisplayer : View<BossBase> {
    [SerializeField] private TextMeshProUGUI txtId;
    [SerializeField] private ButtonBase btnSelect;

    private Action<TestChooseBossItemDisplayer> onSelect;

    private void Start() {
        btnSelect.AddEvent(OnSelectButtonClicked);
    }
    public override void Show() {
        if (Model == null) {
            return;
        }
        txtId.text = $"{Model.BossIndex + 1}";
    }

    public TestChooseBossItemDisplayer OnSelect(Action<TestChooseBossItemDisplayer> onSelect) {
        this.onSelect = onSelect;
        return this;
    }

    private void OnSelectButtonClicked() {
        onSelect?.Invoke(this);
    }
}
