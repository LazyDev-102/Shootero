[System.Serializable]
public abstract class CombatPanelAction : GameAction {
    public virtual CombatPanel GetCombat() {
        return IngameHUD.Instance.Show<CombatPanel>();
    }
}
