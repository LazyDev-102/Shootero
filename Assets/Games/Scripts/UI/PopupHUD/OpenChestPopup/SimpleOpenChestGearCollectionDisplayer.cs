

using System.Collections.Generic;
using UnityEngine;

public class SimpleOpenChestGearCollectionDisplayer : CollectionDisplayer<GearSoftData> {
    [SerializeField] private SimpleOpenChestGearDisplayer prefab;
    [SerializeField] private Transform layout;

    protected readonly List<SimpleOpenChestGearDisplayer> displayers = new List<SimpleOpenChestGearDisplayer>();
    public override int DisplayerCount => displayers.Count;

    public SimpleOpenChestGearDisplayer GetDisplayer(int index) {
        if (index < 0 || index >= DisplayerCount) {
            return null;
        }
        return displayers[index];
    }

    public override void Show() {
        //for (int i = 0; i < Capacity; i++) {
        //    if (DisplayerCount == i) {
        //        displayers.Add(CreateDisplayer());
        //    }

        //    SimpleOpenChestGearDisplayer displayer = GetDisplayer(i);
        //    if (displayer) {
        //        SetupDisplayer(displayer, GetItem(i));
        //        displayer.gameObject.SetActive(true);
        //    }
        //}

        //for (int i = Capacity; i < DisplayerCount; i++) {
        //    SimpleOpenChestGearDisplayer displayer = GetDisplayer(i);
        //    if (displayer) {
        //        displayer.gameObject.SetActive(false);
        //    }
        //}
    }

    public void Show(bool showAllEffect) {
        for (int i = 0; i < Capacity; i++) {
            if (DisplayerCount == i) {
                displayers.Add(CreateDisplayer());
            }

            SimpleOpenChestGearDisplayer displayer = GetDisplayer(i);
            if (displayer) {
                if (showAllEffect || i == Capacity - 1) {
                    SetupDisplayer(displayer, GetItem(i), true);
                }
                else {
                    SetupDisplayer(displayer, GetItem(i), false);
                }
                displayer.gameObject.SetActive(true);
            }
        }

        for (int i = Capacity; i < DisplayerCount; i++) {
            SimpleOpenChestGearDisplayer displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(false);
            }
        }
    }

    public SimpleOpenChestGearDisplayer GetClaimItemView(GearSoftData item) {
        foreach (var displayer in displayers) {
            if (displayer.Model == item) {
                return displayer;
            }
        }
        return null;
    }

    public void SetupDisplayer(SimpleOpenChestGearDisplayer displayer, GearSoftData item, bool showEffect) {
        if (displayer == null) {
            return;
        }
        displayer.SetShowEffect(showEffect)
                 .SetShowFrameEffect(item.CurrentRank >= (int)Rarety.Elite)
                 .SetModel(item)
                 .Show();
    }

    protected SimpleOpenChestGearDisplayer CreateDisplayer() {
        bool activePrefab = prefab.gameObject.activeSelf;
        prefab.gameObject.SetActive(false);
        SimpleOpenChestGearDisplayer viewItem = Instantiate(prefab, layout);
        prefab.gameObject.SetActive(activePrefab);
        return viewItem;
    }
}