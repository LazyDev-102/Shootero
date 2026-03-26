using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FightNotify : MonoBehaviour
{
    [SerializeField]
    private RectTransform _rect1;
    [SerializeField]
    private RectTransform _rect2;

    private Vector2 _anchor1;
    private Vector2 _anchor2;

    void Awake()
    {
        _anchor1 = _rect1.anchoredPosition;
        _anchor2 = _rect2.anchoredPosition;

        _rect1.anchoredPosition = new Vector2(0, _anchor1.y);
        _rect2.anchoredPosition = new Vector2(-15, _anchor2.y);
    }

    [MethodButton("RunAnimation")] public int CallRunAnimation;
    public void RunAnimation()
    {
        gameObject.SetActive(true);
        _rect1.DOKill();
        _rect2.DOKill();
        _rect1.anchoredPosition = new Vector2(-0, _anchor1.y);
        _rect2.anchoredPosition = new Vector2(0, _anchor2.y);
        _rect1.DOAnchorPosX(_anchor1.x - 3, .3f);
        _rect2.DOAnchorPosX(_anchor2.x + 3, .3f).OnComplete(() =>
        {
            _rect1.DOAnchorPosX(_anchor1.x + 3, 1f).SetEase(Ease.Linear);
            _rect2.DOAnchorPosX(_anchor2.x - 3, 1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                _rect1.DOAnchorPosX(800, .15f);
                _rect2.DOAnchorPosX(-800, .15f).OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
            });
        });
    }
}
