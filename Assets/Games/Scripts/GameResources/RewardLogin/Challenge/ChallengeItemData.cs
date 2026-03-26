using GameSystem.Common.UnityInspector;
using Gemmob;
using Helper;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Challenge", menuName = "Resource/HardData/Challenge/ChallengeItemData")]
public class ChallengeItemData : ScriptableObject {
    [SerializeField] private int challengeID;
    [SerializeField] private Sprite icon;
    [SerializeField] private ChallengeType type;
    [SerializeField] private ChallengeRankType rank;
    [SerializeField] private string nameMission;
    [SerializeField] private string missionDescription;
    [SerializeField] private int pointTarget;
    [SerializeField] private int pointProgress;
    [SerializeField] private bool isComplete;
    [SerializeField] private int challengePoint;
    [SerializeField] private float chipAfkPoint;
    [SerializeField] private ItemClaim[] rewards;
    [SerializeField] private GameAction gotoSource;
    [SerializeField, ConstantField(typeof(EventKey))] private int[] eventRegisters;

    public int ChallengeID { get => challengeID; }
    public Sprite Icon { get => icon; }
    public ChallengeType Type { get => type; }
    public ChallengeRankType Rank { get => rank; }
    public string NameMission { get => nameMission; }
    public string MissionDescription { get => missionDescription; }
    public int PointTarget { get => pointTarget; }
    public int PointProgress { get => pointProgress; }
    public bool IsComplete { get => isComplete; }
    public int ChallengePoint { get => challengePoint; }
    public ItemClaim[] Rewards { get => rewards; }
    public bool Claimable { get => !isComplete && pointProgress >= pointTarget; }
    public GameAction GotoSource { get => gotoSource; }

    public void Assign() {
        foreach (int eventRegister in GetEventRegisters()) {
            EventDispatcher.Instance.RemoveListener(eventRegister, Upgrade);
            EventDispatcher.Instance.AddListener(eventRegister, Upgrade);
        }
        AssignReward();
    }
    public void Unassign() {
        foreach (int eventRegister in GetEventRegisters()) {
            EventDispatcher.Instance.RemoveListener(eventRegister, Upgrade);
        }
    }
    public IEnumerable<int> GetEventRegisters() {
        return eventRegisters;
    }
    private void AssignReward() {
        if (rewards == null || rewards.Length == 0)
            return;
        foreach (var item in rewards) {
            if (item.Id == ConstantItemID.ChipId) {
                var value = (GameResources.Instance.ChipPerSecond * Constant.HourToSecond * chipAfkPoint).ConvertToInt();
                if (value < 1)
                    value = 1;
                item.Amount = value;
            }
        }
    }
    public virtual void Apply() {
        if (!CanApply())
            return;
        Unassign();
        ResetProgress();
        SetOnComplete(true);
        foreach (var item in rewards) {
            item.Claim();
        }
        GameResources.Instance.DailyMission.AddPointProgress(MissionType.CompleteChallenge, 1);
    }
    public virtual bool CanApply() {
        if (isComplete)
            return false;
        if (pointProgress < pointTarget)
            return false;
        return true;
    }
    public virtual void Upgrade(int amount) {
        if (pointProgress >= pointTarget)
            return;
        pointProgress += amount;
        if (pointProgress >= pointTarget) {
            pointProgress = pointTarget;
        }
        GameResources.Instance.Challenge.SetMissionItemProgress(challengeID, pointProgress);
    }
    private void Upgrade() {
        if (pointProgress >= pointTarget)
            return;
        pointProgress += 1;
        if (pointProgress >= pointTarget) {
            pointProgress = pointTarget;
        }
        GameResources.Instance.Challenge.SetMissionItemProgress(challengeID, pointProgress);
    }
    public void SetProgress(int value) {
        pointProgress = value;
    }
    public virtual void ResetData() {
        SetOnComplete(false);
        ResetProgress();
    }
    public void ResetProgress() {
        pointProgress = 0;
    }
    public void SetOnComplete(bool status) {
        isComplete = status;
        GameResources.Instance.Challenge.SetMissionItemComplete(challengeID, status);
    }
    public float GetProgress() {
        return (float)pointProgress / (float)pointTarget;
    }
    public void GotoAction() {
        if (gotoSource == null)
            return;
        gotoSource.Execute();
    }
}
