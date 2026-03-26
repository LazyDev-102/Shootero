

public class E06Move : EnemyMove {
    private E06Base e06Base;
    public E06Base E06Base {
        get {
            if(e06Base == null) {
                e06Base = EnemyBase as E06Base;
            }
            return e06Base;
        }
    }
}
