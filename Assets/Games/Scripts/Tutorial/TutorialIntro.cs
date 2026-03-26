using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Gemmob;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class TutorialIntro : MonoBehaviour {
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 midPos;
    [SerializeField] private AnimationCurve moveCurve;

    [SerializeField] private string[] textIntroOnLoad;
    [SerializeField] private string[] textIntroPlayGame;
    [SerializeField] private float[] borderSize;
    [SerializeField] private TextMeshProUGUI introOnLoadText;
    [SerializeField] private TextMeshProUGUI introOnPlayGameText;
    [SerializeField] private RectTransform introOnLoadRect;
    [SerializeField] private RectTransform introOnPlayGameTrans;
    [SerializeField] private Image background;
    [SerializeField] private Image whiteBackground;
    [SerializeField] private ButtonExplorer tabToPlayButton;
    [SerializeField] private TextMeshProUGUI tabToPlayText;
    private System.Action onStartGame;
    private float timeShowState2 = 1f;
    private Tweener tweener;

    public void Assign() {
        tabToPlayButton.interactable = false;
        tabToPlayButton.AddEvent(PlayState2);
    }
    public TutorialIntro StartAction(System.Action onStartGame) {
        this.onStartGame = onStartGame;
        introOnPlayGameTrans.anchoredPosition = startPos;
        tabToPlayText.gameObject.SetActive(false);
        StartCoroutine(ShowIntroText());
        return this;
    }

    public IEnumerator ShowIntroText() {
        yield return Yielder.Wait(1f);
        for (int i = 0; i < textIntroOnLoad.Length; i++) {
            introOnLoadText.text = textIntroOnLoad[i];
            introOnLoadRect.gameObject.SetActive(true);
            tweener?.Kill();
            tweener = introOnLoadRect.DOSizeDelta(new Vector2(borderSize[i], 180), 1f).OnComplete(() => {
                DOVirtual.DelayedCall(1.5f, () => {
                    introOnLoadRect.DOSizeDelta(new Vector2(0, 180), 1f).OnComplete(() => {
                        introOnLoadRect.gameObject.SetActive(false);
                    });
                });
            });
            yield return Yielder.Wait(4f);
        }
        tabToPlayButton.interactable = true;
        tabToPlayText.gameObject.SetActive(true);
        tabToPlayText.SetAlpha(0);
        tweener?.Kill();
        tweener = tabToPlayText.DOFade(1, 1f).SetLoops(-1, LoopType.Yoyo).OnComplete(() => {
            tabToPlayText.SetAlpha(0);
        }).SetUpdate(true);
    }

    private IEnumerator State2() {
        tweener?.Kill();
        tweener = whiteBackground.DOFade(1f, timeShowState2 / 2).SetEase(Ease.Linear).OnComplete(() => {
            background.gameObject.SetActive(false);
            DOVirtual.DelayedCall(timeShowState2, () => whiteBackground.DOFade(0f, timeShowState2 / 2).SetEase(Ease.Linear));
        });
        tabToPlayText.gameObject.SetActive(false);
        yield return StartCoroutine(State3());
    }
    private IEnumerator State3() {
        yield return Yielder.Wait(timeShowState2 * 2);
        onStartGame?.Invoke();
        yield return StartCoroutine(ShowIntroPlayGameText());
    }

    public IEnumerator ShowIntroPlayGameText(int index = 0) {
        yield return Yielder.Wait(1.5f);
        introOnPlayGameText.SetAlpha(0.3f);
        introOnPlayGameText.text = textIntroPlayGame[index];
        introOnPlayGameText.gameObject.SetActive(true);
        tweener?.Kill();
        tweener = introOnPlayGameTrans.DOAnchorPos(midPos, 3f).SetEase(moveCurve);
    }
    public TutorialIntro HideIntroPlayGame() {
        if (!gameObject.activeInHierarchy)
            return this;
        tweener?.Kill();
        tweener = introOnPlayGameText.DOFade(0, 1f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() => {
            introOnPlayGameText.gameObject.SetActive(false);
            introOnPlayGameTrans.anchoredPosition = startPos;
        });
        return this;
    }
    public void PlayState2() {
        if (gameObject.activeInHierarchy) {
            tabToPlayButton.interactable = false;
            StartCoroutine(State2());
        }
    }
}
