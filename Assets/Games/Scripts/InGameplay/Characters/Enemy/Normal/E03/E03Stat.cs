using UnityEngine;

public class E03Stat : EnemyStat{
    private E03Base e03Base;
    public E03Base E03Base {
        get {
            if(e03Base == null) {
                e03Base = EnemyBase as E03Base;
            }
            return e03Base;
        }
    }

    [SerializeField] private FloatStat atkSpeed;

    public FloatStat AtkSpeed { get => atkSpeed; }
}
