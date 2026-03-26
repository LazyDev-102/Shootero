

public class B04Attack : BossAttack {
    private B04Base b04Base;

    public B04Base B04Base {
        get {
            if(b04Base == null) {
                b04Base = BossBase as B04Base;
            }
            return b04Base;
        }
    }
}
