
using Gemmob;
using System;
using System.Collections;
using UnityEngine;

public class ConquerorCombatPanel : CombatPanel {
    [SerializeField] protected ButtonExplorer skipTut;
    [SerializeField] protected AngelBoss angelBoss;
    [SerializeField] protected TutorialIntro tutorialPanel;
    protected override void CombatAwake() {
        base.CombatAwake();
#if !CHEAT
        skipTut.gameObject.SetActive(false);
#endif
        tutorialPanel.Assign();
        angelBoss.RegisterPool(1);
    }
    protected override void CombatStart() {
        base.CombatStart();
        tutorialPanel.gameObject.SetActive(!GameResources.Instance.TutorialSytemData.FinishTutorialIntroduce);
    }
    protected override void AddListener() {
        base.AddListener();
#if CHEAT
        skipTut.AddEvent(SkipTutorial);
#endif
    }
    private void SkipTutorial() {
        if (!GameResources.Instance.TutorialSytemData.FinishTutorialIntroduce) {
            GameResources.Instance.TutorialSytemData.SetFinishAllTutorial();
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
        else {
            skipTut.gameObject.SetActive(false);
        }
    }
    public override IEnumerator IEPlayGame(Action onComplete) {
        var isTutorial = GameResources.Instance.TutorialSytemData.FinishTutorialIntroduce;
        if (!isTutorial) {
            AddListener();
            animGotoPlay?.gameObject.SetActive(false);
            tutorialPanel.StartAction(() => {
                ShowUI();
                onComplete?.Invoke();
            });
        }
        else {
            AddListener();
            yield return null;
            animGotoPlay.gameObject.SetActive(true);
            if (animGotoPlay.AnimationState == null)
                yield break;
            animGotoPlay.AnimationState.SetAnimation(1, animCount, false);
            var duration = animGotoPlay.AnimationState.Data.SkeletonData.Animations.Items[0].Duration;
            yield return Yielder.Wait(duration);
            ShowUI();
            onComplete?.Invoke();
        }
    }
    public void Spawn4ngel() {
        var angelClone = angelBoss.Spawn(GameManager.Instance.GameLoader.transform);
        angelClone.Init(true);
    }
    public void TutorialPlayState2() {
        tutorialPanel.PlayState2();
    }
    public void HideIntroPlayGame() {
        tutorialPanel.HideIntroPlayGame();
    }
}
