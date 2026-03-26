

public class B08Attack : BossAttack {
    private B08Base b08Base;
    public B08Base B08Base {
        get {
            if (b08Base == null) {
                b08Base = BossBase as B08Base;
            }
            return b08Base;
        }
    }

}
