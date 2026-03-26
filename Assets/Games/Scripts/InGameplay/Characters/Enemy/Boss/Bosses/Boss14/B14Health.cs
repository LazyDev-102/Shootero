
public class B14Health : BossHealth {
    private B14Base b14Base;
    public B14Base B14Base {
        get {
            if (b14Base == null) {
                b14Base = CharacterBase as B14Base;
            }
            return b14Base;
        }
    }
    public override void Initalize() {
        ForceChangeCurrentHp(CharacterBase.CharacterStat.MaxHP.Value / 5);
    }
}
