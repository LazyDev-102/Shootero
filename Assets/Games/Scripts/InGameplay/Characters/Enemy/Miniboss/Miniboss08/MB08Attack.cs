
public class MB08Attack : MinibossAttack {
    private MB08Base mb08Base;
    public MB08Base MB08Base {
        get {
            if (mb08Base == null) {
                mb08Base = EnemyBase as MB08Base;
            }
            return mb08Base;
        }
    }
}
