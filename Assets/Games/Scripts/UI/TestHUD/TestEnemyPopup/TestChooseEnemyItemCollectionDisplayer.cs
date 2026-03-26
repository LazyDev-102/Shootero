
using UnityEngine;
using System;
using System.Collections.Generic;
public class TestChooseEnemyItemCollectionDisplayer : CollectionDisplayer<EnemyBase> {
    [SerializeField] private TestChooseEnemyItemDisplayer prefab;
    [SerializeField] private Transform layout;

    private Action<TestChooseEnemyItemDisplayer> onSelect;
    protected readonly List<TestChooseEnemyItemDisplayer> displayers = new List<TestChooseEnemyItemDisplayer>();
    public override int DisplayerCount => displayers.Count;

    public TestChooseEnemyItemDisplayer GetDisplayer(int index) {
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

            TestChooseEnemyItemDisplayer displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(true);
                SetupDisplayer(displayer, GetItem(i));
            }
        }

        for (int i = Capacity; i < DisplayerCount; i++) {
            TestChooseEnemyItemDisplayer displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(false);
            }
        }
    }

    public TestChooseEnemyItemDisplayer GetItemView(EnemyBase abilityData) {
        foreach (var displayer in displayers) {
            if (displayer.Model == abilityData) {
                return displayer;
            }
        }
        return null;
    }

    public void SetupDisplayer(TestChooseEnemyItemDisplayer displayer, EnemyBase item) {
        if (displayer == null) {
            return;
        }
        displayer.OnSelect(onSelect).SetModel(item).Show();
    }

    protected TestChooseEnemyItemDisplayer CreateDisplayer() {
        TestChooseEnemyItemDisplayer viewItem = Instantiate(prefab, layout);
        return viewItem;
    }

    public TestChooseEnemyItemCollectionDisplayer AddOnSelect(Action<TestChooseEnemyItemDisplayer> onSelect) {
        this.onSelect = onSelect;
        return this;
    }
}
