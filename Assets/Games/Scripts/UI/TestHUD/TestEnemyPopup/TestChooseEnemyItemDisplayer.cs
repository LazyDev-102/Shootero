

using System;
using TMPro;
using UnityEngine;

public class TestChooseEnemyItemDisplayer : View<EnemyBase> {
    [SerializeField] private TextMeshProUGUI txtId;
    [SerializeField] private ButtonBase btnSelect;

    private Action<TestChooseEnemyItemDisplayer> onSelect;

    private void Start() {
        btnSelect.AddEvent(OnSelectButtonClicked);
    }
    public override void Show() {
        if (Model == null) {
            return;
        }
        txtId.text = $"{Model.gameObject.name}";
    }

    public TestChooseEnemyItemDisplayer OnSelect(Action<TestChooseEnemyItemDisplayer> onSelect) {
        this.onSelect = onSelect;
        return this;
    }

    private void OnSelectButtonClicked() {
        onSelect?.Invoke(this);
    }
}
