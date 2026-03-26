public class B09Attack : BossAttack {
    private B09Base b09Base;

    public B09Base B09Base {
        get {
            if (b09Base == null) {
                b09Base = BossBase as B09Base;
            }
            return b09Base;
        }
    }
}
