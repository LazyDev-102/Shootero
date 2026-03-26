using UnityEngine;
using System;
using System.Collections.Generic;

public class TestChooseMiniBossItemCollectionDisplayer : CollectionDisplayer<MinibossBase> {
    [SerializeField] private TestChooseMiniBossItemDisplayer prefab;
    [SerializeField] private Transform layout;

    private Action<TestChooseMiniBossItemDisplayer> onSelect;
    protected readonly List<TestChooseMiniBossItemDisplayer> displayers = new List<TestChooseMiniBossItemDisplayer>();
    public override int DisplayerCount => displayers.Count;

    public TestChooseMiniBossItemDisplayer GetDisplayer(int index) {
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

            TestChooseMiniBossItemDisplayer displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(true);
                SetupDisplayer(displayer, GetItem(i));
            }
        }

        for (int i = Capacity; i < DisplayerCount; i++) {
            TestChooseMiniBossItemDisplayer displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(false);
            }
        }
    }

    public TestChooseMiniBossItemDisplayer GetItemView(MinibossBase abilityData) {
        foreach (var displayer in displayers) {
            if (displayer.Model == abilityData) {
                return displayer;
            }
        }
        return null;
    }

    public void SetupDisplayer(TestChooseMiniBossItemDisplayer displayer, MinibossBase item) {
        if (displayer == null) {
            return;
        }
        displayer.OnSelect(onSelect).SetModel(item).Show();
    }

    protected TestChooseMiniBossItemDisplayer CreateDisplayer() {
        TestChooseMiniBossItemDisplayer viewItem = Instantiate(prefab, layout);
        return viewItem;
    }

    public TestChooseMiniBossItemCollectionDisplayer AddOnSelect(Action<TestChooseMiniBossItemDisplayer> onSelect) {
        this.onSelect = onSelect;
        return this;
    }

}
