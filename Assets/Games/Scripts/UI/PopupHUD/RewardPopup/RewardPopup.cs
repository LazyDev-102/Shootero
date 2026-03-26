using DG.Tweening;
using GameSystem.Common.UI;
using Gemmob;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RewardPopup : DOTweenFrame, ILayout<RewardItem, ItemStack> {
    [SerializeField] private RewardItem itemPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private ButtonExplorer closeButton;
    [SerializeField] private TMPro.TextMeshProUGUI title;
    public List<RewardItem> Items { get; set; } = new List<RewardItem>();
    private List<ItemStack> dataStack;
    private List<ItemClaim> dataClaim;
    private string defaultTitle = "REWARDS";

    private bool useName;
    private bool useAmount;
    private bool useIcon;
    private bool useDescription;
    private int multi;
    private Action onClose;
    private void Awake() {
        closeButton.AddEvent(CloseClick);
        dataClaim = new List<ItemClaim>();
        dataStack = new List<ItemStack>();
    }
    public RewardPopup SetTitle(string content) {
        title.text = content;
        return this;
    }
    public RewardPopup UpdateUI(List<ItemStack> data, bool useName = false, bool useAmount = true, bool useIcon = true, bool useDescription = false, Action onClose = null, int multi = 1) {
        SetData(data, useName, useAmount, useIcon, useDescription, onClose, multi);
        GenerateItem();
        SetTitle(defaultTitle);
        return this;
    }
    public RewardPopup UpdateUI(ItemStack[] data, bool useName = false, bool useAmount = true, bool useIcon = true, bool useDescription = false, Action onClose = null, int multi = 1) {
        SetData(data, useName, useAmount, useIcon, useDescription, onClose, multi);
        GenerateItem();
        SetTitle(defaultTitle);
        return this;
    }
    public RewardPopup UpdateClaimUI(ItemClaim[] data, bool useName = false, bool useAmount = true, bool useIcon = true, bool useDescription = false, Action onClose = null, int multi = 1) {
        SetData(data, useName, useAmount, useIcon, useDescription, onClose, multi);
        GenerateItemClaim();
        SetTitle(defaultTitle);
        return this;
    }
    public RewardPopup UpdateClaimUI(List<ItemClaim> data, bool useName = false, bool useAmount = true, bool useIcon = true, bool useDescription = false, Action onClose = null, int multi = 1) {
        SetData(data, useName, useAmount, useIcon, useDescription, onClose, multi);
        GenerateItemClaim();
        SetTitle(defaultTitle);
        return this;
    }
    private void SetData(ItemStack[] data, bool useName = false, bool useAmount = true, bool useIcon = true, bool useDescription = false, Action onClose = null, int multi = 1) {
        ConvertDataStack(data);
        this.useName = useName;
        this.useAmount = useAmount;
        this.useIcon = useIcon;
        this.useDescription = useDescription;
        this.onClose = onClose;
        this.multi = multi;
        closeButton.interactable = true;
    }
    private void SetData(List<ItemStack> data, bool useName = false, bool useAmount = true, bool useIcon = true, bool useDescription = false, Action onClose = null, int multi = 1) {
        this.dataStack = data;
        this.useName = useName;
        this.useAmount = useAmount;
        this.useIcon = useIcon;
        this.useDescription = useDescription;
        this.onClose = onClose;
        this.multi = multi;
        closeButton.interactable = true;
    }
    private void SetData(ItemClaim[] data, bool useName = false, bool useAmount = true, bool useIcon = true, bool useDescription = false, Action onClose = null, int multi = 1) {
        ConvertDataClaim(data);
        this.useName = useName;
        this.useAmount = useAmount;
        this.useIcon = useIcon;
        this.useDescription = useDescription;
        this.onClose = onClose;
        this.multi = multi;
        closeButton.interactable = true;
    }
    private void SetData(List<ItemClaim> data, bool useName = false, bool useAmount = true, bool useIcon = true, bool useDescription = false, Action onClose = null, int multi = 1) {
        ConvertDataClaim(data);
        this.useName = useName;
        this.useAmount = useAmount;
        this.useIcon = useIcon;
        this.useDescription = useDescription;
        this.onClose = onClose;
        this.multi = multi;
        closeButton.interactable = true;
    }
    private void ConvertDataStack(ItemStack[] data) {
        dataStack.Clear();
        for (int i = 0; i < data.Length; i++) {
            dataStack.Add(data[i]);
        }
    }
    private void ConvertDataClaim(ItemClaim[] data) {
        dataClaim.Clear();
        for (int i = 0; i < data.Length; i++) {
            foreach (var item in data[i].Open().Stack()) {
                dataClaim.Add(item);
            }
        }
    }
    private void ConvertDataClaim(List<ItemClaim> data) {
        dataClaim.Clear();
        foreach (var item in data) {
            foreach (var itemChild in item.Open().Stack()) {
                dataClaim.Add(itemChild);
            }
        }
        Dictionary<int, ItemClaim> result = new Dictionary<int, ItemClaim>();
        foreach (var item in dataClaim) {
            if (!result.ContainsKey(item.Id)) {
                result.Add(item.Id, new ItemClaim(item.Id, 0));
            }
            result[item.Id].Amount += item.Amount;
        }
        dataClaim.Clear();
        foreach (var resultItem in result) {
            dataClaim.Add(resultItem.Value);
        }
    }
    public void GenerateItem() {
        if (Items != null && Items.Count > dataStack.Count) {
            for (int i = 0; i < Items.Count; i++) {
                if (i < dataStack.Count) {
                    Items[i].UpdateUI(dataStack[i], multi: multi);
                }
                Items[i].gameObject.SetActive(i < dataStack.Count && dataStack[i].Amount > 0);
            }
        }
        else {
            for (int i = 0; i < dataStack.Count; i++) {
                if (Items == null || i >= Items.Count) {
                    var gearItem = itemPrefab.Spawn(container);
                    gearItem.transform.localScale = Vector3.one;
                    Items.Add(gearItem);
                }
                Items[i].UpdateUI(dataStack[i], multi: multi);
                Items[i].gameObject.SetActive(dataStack[i].Amount > 0);
            }
        }
        if (GameResources.Instance.AutoPlay) {
            DOVirtual.DelayedCall(1, CloseClick);
        }
    }
    public void GenerateItemClaim() {
        if (Items != null && Items.Count > dataClaim.Count) {
            for (int i = 0; i < Items.Count; i++) {
                if (i < dataClaim.Count) {
                    Items[i].UpdateUI(dataClaim[i], multi: multi);
                }
                Items[i].gameObject.SetActive(i < dataClaim.Count && dataClaim[i].Amount > 0);
            }
        }
        else {
            for (int i = 0; i < dataClaim.Count; i++) {
                if (Items == null || i >= Items.Count) {
                    var gearItem = itemPrefab.Spawn(container);
                    Items.Add(gearItem);
                }
                Items[i].UpdateUI(dataClaim[i], multi: multi);
                Items[i].gameObject.SetActive(dataClaim[i].Amount > 0);
            }
        }
        if (GameResources.Instance.AutoPlay) {
            DOVirtual.DelayedCall(1, CloseClick);
        }
    }
    public void SetItemPrefab(RewardItem item) {
        itemPrefab = item;
    }
    private void CloseClick() {
        closeButton.interactable = false;
        DOVirtual.DelayedCall(0.3f, () => {
            Hide();
        });
    }
    public void AddOnClose(Action onClose) {
        this.onClose += onClose;
    }
    protected override void OnHide(Action onCompleted = null, bool instant = false) {
        base.OnHide(onCompleted, instant);
        onClose?.Invoke();
        onClose = null;
        if (GameResources.Instance.TutorialSytemData.FinishAllTutorial) {
            if (GameResources.Instance.RateUs.CanSpecialTrigger()) {
                PopupHUD.Instance.Show<RateUsPopup>();
            }
        }
    }
}
