using UnityEngine;

public class B12ChildAttack : EnemyAttack {
    private B12ChildBase e02Base;
    public B12ChildBase B12ChildBase {
        get {
            if (e02Base == null) {
                e02Base = GetComponent<B12ChildBase>();
            }
            return e02Base;
        }
    }

    [SerializeField] private float aimTime;
    private Countdowner aimCountdowner = new Countdowner();

    public void StartAimTarget() {
        //aimCountdowner.StartCountdown(aimTime);
    }

    protected override void Attacking() {
        //B12ChildBase.B12ChildMove.SetTargetMoveAttack((Vector2)Target.position);
    }

    public override bool CanAttack() {
        //return aimCountdowner.IsTimeOut();
        return false;
    }

    public void AimTarget() {
        //B12ChildBase.LookTarget();
        //aimCountdowner.Countdowning(Time.deltaTime);
    }
}
