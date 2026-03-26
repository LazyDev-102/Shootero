

using System.Collections.Generic;
using UnityEngine;

public class PauseModCollectionDisplayer : CollectionDisplayer<ModData> {
    [SerializeField] private PauseModView prefab;
    [SerializeField] private TrellisLayout layout;
    [SerializeField] private ButtonExplorer backgroundButton;

    protected readonly List<PauseModView> displayers = new List<PauseModView>();
    public override int DisplayerCount => displayers.Count;
    private void Awake() {
        backgroundButton.AddEvent(OnBackgroundClick);
    }
    public PauseModView GetDisplayer(int index) {
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

            PauseModView displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(true);
                SetupDisplayer(displayer, GetItem(i));
            }
        }

        for (int i = Capacity; i < DisplayerCount; i++) {
            PauseModView displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(false);
            }
        }
    }

    public PauseModView GetItemView(ModData abilityData) {
        foreach (var displayer in displayers) {
            if (displayer.Model == abilityData) {
                return displayer;
            }
        }
        return null;
    }

    public void SetupDisplayer(PauseModView displayer, ModData item) {
        if (displayer == null) {
            return;
        }
        displayer.SetModel(item).Show();
    }

    protected PauseModView CreateDisplayer() {
        PauseModView viewItem = Instantiate(prefab, null);
        viewItem.OnSelect(SetBlackBackgroundStatus);
        layout.AddItem(viewItem.transform);
        return viewItem;
    }
    private void SetBlackBackgroundStatus(bool status) {
        backgroundButton.gameObject.SetActive(status);
    }
    private void OnBackgroundClick() {
        backgroundButton.gameObject.SetActive(false);
        foreach (var item in displayers) {
            if (item != null && item.gameObject.activeInHierarchy)
                item.OnDeSelect();
        }
    }

}
