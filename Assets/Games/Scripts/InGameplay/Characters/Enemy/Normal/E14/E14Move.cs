

public class E14Move : EnemyMove {
    private E14Base e14Base;
    public E14Base E14Base {
        get {
            if (e14Base == null) {
                e14Base = EnemyBase as E14Base;
            }
            return e14Base;
        }
    }
    protected override void EndMoveAppear() {
        base.EndMoveAppear();
        StopMoveIdle();
    }
}
