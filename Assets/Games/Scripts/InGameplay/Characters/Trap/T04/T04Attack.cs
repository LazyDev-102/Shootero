using Helper;
using UnityEngine;

public class T04Attack : TrapAttack {
    [SerializeField] private float deltaAttack;
    [SerializeField] private Collider2D myCollider;


    private Countdowner deltaAttackCountdowner = new Countdowner();
    public override void Initialize() {
        base.Initialize();
        deltaAttackCountdowner.StartCountdown(deltaAttack);
    }

    public override void Updating() {
        base.Updating();
        deltaAttackCountdowner.Countdowning(Time.deltaTime);
        if (deltaAttackCountdowner.IsTimeOut()) {
            myCollider.enabled = true;
            this.DelayFrame(2, () => {
                deltaAttackCountdowner.StartCountdown(deltaAttack);
                myCollider.enabled = false;
            });
        }
    }
}
