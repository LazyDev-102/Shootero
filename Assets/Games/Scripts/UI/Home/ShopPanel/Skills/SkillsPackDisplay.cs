
using UnityEngine;
using TMPro;
using Gemmob.Tutorial;

public class SkillsPackDisplay : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI txtDescription;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private ButtonBase btnPrice;
    [SerializeField] private GameObject priceImage;
    [SerializeField] private GameObject freeText;
    [SerializeField] private LockbarNotify lockbar;

    private SkillSystemData data;
    private TutorialSytemData tutData;

    private void SetData() {
        if (data == null)
            data = GameResources.Instance.SkillSystemData;
        if (tutData == null)
            tutData = GameResources.Instance.TutorialSytemData;
    }

    private void Start() {
        btnPrice.AddEvent(OnButtonPriceClicked);
    }

    public void SetActive() {
        SetData();

        bool active = tutData.CanActiveSkills();

        gameObject.SetActive(active);
        if (active)
            UpdateUI();
    }

    private void UpdateUI() {
        lockbar.gameObject.SetActive(false);
        UpdatePrice();
        UpdateDescription();
        ShowTutorial();
    }
    private void UpdatePrice() {
        bool gaveSkill = tutData.GaveSkill;
        priceText.text = $"{data.Pack.Price.Amount}";
        priceImage.SetActive(gaveSkill);
        freeText.SetActive(!gaveSkill);
        priceText.gameObject.SetActive(gaveSkill);
    }
    private void UpdateDescription() {
        txtDescription.text = data.Getx10In <= 1 ? $"x10 Skill" : $"Get x10 Skill in {data.Getx10In} times";
    }

    private void OnButtonPriceClicked() {
        bool gaveSkill = tutData.GaveSkill;
        if (!gaveSkill) {
            OpenChest();
            tutData.SetGaveSkill(true);
        }
        else {
            GameResources.Instance.Inventory.EnoughPrice(data.Pack.Price, () => {
                OpenChest();
            }, () => {
                ShowLockBarNotify(btnPrice.transform);
            });
        }
    }



    private void OpenChest() {
        var count = data.GetRewardCount();
        var skill = data.GetRandomSkill();
        data.ClaimReward(skill, count);
        PopupHUD.Instance.Show<OpenSkillsPopup>().SetData(data)
                                                 .SetSkill(skill)
                                                 .SetCount(count)
                                                 .SetOnClose(UpdateDescription)
                                                 .UpdateUI();
        Tracking.Instance.LogShop(ShopButton.chest_skill);
    }

    public void ShowLockBarNotify(Transform trans) {
        lockbar.transform.position = trans.position;
        lockbar.SetOriginPos(trans.position - Vector3.up * 1)
               .SetContent(GameDefine.InsufficientResources, 0.5f)
               .Show();
    }

    private void ShowTutorial() {
        ShowSkillsEquipTut();
    }
    private void ShowSkillsEquipTut() {
        if (tutData.CanShowOpenSkillTutorial()) {
            TutorialSystem.Instance.SetTimeActiveCanvas(0.1f)
                                    .AssignTarget(TutorialKey.TutorialOpenSkill, 1, btnPrice.gameObject);
        }
    }
}
