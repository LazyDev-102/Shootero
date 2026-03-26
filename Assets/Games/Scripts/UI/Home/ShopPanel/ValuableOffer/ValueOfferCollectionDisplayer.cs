

using System.Collections.Generic;
using UnityEngine;

public class ValueOfferCollectionDisplayer : CollectionDisplayer<PackItem> {
    [SerializeField] private ValueOfferItemDisplayer prefab;
    [SerializeField] private Transform layout;

    protected readonly List<ValueOfferItemDisplayer> displayers = new List<ValueOfferItemDisplayer>();
    public override int DisplayerCount => displayers.Count;

    public ValueOfferItemDisplayer GetDisplayer(int index) {
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

            ValueOfferItemDisplayer displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(true);
                SetupDisplayer(displayer, GetItem(i));
            }
        }

        for (int i = Capacity; i < DisplayerCount; i++) {
            ValueOfferItemDisplayer displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(false);
            }
        }
    }

    public ValueOfferItemDisplayer GetItemView(PackItem item) {
        foreach (var displayer in displayers) {
            if (displayer.Model == item) {
                return displayer;
            }
        }
        return null;
    }

    public void SetupDisplayer(ValueOfferItemDisplayer displayer, PackItem item) {
        if (displayer == null) {
            return;
        }
        displayer.SetModel(item).Show();
    }

    protected ValueOfferItemDisplayer CreateDisplayer() {
        ValueOfferItemDisplayer viewItem = Instantiate(prefab, layout);
        return viewItem;
    }
}
