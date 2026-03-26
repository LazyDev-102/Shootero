using UnityEngine;

[CreateAssetMenu(fileName = "DailyMissionItemData", menuName = "Resource/HardData/DailyMission/DailyMissionItemData")]
public class DailyMissionItemData : ScriptableObject {
    [SerializeField] private int missionID;
    [SerializeField] private Sprite icon;
    [SerializeField] private MissionType type;
    [SerializeField] private string nameMission;
    [SerializeField] private string missionDescription;
    [SerializeField] private int pointTarget;
    [SerializeField] private int pointProgress;
    [SerializeField] private bool isComplete;
    [SerializeField] private ItemClaim reward;
    [SerializeField] private GameAction gotoSource;

    public int MissionID { get => missionID; }
    public Sprite Icon { get => icon; }
    public MissionType Type { get => type; }
    public string NameMission { get => nameMission; }
    public string MissionDescription { get => missionDescription; }
    public int PointTarget { get => pointTarget; }
    public int PointProgress { get => pointProgress; }
    public bool IsComplete { get => isComplete; }
    public ItemClaim Reward { get => reward; }
    public bool Claimable { get => !isComplete && pointProgress >= pointTarget; }
    public GameAction GotoSource { get => gotoSource; }

    public virtual void Apply() {
        if (!CanApply())
            return;
        GameResources.Instance.DailyMission.AddPointProgress(reward.Amount);
        SetOnComplete(true);
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
        GameResources.Instance.DailyMission.SetMissionItemProgress(missionID - 1, pointProgress);

    }
    public void SetOnComplete(bool status) {
        isComplete = status;
        GameResources.Instance.DailyMission.SetMissionItemComplete(missionID - 1, status);
    }
    public void SetProgress(int value) {
        pointProgress = value;
    }
    public virtual void ResetData() {
        SetOnComplete(false);
        pointProgress = 0;
    }
    public float GetProgress() {
        return (float)pointProgress / (float)pointTarget;
    }
    public void GotoAction() {
        if (gotoSource == null)
            return;
        gotoSource.Execute();
    }
    public void SetProgress() {
        pointProgress = pointTarget;
    }
}
