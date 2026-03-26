using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyMissionProgressItem : MonoBehaviour, IItem<DailyMissionProgressItemData> {
    [SerializeField] private Image icon;
    [SerializeField] private Image headerIcon;
    [SerializeField] private GameObject tick;
    [SerializeField] private TextMeshProUGUI progressText;


    public DailyMissionProgressItemData dataStack { get; set; }

    public IItem<DailyMissionProgressItemData> Generate() {
        //icon.sprite = dataStack.Icon;
        progressText.text = $"{dataStack.Target}";
        tick.SetActive(dataStack.IsComplete);
        headerIcon.gameObject.SetActive(!dataStack.IsComplete);
        return this;
    }
    public void UpdateUI(DailyMissionProgressItemData data) {
        dataStack = data;
        Generate();
    }
    public void MoveHeader() {
        headerIcon.transform.DOLocalMoveY(headerIcon.transform.localPosition.y + 10, 0.5f).SetEase(Ease.InFlash);
    }
}
