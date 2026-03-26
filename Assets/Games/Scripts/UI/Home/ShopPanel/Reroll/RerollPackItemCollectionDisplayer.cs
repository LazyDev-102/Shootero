

using System.Collections.Generic;
using UnityEngine;

public class RerollPackItemCollectionDisplayer : CollectionDisplayer<RerollPackItem> {
    [SerializeField] private RerollPackItemDisplayer prefab;
    [SerializeField] private Transform layout;

    protected readonly List<RerollPackItemDisplayer> displayers = new List<RerollPackItemDisplayer>();
    public override int DisplayerCount => displayers.Count;

    public RerollPackItemDisplayer GetDisplayer(int index) {
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

            RerollPackItemDisplayer displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(true);
                SetupDisplayer(displayer, GetItem(i));
            }
        }

        for (int i = Capacity; i < DisplayerCount; i++) {
            RerollPackItemDisplayer displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(false);
            }
        }
    }

    public RerollPackItemDisplayer GetItemView(RerollPackItem abilityData) {
        foreach (var displayer in displayers) {
            if (displayer.Model == abilityData) {
                return displayer;
            }
        }
        return null;
    }

    public void SetupDisplayer(RerollPackItemDisplayer displayer, RerollPackItem item) {
        if (displayer == null) {
            return;
        }
        displayer.SetModel(item).Show();
    }

    protected RerollPackItemDisplayer CreateDisplayer() {
        RerollPackItemDisplayer viewItem = Instantiate(prefab, layout);
        return viewItem;
    }
}
