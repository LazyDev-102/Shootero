using UnityEngine;

public class GearModeCombatPanel : CombatPanel {
    [SerializeField] protected GearCombatFrame combatFrame;
    protected override void CombatAwake() {
        base.CombatAwake();
    }
    protected override void CombatStart() {
        base.CombatStart();
        combatFrame.Active(true);
        waveLabel.gameObject.SetActive(false);
        txtCurrentWave.gameObject.SetActive(false);
    }
    protected override void OnWaveStart(EventKey.GameStartWaveParam param) {
        currentWave = param.currentWaveIndex + 1;
        txtCurrentWave.text = $" {currentWave}/{maxWave}";
    }
}
