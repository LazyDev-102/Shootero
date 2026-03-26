using Gemmob.Tutorial;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChestItemCollectionDisplayer : CollectionDisplayer<ChestItem> {
    [SerializeField] private ChestItemDisplayer prefab;
    [SerializeField] private Transform layout;

    protected readonly List<ChestItemDisplayer> displayers = new List<ChestItemDisplayer>();
    public override int DisplayerCount => displayers.Count;

    public ChestItemDisplayer GetDisplayer(int index) {
        if (index < 0 || index >= DisplayerCount) {
            return null;
        }
        return displayers[index];
    }
    private void Start() {
        ShowTutorial();
    }
    public override void Show() {
        for (int i = 0; i < Capacity; i++) {
            if (DisplayerCount == i) {
                displayers.Add(CreateDisplayer());
            }

            ChestItemDisplayer displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(true);
                SetupDisplayer(displayer, GetItem(i));
            }
        }

        for (int i = Capacity; i < DisplayerCount; i++) {
            ChestItemDisplayer displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(false);
            }
        }
    }

    public ChestItemDisplayer GetClaimItemView(ChestItem item) {
        foreach (var displayer in displayers) {
            if (displayer.Model == item) {
                return displayer;
            }
        }
        return null;
    }

    public void SetupDisplayer(ChestItemDisplayer displayer, ChestItem item) {
        if (displayer == null) {
            return;
        }
        displayer.SetModel(item).Show();
    }

    protected ChestItemDisplayer CreateDisplayer() {
        ChestItemDisplayer viewItem = Instantiate(prefab, layout);
        return viewItem;
    }
    #region Tutorial
    private void ShowTutorial() {
        var finishTutorialOpenChest = GameResources.Instance.TutorialSytemData.FinishTutorialOpenChest;

        if (!finishTutorialOpenChest) {
            TutorialSystem.Instance.SetTimeActiveCanvas(0.1f)
                                    .AssignTarget(TutorialKey.TutorialOpenChest, 1, displayers[0].BtnKey.gameObject);
        }
    }
    #endregion
}