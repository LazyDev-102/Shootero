

public class E17Move : EnemyMove {
    private E17Base e17Base;
    public E17Base E17Base {
        get {
            if (e17Base == null) {
                e17Base = EnemyBase as E17Base;
            }
            return e17Base;
        }
    }
    protected override void EndMoveAppear() {
        base.EndMoveAppear();
        StopMoveIdle();
    }
}
