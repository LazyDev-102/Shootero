using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCollectionDisplayer : CollectionDisplayer<AbilityData> {
    [SerializeField] private AbilityItemView prefab;
    [SerializeField] private TrellisLayout layout;

    private Action<AbilityItemView> onSelect;
    protected readonly List<AbilityItemView> displayers = new List<AbilityItemView>();
    public override int DisplayerCount => displayers.Count;

    public AbilityItemView GetDisplayer(int index) {
        if (index < 0 || index >= DisplayerCount) {
            return null;
        }
        return displayers[index];
    }

    public override void Show() {
        for (int i = 0; i < Capacity; i++) {
            if (DisplayerCount == i) {
                displayers.Add(CreateDisplayer());
            }

            AbilityItemView displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(true);
                SetupDisplayer(displayer, GetItem(i));
            }
        }

        for (int i = Capacity; i < DisplayerCount; i++) {
            AbilityItemView displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(false);
            }
        }
    }

    public AbilityItemView GetItemView(AbilityData abilityData) {
        foreach (var displayer in displayers) {
            if (displayer.Model == abilityData) {
                return displayer;
            }
        }
        return null;
    }

    public void SetupDisplayer(AbilityItemView displayer, AbilityData item) {
        if (displayer == null) {
            return;
        }
        displayer.AddOnSelect(onSelect).SetModel(item).Show();
    }

    protected AbilityItemView CreateDisplayer() {
        AbilityItemView viewItem = Instantiate(prefab, null);
        layout.AddItem(viewItem.transform);
        return viewItem;
    }

    public AbilityCollectionDisplayer AddOnSelect(Action<AbilityItemView> onSelect) {
        this.onSelect = onSelect;
        return this;
    }


}
