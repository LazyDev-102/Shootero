using Gemmob;
using Helper;
using System.Collections.Generic;
using UnityEngine;

public class B08Base : BossBase {
    #region References
    private B08Attack b08Attack;
    public B08Attack B08Attack {
        get {
            if (b08Attack == null) {
                b08Attack = BossAttack as B08Attack;
            }
            return b08Attack;
        }
    }

    private B08Move b08Move;
    public B08Move B08Move {
        get {
            if (b08Move == null) {
                b08Move = BossMove as B08Move;
            }
            return b08Move;
        }
    }

    private B08Health b08Health;
    public B08Health B08Health {
        get {
            if (b08Health == null) {
                b08Health = BossHealth as B08Health;
            }
            return b08Health;
        }
    }

    private B08Stat b08Stat;
    public B08Stat B08Stat {
        get {
            if (b08Stat == null) {
                b08Stat = BossStat as B08Stat;
            }
            return b08Stat;
        }
    }

    private B08Hitbox b08Hitbox;
    public B08Hitbox B08Hitbox {
        get {
            if (b08Hitbox == null) {
                b08Hitbox = BossHitbox as B08Hitbox;
            }
            return b08Hitbox;
        }
    }

    private B08Skill b08Skill;
    public B08Skill B08Skill {
        get {
            if (b08Skill == null) {
                b08Skill = BossSkill as B08Skill;
            }
            return b08Skill;
        }
    }
    #endregion

    [SerializeField] private MESpecialB08Base miniEnemyPrefab;
    [SerializeField] private int numberEnemy;
    [SerializeField] private float radius;
    [SerializeField] private float[] delaySpawn;
    [SerializeField] private float delayMove;
    [SerializeField] private float hpPercent;
    [SerializeField] private float atkPercent;
    [SerializeField] private float rotateSpeed;


    private Transform meContainer;
    private List<MESpecialB08Base> enemies = new List<MESpecialB08Base>();
    private Countdowner delaySpawnCountdowner = new Countdowner();
    private Countdowner delayMoveCountdowner = new Countdowner();


    public override void Initialize() {
        base.Initialize();
        meContainer = new GameObject("Container").transform;
        enemies = new List<MESpecialB08Base>();
        SpawnAllMiniEnemy();
        delayMoveCountdowner.StartCountdown(delayMove);
    }

    public override void Destroy() {
        if (enemies != null) {
            foreach (var me in enemies) {
                me.SelfDestruction();
            }
        }

#if UNITY_EDITOR
        GameObject.DestroyImmediate(meContainer.gameObject);

#else
        GameObject.Destroy(meContainer.gameObject);

#endif

        base.Destroy();


    }

    public override void Updating() {
        base.Updating();
        meContainer.transform.position = BossMove.MyRigi.position;
        if (!IsInRageStatus) {
            meContainer.Rotate(Vector3.back, rotateSpeed * Time.deltaTime);
            if (delaySpawnCountdowner.IsCountdowning()) {
                delaySpawnCountdowner.Countdowning(Time.deltaTime);
                if (delaySpawnCountdowner.IsTimeOut() && (!IsInRageStatus || !IsInEffectRage)) {
                    SpawnMiniEnemy();
                    if (!HasFullMiniEnemy()) {
                        delaySpawnCountdowner.StartCountdown(delaySpawn[CurrentPhaseIndex]);
                    }
                }
            }
            delayMoveCountdowner.Countdowning(Time.deltaTime);
            if (delayMoveCountdowner.IsTimeOut() && HasMiniEnemyForChoose()) {
                delayMoveCountdowner.StartCountdown(delayMove);
                ChooseMove();
            }
        }
    }

    public override void StartRage() {
        base.StartRage();
        RemoveAllEnemy();
    }

    public override void EndRage() {
        base.EndRage();
        delaySpawnCountdowner.StartCountdown(delaySpawn[CurrentPhaseIndex]);
    }

    private void ChooseMove() {
        if (enemies == null) {
            return;
        }
        MESpecialB08Base choose = null;
        int count = 0;
        do {
            choose = RandomHelper.RandomInCollection(enemies);
            count++;
        } while (choose.IsDie || choose.IsMoveToTarget && count < 50);
        choose.transform.SetParent(null);
        choose.StartMoveTarget();
    }

    private void SpawnAllMiniEnemy() {
        int hp = (int)(hpPercent * BossStat.MaxHP.Value);
        int atk = (int)(atkPercent * BossStat.Atk.Value);

        for (int i = enemies.Count; i < numberEnemy; i++) {
            MESpecialB08Base newEnemy = miniEnemyPrefab.Spawn(meContainer);
            enemies.Add(newEnemy);
        }
        float deltaAngle = 360f / numberEnemy;

        for (int i = 0; i < numberEnemy; ++i) {
            enemies[i].SetInfo(hp, atk);
            enemies[i].Initialize();
            float curAngle = i * deltaAngle;
            float x = radius * Mathf.Cos(curAngle * Mathf.Deg2Rad);
            float y = radius * Mathf.Sin(curAngle * Mathf.Deg2Rad);
            enemies[i].SetLocalPosition(new Vector2(x, y));
            enemies[i].SetLocalEuler(-90 + curAngle);
            enemies[i].AddOnMEDead(OnEnemyDead);
        }


    }

    private void SpawnMiniEnemy() {
        if (enemies == null) {
            return;
        }
        int hp = (int)(hpPercent * BossStat.MaxHP.Value);
        int atk = (int)(atkPercent * BossStat.Atk.Value);
        if (NoMiniEnemy()) {
            delayMoveCountdowner.StartCountdown(delayMove);
            foreach (var me in enemies) {
                me.SetInfo(hp, atk);
                me.Initialize();
                me.AddOnMEDead(OnEnemyDead);
                me.transform.SetParent(meContainer);
                me.ResetLocal();
                me.gameObject.SetActive(true);
                me.Show();
            }
        }
        else {
            foreach (var me in enemies) {
                if (me.IsDie) {
                    me.SetInfo(hp, atk);
                    me.Initialize();
                    me.AddOnMEDead(OnEnemyDead);
                    me.transform.SetParent(meContainer);
                    me.ResetLocal();
                    me.gameObject.SetActive(true);
                    me.Show();
                    return;
                }
            }
        }
    }

    private void RemoveAllEnemy() {
        foreach (var me in enemies) {
            me.ForceDie();
        }
    }

    private void OnEnemyDead(MESpecialB08Base e) {
        if (delaySpawnCountdowner.IsTimeOut()) {
            delaySpawnCountdowner.StartCountdown(delaySpawn[CurrentPhaseIndex]);
        }
    }

    private bool NoMiniEnemy() {
        foreach (var me in enemies) {
            if (!me.IsDie) {
                return false;
            }
        }
        return true;
    }

    private bool HasFullMiniEnemy() {
        foreach (var me in enemies) {
            if (me.IsDie) {
                return false;
            }
        }
        return true;
    }

    private bool HasMiniEnemyForChoose() {
        foreach (var me in enemies) {
            if (!me.IsDie && !me.IsMoveToTarget) {
                return true;
            }
        }
        return false;
    }
}
