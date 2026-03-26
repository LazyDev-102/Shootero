using DG.Tweening;
using GameSystem.Common.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPopup : DOTweenFrame {
    #region Variables
    [SerializeField] private float deltaShow = 0.2f;
    [SerializeField] private Image whiteLevelImage;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject tabtoHideGO;
    [SerializeField] private GameObject levelEffect;
    [SerializeField] private Transform levelGO;
    [SerializeField] private Transform levelContent;
    [SerializeField] private Transform modMenuGO;
    [SerializeField] private Transform modContentGO;
    [SerializeField] private Transform rewardGO;
    [SerializeField] private Transform rewardMenuGO;
    [SerializeField] private Transform rewardContentGO;
    [SerializeField] private Transform topTarget;
    [SerializeField] private Transform midTarget;
    [SerializeField] private Transform botTarget;
    [SerializeField] private Button tabToHideButton;
    [SerializeField, Range(0f, 2f)] private float timeAppear = 0.3f;
    [SerializeField, Range(0f, 2f)] private float timeMove = 0.5f;
    [SerializeField, Range(0f, 2f)] private float timeShow = 0.2f;

    [SerializeField] private LevelUpModLayout levelUpModLayout;
    [SerializeField] private LevelUpRewardLayout levelUpRewardLayout;
    private bool hasMod;
    private System.Action onClose;
    #endregion

    #region Function Mono
    private void Awake() {
        tabToHideButton.onClick.AddListener(OnClose);
    }
    #endregion

    #region Function
    public LevelUpPopup SetData() {
        RemoveOnClose();
        var dataProgress = GameResources.Instance.LevelProgress;
        var currentLevel = dataProgress.Datas.PointLevelup;
        if (dataProgress.Datas.MaxLevel) {
            currentLevel = dataProgress.Datas.ExpProgress.Count - 1;
        }
        var modDatas = dataProgress.Datas.UnlockFeatures.GetUnlockMods(currentLevel);
        var rewardData = dataProgress.Datas.LevelReward(currentLevel - 1);
        levelUpModLayout.UpdateUI(modDatas);
        hasMod = modDatas != null;
        levelUpRewardLayout.UpdateUI(rewardData);
        levelText.text = $"{currentLevel}";
        //dataProgress.Datas.PointLevelup++;
        return this;
    }
    public LevelUpPopup SetData(int level) {
        var modDatas = GameResources.Instance.LevelProgress.Datas.UnlockFeatures.GetUnlockMods(level);
        var rewardData = GameResources.Instance.LevelProgress.Datas.ExpProgress[level].GetRewards();
        levelUpModLayout.UpdateUI(modDatas);
        hasMod = modDatas != null;
        levelUpRewardLayout.UpdateUI(rewardData);
        levelText.text = $"{GameResources.Instance.LevelProgress.GetCurrentLevel() + 1}";
        return this;
    }
    public void CheckShowAgain() {
        if (GameResources.Instance.LevelProgress.Datas.PointLevelup < GameResources.Instance.LevelProgress.GetCurrentLevel() + 1) {
            GameResources.Instance.LevelProgress.Datas.PointLevelup++;
            PopupHUD.Instance.Show<LevelUpPopup>().SetData().AddOnClose(CheckShowAgain).Show();
        }
    }
    public void Show() {
        PresetShowLevel();
        ShowLevel();
    }
    private void PresetShowLevel() {
        levelGO.gameObject.SetActive(true);
        levelContent.gameObject.SetActive(false);
        levelGO.localScale = Vector3.one * 1.5f;
        whiteLevelImage.SetAlpha(1);
        levelEffect.SetActive(false);
    }
    private void ShowLevel() {
        levelGO.DOScale(Vector3.one, timeShow)
               .SetEase(Ease.Linear)
               .SetUpdate(true)
               .OnComplete(ShowLevelComplete);
    }
    private void ShowLevelComplete() {
        levelContent.gameObject.SetActive(true);
        levelContent.localScale = Vector3.one * 1.3f;
        levelContent.DOScale(Vector3.one, timeShow).SetUpdate(true);
        whiteLevelImage.DOFade(0, timeShow)
                       .SetUpdate(true)
                       .OnComplete(() => levelEffect.SetActive(true));
        if (hasMod)
            ShowMod();
        else
            ShowWithoutMod();
    }
    private void ShowMod() {
        PresetShowMenuMod();
        modMenuGO.DOScale(Vector3.one, timeShow)
                 .SetEase(Ease.Linear)
                 .SetUpdate(true)
                 .OnComplete(ShowMenuModComplete);
    }
    private void PresetShowMenuMod() {
        modMenuGO.gameObject.SetActive(true);
        modMenuGO.localScale = Vector3.one * 1.5f;
    }
    private void ShowMenuModComplete() {
        PresetShowModContent();
        modContentGO.DOScale(Vector3.one, timeShow)
                    .SetEase(Ease.Linear)
                    .SetUpdate(true)
                    .OnComplete(ShowContentModComplete);
    }
    private void PresetShowModContent() {
        modContentGO.gameObject.SetActive(true);
        modContentGO.localScale = Vector3.one * 1.5f;
    }
    private void ShowContentModComplete() {
        StartCoroutine(levelUpModLayout.PlayWhiteEffect(deltaShow));
        PresetShowMenuReward();
        rewardMenuGO.DOScale(Vector3.one, timeShow)
                    .OnComplete(ShowMenuRewardComplete);
    }
    private void PresetShowMenuReward() {
        rewardMenuGO.gameObject.SetActive(true);
        rewardMenuGO.localScale = Vector3.one * 1.5f;
    }
    private void ShowMenuRewardComplete() {
        PresetShowContentReward();
        rewardContentGO.DOScale(Vector3.one, timeShow)
                       .OnComplete(ShowContentRewardComplete);
    }
    private void PresetShowContentReward() {
        rewardContentGO.gameObject.SetActive(true);
        rewardContentGO.localScale = Vector3.one * 1.5f;
    }
    private void ShowContentRewardComplete() {
        DOVirtual.DelayedCall(0.5f, () => {
            StartCoroutine(levelUpRewardLayout.PlayWhiteEffect(deltaShow, () => {
                tabToHideButton.interactable = true;
                tabtoHideGO.SetActive(true);
            }));
        }).SetUpdate(true);
    }
    private void ShowWithoutMod() {
        PresetShowWithoutMod();
        rewardMenuGO.DOScale(Vector3.one, timeShow)
                    .OnComplete(ShowWithoutModComplete);
    }
    private void PresetShowWithoutMod() {
        rewardGO.position = midTarget.position;
        rewardMenuGO.gameObject.SetActive(true);
        rewardMenuGO.localScale = Vector3.one * 1.5f;
    }
    private void ShowWithoutModComplete() {
        rewardContentGO.gameObject.SetActive(true);
        rewardContentGO.localScale = Vector3.one * 1.5f;
        rewardContentGO.DOScale(Vector3.one, timeShow)
                       .OnComplete(ShowContentRewardComplete);
    }

    private void OnClose() {
        Hide();
        levelGO.gameObject.SetActive(false);
        modContentGO.gameObject.SetActive(false);
        modMenuGO.gameObject.SetActive(false);
        rewardContentGO.gameObject.SetActive(false);
        rewardGO.position = botTarget.position;
        rewardMenuGO.gameObject.SetActive(false);
        tabtoHideGO.SetActive(false);
        tabToHideButton.interactable = false;
        onClose?.Invoke();
        //CheckShowAgain();
    }
    public LevelUpPopup AddOnClose(System.Action onClose) {
        this.onClose = onClose;
        return this;
    }
    public LevelUpPopup RemoveOnClose() {
        this.onClose = null;
        return this;
    }
    public override Frame OnBack() {
        if (tabToHideButton.interactable)
            OnClose();
        return this;
    }
    #endregion
}
