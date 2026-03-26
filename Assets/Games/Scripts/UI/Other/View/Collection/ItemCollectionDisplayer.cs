

using System.Collections.Generic;
using UnityEngine;

public class ItemCollectionDisplayer : CollectionDisplayer<IItemInstance> {
    [SerializeField] private ItemView prefab;
    [SerializeField] private Transform layout;

    protected readonly List<ItemView> displayers = new List<ItemView>();
    public override int DisplayerCount => displayers.Count;

    public ItemView GetDisplayer(int index) {
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

            ItemView displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(true);
                SetupDisplayer(displayer, GetItem(i));
            }
        }

        for (int i = Capacity; i < DisplayerCount; i++) {
            ItemView displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(false);
            }
        }
    }

    public ItemView GetItemView(IItemInstance item) {
        foreach (var displayer in displayers) {
            if (displayer.Model == item) {
                return displayer;
            }
        }
        return null;
    }

    public void SetupDisplayer(ItemView displayer, IItemInstance item) {
        if (displayer == null) {
            return;
        }
        displayer.SetModel(item).Show();
    }

    protected ItemView CreateDisplayer() {
        ItemView viewItem = Instantiate(prefab, layout);
        return viewItem;
    }
}
