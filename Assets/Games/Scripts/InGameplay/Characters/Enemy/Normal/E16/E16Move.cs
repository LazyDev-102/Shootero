

public class E16Move : EnemyMove {
    private E16Base e16Base;
    public E16Base E16Base {
        get {
            if (e16Base == null) {
                e16Base = EnemyBase as E16Base;
            }
            return e16Base;
        }
    }
    protected override void EndMoveAppear() {
        base.EndMoveAppear();
        StopMoveIdle();
    }
}
