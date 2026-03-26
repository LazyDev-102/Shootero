

public class B09RefectorAttack : BossAttack {
    private B09RefectorBase b09RefectorBase;

    public B09RefectorBase B09RefectorBase {
        get {
            if(b09RefectorBase == null) {
                b09RefectorBase = BossBase as B09RefectorBase;
            }
            return b09RefectorBase;
        }
    }
}
