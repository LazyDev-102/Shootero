using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyLoginItemItem : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Image icon;

    public void SetData(int count, string preDescription, Sprite icon) {
        countText.text = $"x{count}";
        this.icon.sprite = icon;
    }
}
