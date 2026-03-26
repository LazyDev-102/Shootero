using UnityEngine;
using System;
using System.Collections.Generic;

public class TestChooseBossItemCollectionDisplayer : CollectionDisplayer<BossBase> {
    [SerializeField] private TestChooseBossItemDisplayer prefab;
    [SerializeField] private Transform layout;

    private Action<TestChooseBossItemDisplayer> onSelect;
    protected readonly List<TestChooseBossItemDisplayer> displayers = new List<TestChooseBossItemDisplayer>();
    public override int DisplayerCount => displayers.Count;

    public TestChooseBossItemDisplayer GetDisplayer(int index) {
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

            TestChooseBossItemDisplayer displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(true);
                SetupDisplayer(displayer, GetItem(i));
            }
        }

        for (int i = Capacity; i < DisplayerCount; i++) {
            TestChooseBossItemDisplayer displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(false);
            }
        }
    }

    public TestChooseBossItemDisplayer GetItemView(BossBase abilityData) {
        foreach (var displayer in displayers) {
            if (displayer.Model == abilityData) {
                return displayer;
            }
        }
        return null;
    }

    public void SetupDisplayer(TestChooseBossItemDisplayer displayer, BossBase item) {
        if (displayer == null) {
            return;
        }
        displayer.OnSelect(onSelect).SetModel(item).Show();
    }

    protected TestChooseBossItemDisplayer CreateDisplayer() {
        TestChooseBossItemDisplayer viewItem = Instantiate(prefab, layout);
        return viewItem;
    }

    public TestChooseBossItemCollectionDisplayer AddOnSelect(Action<TestChooseBossItemDisplayer> onSelect) {
        this.onSelect = onSelect;
        return this;
    }

}
