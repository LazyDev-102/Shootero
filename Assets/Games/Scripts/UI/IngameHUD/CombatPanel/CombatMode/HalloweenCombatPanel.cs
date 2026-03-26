
using UnityEngine;

public class HalloweenCombatPanel : CombatPanel {
    [SerializeField] protected HalloweenCombatFrame combatFrame;
    protected override void CombatAwake() {
        base.CombatAwake();
    }
    protected override void CombatStart() {
        base.CombatStart();
        combatFrame.Active();
        waveLabel.gameObject.SetActive(false);
        txtCurrentWave.gameObject.SetActive(false);
    }
    protected override void OnWaveStart(EventKey.GameStartWaveParam param) {
        currentWave = param.currentWaveIndex + 1;
        txtCurrentWave.text = $" {currentWave}/{maxWave}";
        GameResources.Instance.Halloween.SetCurrentWave(currentWave);
    }
}
