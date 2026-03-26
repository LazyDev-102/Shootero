

public class B07Attack : BossAttack {

    private B07Base b07Base;
    public B07Base B07Base {
        get {
            if (b07Base == null) {
                b07Base = BossBase as B07Base;
            }
            return b07Base;
        }
    }

}
