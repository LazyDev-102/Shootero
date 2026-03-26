

using System.Collections.Generic;
using UnityEngine;

public class ChipPackItemCollectionDisplayer : CollectionDisplayer<ChipPackItem> {
    [SerializeField] private ChipPackItemDisplayer prefab;
    [SerializeField] private Transform layout;

    protected readonly List<ChipPackItemDisplayer> displayers = new List<ChipPackItemDisplayer>();
    public override int DisplayerCount => displayers.Count;

    public ChipPackItemDisplayer GetDisplayer(int index) {
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

            ChipPackItemDisplayer displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(true);
                SetupDisplayer(displayer, GetItem(i));
            }
        }

        for (int i = Capacity; i < DisplayerCount; i++) {
            ChipPackItemDisplayer displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(false);
            }
        }
    }

    public ChipPackItemDisplayer GetItemView(ChipPackItem abilityData) {
        foreach (var displayer in displayers) {
            if (displayer.Model == abilityData) {
                return displayer;
            }
        }
        return null;
    }

    public void SetupDisplayer(ChipPackItemDisplayer displayer, ChipPackItem item) {
        if (displayer == null) {
            return;
        }
        displayer.SetModel(item).Show();
    }

    protected ChipPackItemDisplayer CreateDisplayer() {
        ChipPackItemDisplayer viewItem = Instantiate(prefab, layout);
        return viewItem;
    }
}
