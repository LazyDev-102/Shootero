using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ToolBarItem : MonoBehaviour {
    [SerializeField] private Image icon;
    [SerializeField] private Image noticeIcon;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TMPro.TextMeshProUGUI name = default;
    [SerializeField] private Sprite iconSelected;
    [SerializeField] private Sprite iconDeselect;
    [SerializeField] private GameObject lockTab;
    public void SetAlpha(float value) {
        icon.SetAlpha(value);
    }
    public void SetBackgroundAlpha(float value) {
        backgroundImage.SetAlpha(value);
    }
    public ToolBarItem SetLockTab(bool isSelect) {
        lockTab.SetActive(isSelect);
        icon.gameObject.SetActive(!isSelect);
        return this;
    }
    public ToolBarItem SetMaterial(Material mat) {
        backgroundImage.material = mat;
        return this;
    }

    public ToolBarItem MoveUpIcon(bool isSelect, float origin, float diffHigh, AnimationCurve curve) {
        icon.transform.DOLocalMoveY(isSelect ? origin + diffHigh : origin, 0.5f).SetEase(curve);
        return this;
    }

    public ToolBarItem ChangeIcon(bool isSelect) {
        icon.sprite = isSelect ? iconSelected : iconDeselect;
        return this;
    }


    public ToolBarItem SetActiveName(bool active) {
        name.gameObject.SetActive(active);
        return this;
    }

    public float GetOriginPosY() {
        return icon.transform.localPosition.y;
    }

    public void SetNotification(bool value) {
        noticeIcon.gameObject.SetActive(value);
    }
}
