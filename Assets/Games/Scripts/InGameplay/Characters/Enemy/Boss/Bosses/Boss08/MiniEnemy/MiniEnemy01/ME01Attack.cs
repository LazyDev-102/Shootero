

using Helper;
using UnityEngine;

public class ME01Attack : EnemyAttack {
    [SerializeField] private float maxRadius;
    [SerializeField] private float radiateDuration;
    [SerializeField] private RangeFloatValue fadeRangeValue;
    [SerializeField] private Transform circleAttack;
    [SerializeField] private SpriteRenderer[] circleSprites;
    [SerializeField] private float deltaAttack;

    private Countdowner radiateCountdowner = new Countdowner();
    private Countdowner deltaAttackCountdowner = new Countdowner();


    public override bool CanAttack() {
        return true;
    }

    protected override void Attacking() {
        StartRadiateCircle();
    }

    private void StartRadiateCircle() {
        radiateCountdowner.StartCountdown(radiateDuration);
        ActiveCircle(true);
        circleAttack.Scale(0);
    }

    public void RadiatingCircle() {
        if (isAttacking) {
            if (radiateCountdowner.IsCountdowning()) {
                float ratio = 1 - radiateCountdowner.Countdown / radiateDuration;
                circleAttack.Scale(ratio * maxRadius);
                foreach (var sprite in circleSprites) {
                    sprite.ChangeAlpha(fadeRangeValue.GetRatioValue(ratio));
                }
                radiateCountdowner.Countdowning(Time.deltaTime);

                if (radiateCountdowner.IsTimeOut()) {
                    deltaAttackCountdowner.StartCountdown(deltaAttack);
                }
            }
            if (deltaAttackCountdowner.IsCountdowning()) {
                deltaAttackCountdowner.Countdowning(Time.deltaTime);
                if (deltaAttackCountdowner.IsTimeOut()) {
                    radiateCountdowner.StartCountdown(radiateDuration);
                }
            }

        }
    }

    private void ActiveCircle(bool active) {
        circleAttack.gameObject.SetActive(active);
    }

}
