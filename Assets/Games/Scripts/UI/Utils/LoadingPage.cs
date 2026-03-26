using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LoadingPage : MonoBehaviour {
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject group;

    public int CallRunLoading;

    public void TestRunLoading() {
        RunLoading(null, null);
    }

    public void RunLoading(Action loading1Complete, Action loading2Complete) {
        group.SetActive(true);
        _canvasGroup.alpha = 0;
        //bool isShowBackground;

        _canvasGroup.DOFade(1, .2f).OnComplete(() => {

            //isShowBackground = PageManager.Instance.BackgroundMenu.activeInHierarchy;
            //PageManager.Instance.BackgroundMenu.SetActive(false);

            float delayTime = 2;
            if (Application.platform == RuntimePlatform.WindowsEditor)
                delayTime = 1f;

            if (loading1Complete != null)
                loading1Complete.Invoke();

            _canvasGroup.DOFade(0, .2f).OnComplete(() => {
                //if (isShowBackground)
                //PageManager.Instance.BackgroundMenu.SetActive(true);
                group.SetActive(false);
                if (loading2Complete != null)
                    loading2Complete.Invoke();

            }).SetDelay(delayTime);
        });
    }
}
