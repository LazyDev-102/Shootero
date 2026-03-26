

public class B15Attack : BossAttack {
    private B15Base b15Base;

    public B15Base B15Base {
        get {
            if(b15Base == null) {
                b15Base = BossBase as B15Base;
            }
            return b15Base;
        }
    }
}
