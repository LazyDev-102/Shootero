using UnityEngine;
using System;
using System.Collections.Generic;

public class TestChooseModItemCollectionDisplayer : CollectionDisplayer<ModData> {
    [SerializeField] private TestChooseModItemDisplayer prefab;
    [SerializeField] private Transform layout;

    private Action<TestChooseModItemDisplayer> onSelect;
    protected readonly List<TestChooseModItemDisplayer> displayers = new List<TestChooseModItemDisplayer>();
    public override int DisplayerCount => displayers.Count;

    public TestChooseModItemDisplayer GetDisplayer(int index) {
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

            TestChooseModItemDisplayer displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(true);
                SetupDisplayer(displayer, GetItem(i));
            }
        }

        for (int i = Capacity; i < DisplayerCount; i++) {
            TestChooseModItemDisplayer displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(false);
            }
        }
    }

    public TestChooseModItemDisplayer GetItemView(ModData abilityData) {
        foreach (var displayer in displayers) {
            if (displayer.Model == abilityData) {
                return displayer;
            }
        }
        return null;
    }

    public void SetupDisplayer(TestChooseModItemDisplayer displayer, ModData item) {
        if (displayer == null) {
            return;
        }
        displayer.OnSelect(onSelect).SetModel(item).Show();
    }

    protected TestChooseModItemDisplayer CreateDisplayer() {
        TestChooseModItemDisplayer viewItem = Instantiate(prefab, layout);
        return viewItem;
    }

    public TestChooseModItemCollectionDisplayer AddOnSelect(Action<TestChooseModItemDisplayer> onSelect) {
        this.onSelect = onSelect;
        return this;
    }

}
