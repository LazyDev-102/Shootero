using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class CurrencyView : View<IItemInstance> {
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemAmount;
    [SerializeField] protected float duration = 1;

    protected int previousValue;
    protected Tween curTween;
    protected bool isInit = true;

    public Image ItemIcon => itemIcon;
    public TextMeshProUGUI ItemAmount => itemAmount;

    public override void Show() {
        if (Model == null) {
            return;
        }

        if (ItemIcon != null) {
            ItemIcon.sprite = Model.Icon;
        }

        if (isInit) {
            isInit = false;
            if (ItemAmount != null) {
                ItemAmount.text = Model.Amount.ToString();
            }
        }
        else {
            if (curTween != null) {
                curTween.Kill();
            }
            curTween = DOTween.To(() => previousValue, x => {
                previousValue = x;
                ItemAmount.text = previousValue.ToString();
            }, Model.Amount, duration).OnComplete(() => {
                if (ItemAmount != null) {
                    ItemAmount.text = Model.Amount.ToString();
                }
                previousValue = Model.Amount;
            });
        }
    }

    public CurrencyView SetPreviousValue(int pre) {
        previousValue = pre;
        return this;
    }

    public CurrencyView SetContentAmount(string content, bool show) {
        if (ItemAmount != null) {
            ItemAmount.gameObject.SetActive(show);
            if (show) {
                ItemAmount.text = content;
            }
        }
        return this;
    }
}
