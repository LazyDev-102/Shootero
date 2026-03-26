

public class E13Move : EnemyMove {
    private E13Base e13Base;
    public E13Base E13Base {
        get {
            if (e13Base == null) {
                e13Base = EnemyBase as E13Base;
            }
            return e13Base;
        }
    }
    protected override void EndMoveAppear() {
        base.EndMoveAppear();
        StopMoveIdle();
    }
}
