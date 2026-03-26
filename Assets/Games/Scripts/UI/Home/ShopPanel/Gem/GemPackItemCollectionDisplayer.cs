

using System.Collections.Generic;
using UnityEngine;

public class GemPackItemCollectionDisplayer : CollectionDisplayer<GemPackItem> {
    [SerializeField] private GemPackItemDisplayer prefab;
    [SerializeField] private Transform layout;

    protected readonly List<GemPackItemDisplayer> displayers = new List<GemPackItemDisplayer>();
    public override int DisplayerCount => displayers.Count;

    public GemPackItemDisplayer GetDisplayer(int index) {
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

            GemPackItemDisplayer displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(true);
                SetupDisplayer(displayer, GetItem(i));
            }
        }

        for (int i = Capacity; i < DisplayerCount; i++) {
            GemPackItemDisplayer displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(false);
            }
        }
    }

    public GemPackItemDisplayer GetItemView(GemPackItem abilityData) {
        foreach (var displayer in displayers) {
            if (displayer.Model == abilityData) {
                return displayer;
            }
        }
        return null;
    }

    public void SetupDisplayer(GemPackItemDisplayer displayer, GemPackItem item) {
        if (displayer == null) {
            return;
        }
        displayer.SetModel(item).Show();
    }

    protected GemPackItemDisplayer CreateDisplayer() {
        GemPackItemDisplayer viewItem = Instantiate(prefab, layout);
        return viewItem;
    }
}
