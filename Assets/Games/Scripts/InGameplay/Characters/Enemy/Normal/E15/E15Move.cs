

public class E15Move : EnemyMove {
    private E15Base e15Base;
    public E15Base E15Base {
        get {
            if (e15Base == null) {
                e15Base = EnemyBase as E15Base;
            }
            return e15Base;
        }
    }
    protected override void EndMoveAppear() {
        base.EndMoveAppear();
        StopMoveIdle();
    }
}
