

using GameSystem.Common.UI;
using System;
using UnityEngine;
public class TestModPopup : DOTweenFrame {
    [SerializeField] private TestChooseModItemCollectionDisplayer collection;



    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        ModData[] mods = GameResources.Instance.ModGenerator.AllMods;
        collection.AddOnSelect(OnSelect).SetCapacity(mods.Length).SetItems(mods).Show();
        GameManager.Instance.Pause();
    }

    protected override void OnHide(Action onCompleted = null, bool instant = false) {
        base.OnHide(onCompleted, instant);
        GameManager.Instance.Resume();
    }

    private void OnSelect(TestChooseModItemDisplayer displayer) {
        displayer.Model.ApplyTo(GameManager.Instance.GameLoader.Ship);
        Hide();
    }
}
