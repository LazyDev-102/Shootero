using UnityEngine;
using Helper;
using Gemmob;

public class MB15ChildSkill01AttackComponent : MinibossAttackComponent<MB15ChildAttack> {
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private MB15ChildBase miniEnemyPrefab;
    [SerializeField] private Area leftArea;
    [SerializeField] private Area rightArea;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float damagePercent;
    [SerializeField] private float duration;
    [SerializeField] private float hpPercent;
    [SerializeField] private int numberPreload;


    private bool isEnded;
    private int enemyInited;

    public override void PreloadIngame() {
        if (miniEnemyPrefab) {
            miniEnemyPrefab.PreloadIngame();
            miniEnemyPrefab.RegisterPool(numberPreload);
        }

    }

    public override void StartAttack() {
        isEnded = false;
        enemyInited = 0;
    }

    public override void Updating() {
        minibossAttack.MB15ChildBase.LookTarget();
    }
    public override void Attacking() {
        int hp = (int)(hpPercent * minibossAttack.MB15ChildBase.MB15ChildStat.MaxHP.Value);
        int atk = (int)(damagePercent * minibossAttack.MB15ChildBase.MB15ChildStat.Atk.Value);


        MB15ChildBase newMERight = miniEnemyPrefab.Spawn(transform.position);
        newMERight.SetInfo(hp, atk);
        newMERight.MB15ChildAttack.SetShotDuration(duration);
        newMERight.MB15ChildMove.SetTargetPosition(BorderHelper.GetWorldPointInsideArea(rightArea));
        newMERight.MB15ChildMove.SetRotateSpeed(-1 * rotateSpeed);
        newMERight.Initialize();
        newMERight.AddOnEndBossAttack(EnemyComplete);
        enemyInited++;

    }

    private void EnemyComplete() {
        enemyInited--;
        if (enemyInited <= 0) {
            EndAttack();
        }
    }

    public override void EndAttack() {
        if (isEnded) {
            return;
        }
        isEnded = true;
        base.EndAttack();
    }

    public override void StopAttack() {
        base.StopAttack();
        isEnded = true;
    }
}