using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZoneItemVirtualDisplay : MonoBehaviour {
    [Header("Item Center")]
    [SerializeField] private TextMeshProUGUI titleCenter;
    [SerializeField] private TextMeshProUGUI nameWaveCenter;
    [SerializeField] private TextMeshProUGUI nameZoneCenter;
    [SerializeField] private Image backgroundCenter;
    [SerializeField] private float alphaCenter = 0.5f;
    [SerializeField] private Transform leftTarget;

    [Header("Item Right")]
    [SerializeField] private TextMeshProUGUI titleRight;
    [SerializeField] private TextMeshProUGUI nameWaveRight;
    [SerializeField] private TextMeshProUGUI nameZoneRight;
    [SerializeField] private Image backgroundRight;
    [SerializeField] private float alphaRight = 1f;
    [SerializeField] private Transform midTarget;

    public void Show() {
        var originCenter = backgroundCenter.transform.position;
        backgroundCenter.gameObject.SetActive(true);
        backgroundRight.gameObject.SetActive(true);

        titleCenter.DOFade(alphaCenter, 1f);
        nameWaveCenter.DOFade(alphaCenter, 1f);
        nameZoneCenter.DOFade(alphaCenter, 1f);
        backgroundCenter.DOFade(alphaCenter, 1f);
        backgroundCenter.transform.DOMove(leftTarget.position, 1f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() => {
            backgroundCenter.gameObject.SetActive(false);
            backgroundCenter.transform.position = originCenter;
        });

        var originRight = backgroundRight.transform.position;
        titleRight.DOFade(alphaRight, 1f);
        nameWaveRight.DOFade(alphaRight, 1f);
        nameZoneRight.DOFade(alphaRight, 1f);
        backgroundRight.DOFade(alphaRight, 1f);
        backgroundRight.transform.DOMove(midTarget.position, 1f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() => {
            backgroundRight.gameObject.SetActive(false);
            backgroundRight.transform.position = originRight;
        });
    }

}
