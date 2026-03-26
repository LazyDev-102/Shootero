using UnityEngine;

public class MERageB08Attack : EnemyAttack {
    [SerializeField] private float deltaHealTime;
    [SerializeField] private float valueHealPercent;
    [SerializeField] private LineRenderer lineEffect;
    [SerializeField] private float lineSize;
    [SerializeField] private RangeFloatValue sizeLinePercentRandom;


    private EnemyBase targetEnemy;
    private Countdowner deltaHealCountdowner = new Countdowner();

    public void SetTargetEnemy(EnemyBase enemy) {
        targetEnemy = enemy;
    }

    public void SetValueHeal(float heal) {
        valueHealPercent = heal;
    }

    public override bool CanAttack() {
        return true;
    }

    protected override void Attacking() {
        deltaHealCountdowner.StartCountdown(deltaHealTime);
        lineEffect.SetPosition(0, transform.position);
        lineEffect.SetPosition(1, targetEnemy.transform.position);
        lineEffect.widthMultiplier = lineSize;
    }

    public void Healing() {
        deltaHealCountdowner.Countdowning(Time.deltaTime);
        lineEffect.SetPosition(0, transform.position);
        lineEffect.SetPosition(1, targetEnemy.transform.position);
        lineEffect.widthMultiplier = lineSize * sizeLinePercentRandom.GetRandomValue();
        if (deltaHealCountdowner.IsTimeOut()) {
            Heal();
            deltaHealCountdowner.StartCountdown(deltaHealTime);
        }
    }

    private void Heal() {
        if (targetEnemy) {
            int heal = (int)(valueHealPercent * targetEnemy.CharacterStat.MaxHP.Value);
            targetEnemy.EnemyHealth.AddHp(heal);
        }
    }
}
