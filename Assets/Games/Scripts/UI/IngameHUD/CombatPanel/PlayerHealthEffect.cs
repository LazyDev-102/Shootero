using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Helper;
using System;
using Gemmob;

public class PlayerHealthEffect : MonoBehaviour {

    [SerializeField] private Image imgHealthSign;
    [SerializeField] private Image imgHealthSignConfig;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float fadeDurationOnWeak = 1f;
    [SerializeField] private Image[] icons;
    [SerializeField] private ParticleSystem[] effects;
    [SerializeField] private ParticleSystem bloodSuckingEffects;
    [SerializeField] private ParticleSystem healSmallEffect;

    private Tween currentFadeTween;
    private Tween currentFadeTweenConfig;
    private bool moveDone;
    private long startEffect = -1;
    public void ShowFade() {
        if (startEffect != -1) {
            var timeNow = DateTimeOffset.Now.ToUnixTimeSeconds();
            if (timeNow - startEffect < fadeDuration * 1.1f)
                return;
        }
        ShowHealthEffect();
        startEffect = DateTimeOffset.Now.ToUnixTimeSeconds();
        imgHealthSign.ChangeAlpha(0);
        currentFadeTween = imgHealthSign.DOFade(0.5f, fadeDuration / 3).SetEase(Ease.OutBack).OnComplete(() => {
            HideFade();
        });
    }

    private void HideFade() {
        imgHealthSign.ChangeAlpha(0.5f);
        currentFadeTween = imgHealthSign.DOFade(0, fadeDuration).SetEase(Ease.Linear).OnComplete(HideHealthEffect);
    }
    public void ShowFade(float startValue, float endValue) {
        imgHealthSignConfig.ChangeAlpha(startValue);
        var update = false;
        transform.DOScaleX(1, fadeDurationOnWeak * 2).OnUpdate(() => {
            if (!update) {
                update = true;
                currentFadeTweenConfig = imgHealthSignConfig.DOFade(endValue, fadeDurationOnWeak).SetEase(Ease.OutBack).OnComplete(() => {
                    HideFade(startValue, endValue, () => { update = false; });
                });
            }
        }).SetLoops(-1, LoopType.Restart);
    }

    private void HideFade(float startValue, float endValue, Action onComplete) {
        imgHealthSignConfig.ChangeAlpha(endValue);
        currentFadeTweenConfig = imgHealthSignConfig.DOFade(startValue, fadeDurationOnWeak).SetEase(Ease.Linear).OnComplete(() => {
            onComplete?.Invoke();
        });
    }
    public void StopShowFade() {
        currentFadeTween.Kill(true);
    }
    public void StopShowFadeConfig() {
        currentFadeTweenConfig.Kill(true);
    }

    public IEnumerator SpawnIconPlus() {
        if (!moveDone) {
            foreach (var item in icons) {
                item.gameObject.SetActive(false);
                item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y - 3, item.transform.position.z);
            }
        }
        foreach (var item in icons) {
            moveDone = false;
            item.gameObject.SetActive(true);
            item.DOFade(0.6f, 0.5f).OnComplete(() => {
                item.DOFade(0, 1.5f);
            });
            item.transform.DOScale(Vector3.one, 0.5f).OnComplete(() => {
                item.transform.DOScale(Vector3.zero, 1.5f);
            });
            item.transform.DOMoveY(item.transform.position.y + 3, 2).OnComplete(() => {
                item.gameObject.SetActive(false);
                item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y - 3, item.transform.position.z);
                moveDone = true;
            });
            yield return Yielder.Wait(0.05f);
        }
    }
    public void ShowHealthEffect() {
        if (effects == null || effects.Length == 0)
            return;
        foreach (var item in effects) {
            if (item != null) {
                item.transform.localScale = Vector3.one;
                item.gameObject.SetActive(true);
                item.Play();
            }
        }
    }
    public void HideHealthEffect() {
        if (effects == null || effects.Length == 0)
            return;
        foreach (var item in effects) {
            if (item != null) {
                item.Stop();
            }
        }
    }

    public void BloodSuking() {
        if (bloodSuckingEffects == null)
            return;
        for (int i = 0; i < 3; i++) {
            var clone = bloodSuckingEffects.Spawn(GameManager.Instance.GameLoader.Ship.transform);
            clone.gameObject.SetActive(true);
            clone.transform.localScale = Vector3.one * UnityEngine.Random.Range(1f, 2f);
            clone.transform.localPosition = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0);
            clone.Play();
            DOVirtual.DelayedCall(1f, () => clone.Recycle());
        }
    }
    public void ShowHealSmallEffect() {
        if (healSmallEffect != null && !healSmallEffect.isPlaying) {
            healSmallEffect.transform.position = GameManager.Instance.GameLoader.Ship.transform.position;
            healSmallEffect.Play();
        }
    }
}
