

public class E11Move : EnemyMove{
    private E11Base e11Base;
    public E11Base E11Base {
        get {
            if(e11Base == null) {
                e11Base = EnemyBase as E11Base;
            }
            return e11Base;
        }
    }
}
