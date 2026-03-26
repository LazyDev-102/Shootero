using UnityEngine;
using GameSystem.Common.UI;
using System;
using TMPro;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;
using Gemmob.Tutorial;

public class ChooseModPopup : DOTweenFrame {
    [SerializeField] private RerollChooseModFrame rerollFrame;
    [SerializeField] private ModItemDisplayer[] displayers;
    [SerializeField] private TextMeshProUGUI levelUpText;
    [SerializeField] private TextMeshProUGUI chooseModText;
    [SerializeField] private GameObject header;
    [SerializeField] private GameObject content;
    [SerializeField] private Image bg;
    [SerializeField] private ButtonBase closeButton;
    [SerializeField] private int endScrollValue;
    [SerializeField] private int speedScroll = 70;
    [SerializeField] private float showTime = 0.1f;

    private TutorialSytemData tutData;
    private LevelProgressData levelProgressData;
    private ShipBase ship;
    private ModGenerator modGenerator;
    private bool canClose;
    private ModData[] mods;
    private bool isTutorialPlayGame;
    private Action onComplete;
    private bool isAbilityStartPattern;

    private void Awake() {
        closeButton.AddEvent(OnClose);
        isTutorialPlayGame = CanShowTutorialPlayGame();
        tutData = GameResources.Instance.TutorialSytemData;
        levelProgressData = GameResources.Instance.LevelProgress;
        ship = GameManager.Instance.GameLoader.Ship;
        modGenerator = GameResources.Instance.ModGenerator;
    }
    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        if (!GameManager.Instance.GameState.Equals(GameState.Playing)) {
            canClose = true;
            OnClose();
            return;
        }
        Init();
        base.OnShow(onCompleted, instant);
        SetupShow(() => GeneralMods(null), ShowHeader, ShowContent);
    }
    protected override void OnHide(Action onCompleted = null, bool instant = false) {
        base.OnHide(onCompleted, instant);
        var combat = IngameHUD.Instance.Combat;
        GameManager.Instance.Resume();
        if (ship != null) {
            if (PrefSaver.MoveFocus)
                ship.ShipMove.ForceTouchDown = true;
            if (!isAbilityStartPattern)
                ship.ShipLevel.EndLeveluping();
            combat.RemoveCanvasPlayerLevelBar();
            if (!combat.PlayerLevelBar.CanChooseMod) {
                combat.ShowModInfo.ShowNewMod();
            }
        }
    }
    public override Frame OnBack() {
        return this;
    }
    public void Init() {
        canClose = false;
        header.SetActive(false);
        content.SetActive(false);
        rerollFrame.gameObject.SetActive(false);
        if(IngameData.currentGameMode == GameMode.Conqueror) {
            var conqueCombat = IngameHUD.Instance.GetCombat<ConquerorCombatPanel>();
            if (conqueCombat != null) {
                conqueCombat.HideIntroPlayGame();
            }
        }
        var combat = IngameHUD.Instance.GetCombat<CombatPanel>();
        if (combat != null) {
            combat.AddCanvasPlayerLevelBar();
        }
    }

    #region Tutorial
    private void ShowTutorial() {
        if (IngameData.currentGameMode == GameMode.Conqueror) {
            ShowTutorialIntroduce();
            ShowTutorialPlayGame();
        }
    }
    private bool ShowTutorialIntroduce() {
        var finishTutorial = tutData.FinishTutorialIntroduce;
        var level = IngameHUD.Instance.Combat.GetCurrentLevelInGame() - ship.ShipLevel.UpgradePoint + 1;
        if (!finishTutorial) {
            if (level == 2) {
                ActiveChooseModText(false);
                displayers[1].OffsetLayoutTutorial();
                TutorialSystem.Instance.SetTimeActiveCanvas(0.1f)
                                        .InitPointer(Vector3.one * 1.5f, 4f, "Choose bullet pattern", 7)
                                        .GetData(TutorialKey.TutorialIntroduce)
                                        .AssignTarget(TutorialKey.TutorialIntroduce, 0, displayers[1].gameObject)
                                        .ShowTutorial(OnCompleteTutorialIntroduce);
            }
            if (level == 4) {
                ActiveChooseModText(false);
                displayers[1].OffsetLayoutTutorial();
                TutorialSystem.Instance.InitPointer(Vector3.one * 1.5f, 4f, "Not strong enough? \n Upgrade your bullet now!", 7)
                                        .AssignTarget(TutorialKey.TutorialIntroduce, 1, displayers[1].gameObject);
            }
        }
        return finishTutorial;
    }
    private void ShowTutorialPlayGame() {
        if (isTutorialPlayGame) {
            var level = IngameHUD.Instance.Combat.GetCurrentLevelInGame() - ship.ShipLevel.UpgradePoint + 1;
            if (level == 2) {
                ActiveChooseModText(false);
                displayers[1].OffsetLayoutTutorial();
                TutorialSystem.Instance.SetTimeActiveCanvas(0.1f)
                                        .InitPointer(Vector3.one * 1.5f, 4f, "Try it out!", 7)
                                        .GetData(TutorialKey.TutorialPlayGame)
                                        .AssignTarget(TutorialKey.TutorialPlayGame, 0, displayers[1].gameObject)
                                        .ShowTutorial(OnCompleteTutorialPlayGame);
            }
            if (level == 3) {
                ActiveChooseModText(false);
                displayers[1].OffsetLayoutTutorial();
                TutorialSystem.Instance.InitPointer(Vector3.one * 1.5f, 4f, "", 7)
                                        .AssignTarget(TutorialKey.TutorialPlayGame, 1, displayers[1].gameObject);
            }
        }
    }
    private bool CanShowTutorialPlayGame() {
        if (tutData == null)
            tutData = GameResources.Instance.TutorialSytemData;
        return tutData.FinishTutorialIntroduce &&
            GameResources.Instance.ConquerorData.IsTutPlayGame;
    }
    private void OnCompleteTutorialIntroduce() {
        tutData.SetFinishTutorialIntroduce(true)
                .GetRewardKey()
                .GetRewardEnergy();
    }
    private void OnCompleteTutorialPlayGame() {
        tutData.SetFinishTutorialPlayGame(true);
    }
    #endregion

    public void SetStartPattern() {
        isAbilityStartPattern = true;
    }
    private void SetDisplayer(ModData[] mods) {
        content.SetActive(true);
        for (int i = 0; i < mods.Length; ++i) {
            int index = i;
            displayers[i].SetAlpha(0);
            displayers[i].SetIcon(mods[i].Icon).SetName(mods[i].NameMod);
            displayers[i].OnItemClicked(() => {
                if (!gameObject.activeInHierarchy)
                    return;
                if (isAbilityStartPattern) {
                    isAbilityStartPattern = false;
                    mods[index].ApplyTo(ship);
                    ship.ShipLevel.EnableAbilityStartPattern();
                }
                else {
                    IngameHUD.Instance.Combat.PlayerLevelBar.SetNumberLevelUp(-1);
                    mods[index].ApplyTo(ship);
                    ship.ShipLevel.CurrentUpgradeLevel++;
                }
                for (int j = 0; j < displayers.Length; j++) {
                    displayers[j].CanClick(false);
                    if (j != index) {
                        displayers[j].transform.DOScale(Vector3.zero, 0.3f).SetUpdate(true)/*.SetEase(Ease.InOutBack)*/;
                        continue;
                    }
                    displayers[index].transform.DOScale(Vector3.one * 1.5f, 0.25f).SetUpdate(true).SetEase(Ease.Linear).OnComplete(() => {
                        canClose = true;
                        displayers[index].transform.DOScale(Vector3.zero, 0.2f).SetUpdate(true).SetEase(Ease.Linear).OnComplete(() => {
                            IngameHUD.Instance.Combat.ShowModInfo.AddModInfor(mods[index]);
                            CheckShowNext();
                        });
                    });
                }
            });
        }
    }
    private bool CheckMod() {
        if (mods == null || mods.Length == 0)
            return false;
        foreach (var item in mods) {
            if (item == null)
                return false;
        }
        return true;
    }
    public void GeneralMods(Action onComplete) {
        if (ship != null) {
            ActiveChooseModText(true);
            bool trial = GameManager.Instance.IsTrial;
            if (GameManager.Instance.IsTrial) {
                GenModTrial();
            }
            else {
                bool condition1 = !tutData.FinishTutorialIntroduce;
                bool condition2 = !condition1 && isTutorialPlayGame && IngameData.currentGameMode == GameMode.Conqueror;
                bool condition3 = !condition2 && levelProgressData.Datas.NewLevelUnlock;
                GenModsWithTutorialIntroduce(condition1, onComplete);
                GenModWithTutorialPlayGame(condition2, onComplete);
                GenModOnLevelUp(!condition1 && condition3);
                GenModNormal(!condition1 && !condition2 && !condition3);
                if (!CheckMod())
                    GenModNormal(true);
            }
            DOVirtual.DelayedCall(0.5f, () => {
                SetDisplayer(mods);
                StartShow();
                DOVirtual.DelayedCall(0.3f, SetState1).SetUpdate(true);
                DOVirtual.DelayedCall(0.6f, SetState2).SetUpdate(true);
                DOVirtual.DelayedCall(1f, SetState3).SetUpdate(true);
            }).SetUpdate(true);
        }
        else {
            OnClose();
        }
    }
    private void GenModsWithTutorialIntroduce(bool status, Action onComplete) {
        if (!status)
            return;
        var clevelIngame = IngameHUD.Instance.Combat.GetCurrentLevelInGame() - ship.ShipLevel.UpgradePoint + 1;
        mods = modGenerator.GetModDatasInTutorialIntroduce(clevelIngame);
        DOVirtual.DelayedCall(4f, () => onComplete?.Invoke()).SetUpdate(true);
    }
    private void GenModWithTutorialPlayGame(bool status, Action onComplete) {
        if (!status)
            return;
        var clevelIngame = IngameHUD.Instance.Combat.GetCurrentLevelInGame() - ship.ShipLevel.UpgradePoint + 1;
        mods = modGenerator.GetModDatasInTutorialPlayGame(clevelIngame);
        DOVirtual.DelayedCall(4f, () => onComplete?.Invoke()).SetUpdate(true);
    }
    private void GenModOnLevelUp(bool status) {
        if (!status)
            return;
        bool isAttackMod = ship.ShipLevel.HasMustChooseAttackMod;
        mods = modGenerator.GetRandomModDatasOnLevelUp(isAttackMod);
        if (!isAttackMod) {
            levelProgressData.Datas.SetNewLevelUnlock(false);
        }
    }
    private void GenModTrial() {
        var clevelIngame = IngameHUD.Instance.Combat.GetCurrentLevelInGame() - ship.ShipLevel.UpgradePoint + 1;
        mods = modGenerator.GetModDatasInTrial(clevelIngame);
    }
    private void GenModNormal(bool status) {
        if (!status)
            return;
        bool isAttackMod = ship.ShipLevel.HasMustChooseAttackMod;
        mods = modGenerator.GetRandomModDatas(isAttackMod);
    }
    private void SetupShow(Action onComplete, Action showHeader, Action showContent, bool skip = false) {
        bg.SetAlpha(0);
        if (gameObject.activeInHierarchy)
            StartCoroutine(SetAlphaBg(0.2f, onComplete, showHeader, showContent));
        else {
            onComplete?.Invoke();
            showHeader?.Invoke();
            showContent?.Invoke();
        }
        GameManager.Instance.Pause();
    }
    private IEnumerator SetAlphaBg(float time, Action onComplete, Action showHeader, Action showContent) {
        var duration = 0f;
        float ratio = 0.75f / time;
        while (duration < time) {
            duration += 0.02f;
            bg.SetAlpha(duration * ratio);
            yield return new WaitForSecondsRealtime(0.02f);
        }
        PlayFocusEffect();
        yield return new WaitForSecondsRealtime(0.5f);
        bg.SetAlpha(0.75f);
        onComplete?.Invoke();
        showHeader?.Invoke();
        showContent?.Invoke();
    }
    public void SetTextLevel() {
        levelUpText.text = IngameHUD.Instance.Combat.GetLevelText();
    }
    private void ShowHeader() {
        SetTextLevel();
        header.SetActive(true);
        header.transform.localScale = Vector3.zero;
        header.transform.DOScale(Vector3.one, showTime).SetEase(Ease.Linear).SetUpdate(true);
        chooseModText.SetAlpha(0);
        DOVirtual.DelayedCall(1f, () => chooseModText.DOFade(1, 0.2f).SetUpdate(true)).SetUpdate(true);
    }
    private void ShowContent() {
        foreach (var item in displayers) {
            item.transform.localScale = Vector3.zero;
            item.transform.DOScale(Vector3.one, showTime).SetUpdate(true);
        }
    }
    private void ActiveChooseModText(bool status) {
        chooseModText.gameObject.SetActive(status);
    }
    private void CheckShowNext() {
        if (IngameHUD.Instance.Combat.PlayerLevelBar.CanChooseMod) {
            Init();
            PlayFocusEffect();
            DOVirtual.DelayedCall(0.5f, () => {
                GeneralMods(ShowTutorial);
                ShowHeader();
                DOVirtual.DelayedCall(0.1f, ShowContent).SetUpdate(true);
            }).SetUpdate(true);
        }
        else { OnClose(); }
    }
    private void PlayFocusEffect() {
        if (gameObject.activeInHierarchy)
            IngameHUD.Instance.Combat.PlayerLevelBar.PlayFocusLevelEffect();
    }
    public void AddOnComplete(Action onComplete) {
        this.onComplete = onComplete;
    }
    private void OnClose() {
        if (!canClose)
            return;
        StopAllCoroutines();
        Hide();
        onComplete?.Invoke();
        DOVirtual.DelayedCall(1, () => onComplete = null);
        if (ship != null && ship.ShipHitbox != null)
            ship.ShipHitbox.TurnOnProtectShield(2);
    }
    public void RerollMods(bool isSpecial) {
        if (ship != null) {
            bool isAttackMod = ship.ShipLevel.HasMustChooseAttackMod;
            mods = modGenerator.GetRerollRandomModDatas(isAttackMod, mods, isSpecial);
            DOVirtual.DelayedCall(0.5f, () => {
                SetDisplayer(mods);
                StartShow();
                DOVirtual.DelayedCall(0.3f, SetState1).SetUpdate(true);
                DOVirtual.DelayedCall(0.6f, SetState2).SetUpdate(true);
                DOVirtual.DelayedCall(1f, SetState3).SetUpdate(true);
            }).SetUpdate(true);
        }
        else {
            OnClose();
        }
    }
    #region ShowAnim
    private void StartShow() {
        displayers[0].CanClick(false);
        displayers[1].CanClick(false);
        displayers[2].CanClick(false);
    }

    private void SetState1() {
        StartCoroutine(State1());
    }
    private IEnumerator State1() {
        yield return StartCoroutine(displayers[0].Showing(endScrollValue, mods[0].Icon, speedScroll));
    }
    private void SetState2() {
        StartCoroutine(State2());
    }
    private IEnumerator State2() {
        yield return StartCoroutine(displayers[1].Showing(endScrollValue, mods[1].Icon, speedScroll));
    }
    private void SetState3() {
        StartCoroutine(State3());
    }
    private IEnumerator State3() {
        yield return StartCoroutine(displayers[2].Showing(endScrollValue, mods[2].Icon, speedScroll, EndState));
    }
    private void EndState() {
        ShowTutorial();
        DOVirtual.DelayedCall(0.5f, () => {
            displayers[0].CanClick(true);
            displayers[1].CanClick(true);
            displayers[2].CanClick(true);
            if (GameResources.Instance.AutoPlay)
                displayers[UnityEngine.Random.Range(0, 3)].ChooseCheat();
        }).SetUpdate(true);
        if (!GameManager.Instance.IsTrial && GameResources.Instance.ConquerorData.UnlockZone > 1)
            rerollFrame.SetRef(this)
                       .Active(HideItem);
    }
    private void HideItem() {
        for (int j = 0; j < displayers.Length; j++) {
            displayers[j].CanClick(false);
            displayers[j].ResetUI();
            displayers[j].transform.DOScale(Vector3.zero, 0.1f).SetUpdate(true);
        }
        DOVirtual.DelayedCall(0.3f, ShowContent);
    }
    #endregion
}