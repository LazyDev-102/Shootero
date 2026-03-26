using UnityEngine;

public class MaterialModeCombatPanel : CombatPanel {
    [SerializeField] protected MaterialCombatFrame combatFrame;
    protected override void CombatAwake() {
        base.CombatAwake();
    }
    protected override void CombatStart() {
        base.CombatStart();
        combatFrame.Active(true);
    }
    public override void SetMaxWave() {
        combatFrame.SetMaxWave();
    }
    protected override void OnWaveStart(EventKey.GameStartWaveParam param) {
        currentWave = param.currentWaveIndex + 1;
        txtCurrentWave.text = $" {currentWave}/{maxWave}";
        combatFrame.ChangeReward(currentWave - 1);
    }
    public void ShowMaterialModeRewardPerWave() {
        combatFrame.ShowRewardOnWinWave();
    }
    public void PlayModesBuffEffect(bool isBuff, string description) {
        combatFrame.PlayModesBuffEffect(isBuff, description);
    }
    public void StopModesBuffEffect() {
        combatFrame.StopModesBuffEffect();
    }
}
