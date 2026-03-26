using Gemmob;
using GameSystem.Common.UI;
using Gemmob.Tutorial;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenChestPopup : BasePopup {
    [Header("PreOpen")]
    [SerializeField] private TextMeshProUGUI txtTapOpen;
    [SerializeField] private SkeletonGraphic skeIconChest;
    [SerializeField] private Image imgIconChest;
    [SerializeField] private ButtonBase btnTapOpen;
    [SerializeField] private Image imgFlash;
    [Header("A Gear")]
    [SerializeField] private OpenChestGearDisplayer gearDisplayer;
    [SerializeField] private ButtonBase btnOpenAgain;
    [SerializeField] private CurrentItemView keyView;
    [SerializeField] private ItemView priceView;
    [SerializeField] private ItemView salePriceView;
    [Header("Gears")]
    [SerializeField] private SimpleOpenChestGearCollectionDisplayer simpleGearCollectionDisplayer;
    [Header("AllAnim")]
    [SerializeField] private DotweenAnimation preOpenChest;
    [SerializeField] private DotweenAnimation openingChest;
    [SerializeField] private DotweenAnimation showGear;
    [SerializeField] private DotweenAnimation showGears;

    [Header("Skills"), Space]
    [SerializeField] private TextMeshProUGUI txtSkillAmount;
    [SerializeField] private TextMeshProUGUI txtSkillDescription;

    [Header("Skeleton")]
    [SerializeField] private string shakeWeak;
    [SerializeField] private string shakeStrong;
    [Header("Notification")]
    [SerializeField] private LockbarNotify lockbarNotify;


    private ChestItem chest;
    private GearSoftData newGear;
    private List<GearSoftData> newGears;
    private bool isOpenSingle;
    private bool isShowSalePrice;

    protected override void Start() {
        base.Start();
        btnTapOpen?.AddEvent(OnButtonTapOpenChestClicked);
        btnOpenAgain?.AddEvent(OnButtonOpenAgainClicked);
        preOpenChest?.Initialize();
        openingChest?.Initialize();
        showGear?.Initialize();
    }

    public OpenChestPopup SetChest(ChestItem chest) {
        this.chest = chest;
        return this;
    }

    public OpenChestPopup SetGear(GearSoftData gear) {
        this.newGear = gear;
        isOpenSingle = true;
        return this;
    }

    public OpenChestPopup SetGears(List<GearSoftData> gears) {
        newGears = gears;
        isOpenSingle = false;
        return this;
    }

    public OpenChestPopup SetShowSalePrice(bool isShow) {
        isShowSalePrice = isShow;
        return this;
    }

    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        LoadChest();
        lockbarNotify.gameObject.SetActive(false);
    }
    private void ShowTutorialOpenChest() {
        if (!GameResources.Instance.TutorialSytemData.FinishTutorialEquipment) {
            TutorialSystem.Instance.SetTimeActiveCanvas(0.1f)
                                    .InitPointer(Vector3.one, 1f, "", 5)
                                    .AssignTarget(TutorialKey.TutorialOpenChest, 2, closeButton.gameObject);
        }
    }
    private void LoadChest() {
        SetCloseState(false, false);
        gearDisplayer.gameObject.SetActive(false);
        simpleGearCollectionDisplayer.gameObject.SetActive(false);
        if (chest) {
            SetTile(chest.Name, true);
            SetSkeletonIconChest(chest.SkinName, true);
            txtTapOpen.gameObject.SetActive(true);
            ShakeWeak();
            SetStateTapOpenButton(true, true);
            ShowIconGear(false);
            ShowButtonOpen(false);
            SetCloseState(false, false);
        }
    }

    private void OnButtonOpenAgainClicked() {
        var inv = GameResources.Instance.Inventory;
        ItemStack curKeyItem = inv.GetItem(chest.KeyOpen.Id);
        bool hasKey = curKeyItem.Amount >= chest.KeyOpen.Amount;
        inv.EnoughPrice(chest.KeyOpen, () => {
            newGear = chest.OpenChest();
            LoadChest();
            DispatchEvent(true);
        }, () => {
            ItemStack price = isShowSalePrice ? chest.NextPrice : chest.Price;
            inv.EnoughPrice(price, () => {
                isShowSalePrice = true;
                newGear = chest.OpenChest();
                LoadChest();
                DispatchEvent(false);
            }, () => {
                ShowLockBarNotify(btnOpenAgain.transform);
            });
        });
    }

    private void DispatchEvent(bool isKey) {
        if (chest.KeyOpen.Id == ConstantItemID.NormalKey) {
            GameResources.Instance.DailyMission.AddPointProgress(MissionType.OpenNormalChest, 1);
            EventDispatcher.Instance.Dispatch(EventKey.OnOpenNormalChest);
            Tracking.Instance.LogShop(isKey? ShopButton.chest_normal_key : ShopButton.chest_normal_gem);
        }
        else {
            GameResources.Instance.DailyMission.AddPointProgress(MissionType.OpenEliteChest, 1);
            EventDispatcher.Instance.Dispatch(EventKey.OnOpenEliteChest);
            Tracking.Instance.LogShop(isKey ? ShopButton.chest_elite_key : ShopButton.chest_elite_gem);
        }
    }
    private void OnButtonTapOpenChestClicked() {
        if (isOpenSingle) {
            OpenAGear();
        }
        else {
            StartCoroutine(IOpenGears());
        }
        txtTapOpen.gameObject.SetActive(false);
        SetStateTapOpenButton(true, false);
    }

    private void OpenSkill() {
        HUDManager.IgnoreUserInput(true);
        OpenChest(() => {
            SetSkeletonIconChest(string.Empty, false);
            ShowGear(() => {
                HUDManager.IgnoreUserInput(false);
                SetCloseState(true, true);
            });
        });
    }
    private void OpenAGear() {
        HUDManager.IgnoreUserInput(true);
        OpenChest(() => {
            SetSkeletonIconChest(string.Empty, false);
            ShowIconGear(true);
            ShowButtonOpen(true);
            if (!GameResources.Instance.TutorialSytemData.FinishTutorialEquipment)
                SetStateOpenAgainButton(false, false);
            ShowGear(() => {
                HUDManager.IgnoreUserInput(false);
                SetCloseState(true, true);
                ShowTutorialOpenChest();
            });
        });
    }

    private IEnumerator IOpenGears() {
        HUDManager.IgnoreUserInput(true);
        OpenChest(() => {
            if (showGears) {
                showGears.Play();
            }
        });
        yield return Yielder.Wait(1f);
        simpleGearCollectionDisplayer.gameObject.SetActive(true);
        for (int i = 1; i <= newGears.Count; ++i) {
            if (simpleGearCollectionDisplayer) {
                if (i == newGears.Count - 1) {
                    simpleGearCollectionDisplayer.SetCapacity(i).SetItems(newGears);
                    simpleGearCollectionDisplayer.Show(false);

                }
                else {
                    simpleGearCollectionDisplayer.SetCapacity(i).SetItems(newGears);
                    simpleGearCollectionDisplayer.Show(true);
                }
            }
            yield return Yielder.Wait(0.35f);
        }
        SetSkeletonIconChest(string.Empty, false);
        HUDManager.IgnoreUserInput(false);
        SetCloseState(true, true);
    }

    private void ShowIconGear(bool show) {
        if (gearDisplayer) {
            gearDisplayer.gameObject.SetActive(show);
            if (show) {
                gearDisplayer.SetModel(newGear).Show();
            }
        }
    }

    private void ShowButtonOpen(bool show) {
        if (show) {
            if (GameResources.Instance.Inventory.EnoughPrice(chest.KeyOpen)) {
                SetPriceView(null, false);
                SetSalePriceView(null, false);
                SetContentKeyView(chest.KeyOpen, true);
                SetStateOpenAgainButton(true, true);
            } else {
                SetPriceView(chest.Price, false);
                SetSalePriceView(chest.Price, true);
                SetContentKeyView(chest.KeyOpen, false);
                SetStateOpenAgainButton(true, true);
            };
        }
        else {
            SetStateOpenAgainButton(false, false);
        }
    }

    private void ShakeWeak() {
        if (skeIconChest) {
            skeIconChest.AnimationState.SetAnimation(0, shakeWeak, true);
        }
    }

    private void OpenChest(Action onComplete) {
        if (skeIconChest) {
            skeIconChest.AnimationState.SetAnimation(0, shakeStrong, false);
        }
        if (openingChest) {
            openingChest.Play(onComplete, true);
        }
        else {
            onComplete?.Invoke();
        }
    }

    private void ShowGear(Action onComplete) {
        if (showGear) {
            showGear.Play(onComplete, true);
        }
        else {
            onComplete?.Invoke();
        }
    }

    private void SetSkeletonIconChest(string skin, bool show) {
        if (skeIconChest) {
            skeIconChest.gameObject.SetActive(show);
            if (show) {
                skeIconChest.Skeleton.SetSkin(skin);
                skeIconChest.Skeleton.SetSlotsToSetupPose();
                skeIconChest.LateUpdate();
            }
        }
    }

    private void SetStateTapOpenButton(bool interaction, bool show) {
        if (btnTapOpen) {
            btnTapOpen.gameObject.SetActive(show);
            if (show) {
                btnTapOpen.SetState(interaction);
            }
        }
    }

    private void SetContentKeyView(ItemStack item, bool show) {
        if (keyView) {
            keyView.gameObject.SetActive(show);
            if (show) {
                keyView.SetModel(item).Show();
            }
        }
    }

    private void SetStateOpenAgainButton(bool interaction, bool show) {
        if (btnOpenAgain) {
            btnOpenAgain.gameObject.SetActive(show);
            if (show) {
                btnOpenAgain.SetState(interaction);
            }
        }
    }

    private void SetPriceView(ItemStack item, bool show) {
        if (priceView) {
            priceView.gameObject.SetActive(show);
            if (show) {
                priceView.SetModel(item).Show();
            }
        }
    }

    private void SetSalePriceView(ItemStack item, bool show) {
        if (salePriceView) {
            salePriceView.gameObject.SetActive(show);
            if (show) {
                salePriceView.SetModel(item).Show();
            }
        }
    }
    public void ShowLockBarNotify(Transform trans) {
        lockbarNotify.transform.position = trans.position;
        lockbarNotify.SetOriginPos(trans.position - Vector3.up * 1).SetContent(GameDefine.InsufficientResources, 0.5f).Show();
    }
    #region Tutorial
    protected override void OnHide(Action onCompleted = null, bool instant = false) {
        base.OnHide(onCompleted, instant);
        DG.Tweening.DOVirtual.DelayedCall(0.3f, ShowTutorialEquipment);
        if (GameResources.Instance.TutorialSytemData.FinishAllTutorial) {
            if (GameResources.Instance.RateUs.CanSpecialTrigger()) {
                PopupHUD.Instance.Show<RateUsPopup>();
            }
        }
    }
    private void ShowTutorialEquipment() {
        if (CanShowTutorialEquipment()) {
            TutorialSystem.Instance.SetTimeActiveCanvas(0.5f)
                                    .GetData(TutorialKey.TutorialEquipment)
                                    .SetBackgroundButtonAlpha(0)
                                    .AssignTarget(TutorialKey.TutorialEquipment, 0, ToolbarScaler.Instance.GetTabObject(ToolBarType.Gears))
                                    .ShowTutorial(OnCompleteTutorialEquipment);
        }
    }
    private bool CanShowTutorialEquipment() {
        return GameResources.Instance.TutorialSytemData.FinishTutorialOpenChest &&
            GameResources.Instance.GearInventory.GearItems.Count > 0 && !GameResources.Instance.TutorialSytemData.FinishTutorialEquipment;
    }
    private void OnCompleteTutorialEquipment() {
        GameResources.Instance.TutorialSytemData.SetFinishTutorialEquipment(true);
    }
    #endregion
}
