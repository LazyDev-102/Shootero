using GameSystem.Common.UnityInspector;
using Gemmob;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionItemDataBase", menuName = "Resource/Missions/MissionItemDataBase")]
public class MissionItemDataBase : ScriptableObject {
    [SerializeField] private string nameMission;
    [SerializeField] private string missionDescription;
    [SerializeField] private int pointTarget;
    [SerializeField] private int pointProgress;
    [SerializeField] private bool isComplete;
    [SerializeField] private ItemClaim[] reward;
    [SerializeField] private GameCondition condition;
    [SerializeField] private GameAction gotoSource;
    [SerializeField, ConstantField(typeof(EventKey))] private int[] eventRegisters;

    public string NameMission { get => nameMission; }
    public string MissionDescription { get => missionDescription; }
    public int PointTarget { get => pointTarget; }
    public int PointProgress { get => pointProgress; }
    public bool IsComplete { get => isComplete; }
    public ItemClaim[] Reward { get => reward; }
    public GameCondition Condition { get => condition; }
    public GameAction GotoSource { get => gotoSource; }

    public virtual bool Active() {
        if (condition != null)
            return condition.CheckCondition(null) && !IsComplete;
        else
            return !IsComplete;
    }
    public void Assign() {
        //isComplete = pointProgress >= pointTarget;
        if (!Active())
            return;
        foreach (int eventRegister in GetEventRegisters()) {
            EventDispatcher.Instance.RemoveListener(eventRegister, Upgrade);
            EventDispatcher.Instance.AddListener(eventRegister, Upgrade);
        }
    }
    public void Unassign() {
        foreach (int eventRegister in GetEventRegisters()) {
            EventDispatcher.Instance.RemoveListener(eventRegister, Upgrade);
        }
    }
    private IEnumerable<int> GetEventRegisters() {
        return eventRegisters;
    }
    public virtual void Apply() {
        if (!CanApply())
            return;
        Unassign();
        ClaimReward();
        SetOnComplete(true);
    }
    public virtual bool CanApply() {
        if (isComplete)
            return false;
        if (pointProgress < pointTarget)
            return false;
        return true;
    }
    public virtual void Upgrade() {
        if (pointProgress >= pointTarget)
            return;
        pointProgress += 1;
        if (pointProgress >= pointTarget) {
            pointProgress = pointTarget;
        }
    }
    public virtual void Upgrade(int value) {
        if (pointProgress >= pointTarget)
            return;
        pointProgress += value;
        if (pointProgress >= pointTarget) {
            pointProgress = pointTarget;
        }
    }

    public void SetOnComplete(bool status) {
        isComplete = status;
    }

    public void SetProgress(int value) {
        pointProgress = value;
    }

    private void ClaimReward() {
        if (reward == null || reward.Length == 0)
            return;
        for (int i = 0; i < reward.Length; i++) {
            if (reward[i] != null)
                reward[i].Claim();
        }
    }

    public virtual void ResetData() {
        SetOnComplete(false);
        pointProgress = 0;
    }

    public float GetProgress() {
        return (float)pointProgress / (float)pointTarget;
    }

    public void SetProgress() {
        pointProgress = pointTarget;
    }

    public void GotoAction() {
        if (gotoSource == null)
            return;
        gotoSource.Execute();
    }
}
