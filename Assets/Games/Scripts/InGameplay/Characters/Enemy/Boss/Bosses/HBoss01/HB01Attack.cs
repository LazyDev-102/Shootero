

public class HB01Attack : BossAttack {
    private HB01Base hb01Base;

    public HB01Base HB01Base {
        get {
            if (hb01Base == null) {
                hb01Base = BossBase as HB01Base;
            }
            return hb01Base;
        }
    }
}
