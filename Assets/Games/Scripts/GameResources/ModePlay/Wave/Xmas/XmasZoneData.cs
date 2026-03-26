
using Gemmob;
using Helper;
using System;
using System.Collections.Generic;
using UnityEngine;
using static EnemyData;
using static ZoneEnemies;

[CreateAssetMenu(fileName = "XmasZoneData", menuName = "Resource/Modes/Xmas/XmasZoneData")]
public class XmasZoneData : ScriptableObject {

    [Header("Monster")]
    [SerializeField] private EnemyClassification[] enemies;
    [SerializeField] private MinibossPrefabData.MinibossData[] minibosss;
    [SerializeField] private BossPrefabData.BossData[] bosses;
    [SerializeField] private TrapSpecies[] traps;

    [Space, Header("Preload")]
    [SerializeField] private int minibossNumberPreload;
    [SerializeField] private int bossNumberPreload;
    [SerializeField] private int trapNumberPreload;

    #region Preload

    public void PreloadIngame() {
        PreloadEnemies();
        PreloadBoss();
        PreloadMiniboss();
        PreloadTrap();
    }
    private void PreloadEnemies() {
        if (enemies == null)
            return;
        for (int i = 0; i < enemies.Length; i++) {
            for (int j = 0; j < enemies[i].E.Length; j++) {
                enemies[i].E[j].PreloadIngame();
                enemies[i].E[j].RegisterPool(10);
            }
        }
    }
    private void PreloadMiniboss() {
        if (minibosss == null)
            return;
        for (int i = 0; i < minibosss.Length; i++) {
            minibosss[i].minibossBase.PreloadIngame();
            minibosss[i].minibossBase.RegisterPool(minibossNumberPreload);
        }
    }
    private void PreloadBoss() {
        if (bosses == null)
            return;
        for (int i = 0; i < bosses.Length; i++) {
            bosses[i].bossBase.PreloadIngame();
            bosses[i].bossBase.RegisterPool(bossNumberPreload);
        }
    }
    private void PreloadTrap() {
        if (traps == null)
            return;
        for (int i = 0; i < traps.Length; i++) {
            for (int j = 0; j < traps[i].traps.Length; j++) {
                traps[i].traps[j].PreloadIngame();
                traps[i].traps[j].RegisterPool(trapNumberPreload);
            }
        }
    }
    #endregion

    #region Get Trap

    public TrapBase GetTrapRandom(int[] indexs, EnemyType type) {
        TrapSpecies species = GetTrapSpeciesRandom(indexs);
        foreach (TrapBase enemyBase in species.traps) {
            if (enemyBase.Type == type) {
                return enemyBase;
            }
        }
        return null;
    }

    private TrapSpecies GetTrapSpeciesRandom(int[] indexs) {
        List<TrapSpecies> es = GetTrapSpeciesByIndex(indexs);
        return RandomHelper.RandomInCollection(es);
    }

    public List<TrapSpecies> GetTrapSpeciesByIndex(int[] indexs) {
        List<TrapSpecies> es = new List<TrapSpecies>();
        for (int i = 0; i < indexs.Length; ++i) {
            es.Add(traps[indexs[i] - 1]);
        }
        return es;
    }
    #endregion

    #region Get Enemy
    public EnemyBase GetEnemyBaseRandom(int[] indexs, EnemyType type) {
        EnemyClassification species = enemies[RandomHelper.RandomInCollection(indexs) - 1];
        foreach (EnemyBase enemyBase in species.E) {
            if (enemyBase.Type == type) {
                return enemyBase;
            }
        }
        return null;
    }
    #endregion

    #region Get MiniBoss
    public MinibossBase GetMiniBossByIndex(int index) {
        return minibosss[index].minibossBase;
    }
    public Color GetMiniBossBGColor(MinibossBase miniboss) {
        return minibosss[miniboss.MinibossIndex].minibossBG;
    }
    #endregion

    #region Get Boss
    public BossBase GetBossByIndex(int index) {
        return bosses[index].bossBase;
    }
    public Color GetBossBGColor(BossBase boss) {
        return bosses[boss.BossIndex].bossBG;
    }
    #endregion
}
