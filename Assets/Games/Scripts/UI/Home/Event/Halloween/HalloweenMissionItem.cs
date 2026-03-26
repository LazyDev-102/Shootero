using Gemmob;
using TMPro;
using UnityEngine;

public class HalloweenMissionItem : MonoBehaviour, IItem<HalloweenMissionItemData>
{
    [SerializeField] private TextMeshProUGUI missionDescription;
    [SerializeField] private TextMeshProUGUI rewardAmountText;
    [SerializeField] private GameObject completeFrame;
    [SerializeField] private GameObject lockFrame;
    [SerializeField] private GameObject rewardFrame;
    [SerializeField] private ButtonBase claimButton;

    public HalloweenMissionItemData dataStack { get ; set ; }

    private void Start() {
        claimButton.AddEvent(ClaimReward);
    }

    public void Initialize(HalloweenMissionItemData data) {
        SetData(data);
        Generate();
    }
    private void SetData(HalloweenMissionItemData data) {
        dataStack = data;
    }
    public IItem<HalloweenMissionItemData> Generate() {
        bool lastMissionCompleted = dataStack.IsComplete && dataStack.IsLastMission;
        lockFrame.SetActive(lastMissionCompleted);
        rewardFrame.SetActive(!lastMissionCompleted);
        claimButton.SetState(!lastMissionCompleted && dataStack.CanApply());
        missionDescription.text = dataStack.GetDescription();
        rewardAmountText.text = dataStack.GetRewardAmount();
        completeFrame.SetActive(!lastMissionCompleted && dataStack.CanApply());
        return this;
    }

    private void ClaimReward() {
        dataStack.Apply();
        Reload();
        EventDispatcher.Instance.Dispatch(new EventKey.OnClaimHalloweenMission() { Position = rewardAmountText.transform.position });
    }
    private void Reload() {
        if(dataStack.NextMission != null) {
            dataStack.NextMission.Assign();
            Initialize(dataStack.NextMission);
        }
        Initialize(dataStack);
        //else {
        //    this.Recycle();
        //}
    }
}
