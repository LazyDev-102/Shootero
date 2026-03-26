

using UnityEngine;

public class E04Attack : EnemyAttack {
    private E04Base e04Base;
    public E04Base E04Base {
        get {
            if(e04Base == null) {
                e04Base = GetComponent<E04Base>();
            }
            return e04Base;
        }
    }

    [SerializeField] private float aimTime;
    private Countdowner aimCountdowner = new Countdowner();

    public void StartAimTarget() {
        aimCountdowner.StartCountdown(aimTime);
    }

    public void AimTarget() {
        aimCountdowner.Countdowning(Time.deltaTime);
    }

    public override bool CanAttack() {
        return aimCountdowner.IsTimeOut();
    }

    protected override void Attacking() {
        E04Base.E04Move.SetTargetMoveAttack((Vector2)Target.position);
    }



}
