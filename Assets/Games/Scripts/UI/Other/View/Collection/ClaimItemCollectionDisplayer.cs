

using System.Collections.Generic;
using UnityEngine;

public class ClaimItemCollectionDisplayer : CollectionDisplayer<ItemClaim> {
    [SerializeField] private ClaimItemView prefab;
    [SerializeField] private Transform layout;

    protected readonly List<ClaimItemView> displayers = new List<ClaimItemView>();
    public override int DisplayerCount => displayers.Count;

    public ClaimItemView GetDisplayer(int index) {
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

            ClaimItemView displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(true);
                SetupDisplayer(displayer, GetItem(i));
            }
        }

        for (int i = Capacity; i < DisplayerCount; i++) {
            ClaimItemView displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(false);
            }
        }
    }

    public ClaimItemView GetClaimItemView(ItemClaim item) {
        foreach (var displayer in displayers) {
            if (displayer.Model == item) {
                return displayer;
            }
        }
        return null;
    }

    public void SetupDisplayer(ClaimItemView displayer, ItemClaim item) {
        if (displayer == null) {
            return;
        }
        displayer.SetModel(item).Show();
    }

    protected ClaimItemView CreateDisplayer() {
        ClaimItemView viewItem = Instantiate(prefab, layout);
        return viewItem;
    }
}