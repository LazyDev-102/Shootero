using UnityEngine;

public class E02Attack : EnemyAttack {
    private E02Base e02Base;
    public E02Base E02Base {
        get {
            if (e02Base == null) {
                e02Base = GetComponent<E02Base>();
            }
            return e02Base;
        }
    }

    [SerializeField] private float aimTime;
    private Countdowner aimCountdowner = new Countdowner();

    public void StartAimTarget() {
        aimCountdowner.StartCountdown(aimTime);
    }

    protected override void Attacking() {
        E02Base.E02Move.SetTargetMoveAttack((Vector2)Target.position);
    }

    public override bool CanAttack() {
        return aimCountdowner.IsTimeOut();
    }

    public void AimTarget() {
        E02Base.LookTarget();
        aimCountdowner.Countdowning(Time.deltaTime);
    }
}
