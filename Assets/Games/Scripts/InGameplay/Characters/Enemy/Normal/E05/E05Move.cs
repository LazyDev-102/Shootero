

public class E05Move : EnemyMove{
    private E05Base e05Base;
    public E05Base E05Base {
        get {
            if(e05Base == null) {
                e05Base = EnemyBase as E05Base;
            }
            return e05Base;
        }
    }
}
