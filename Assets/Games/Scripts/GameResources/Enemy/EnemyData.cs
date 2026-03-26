using Gemmob;
using Helper;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Resource/HardData/EnemyData/EnemyData")]
public class EnemyData : ScriptableObject {
    [SerializeField] private TrapSpecies[] traps;
    [SerializeField] private ChestBase[] chests;
    [SerializeField] private ObstacleSpecies[] obstacles;
    private ZoneEnemies[] zoneEnemies;
    private CacheLoader<BossPrefabData> bossData = new CacheLoader<BossPrefabData>("EnemyData/BossPrefabData");
    private CacheLoader<MinibossPrefabData> minibossData = new CacheLoader<MinibossPrefabData>("EnemyData/MinibossPrefabData");

    private readonly string pathEnemy = "EnemyData/ZoneEnemies";

    public TrapSpecies[] Traps { get => traps; }
#if CHEAT
    public ZoneEnemies[] ZoneEnemies { get => zoneEnemies; }
#endif
    public BossPrefabData BossDatas => bossData.GetRef();
    public MinibossPrefabData MinibossDatas => minibossData.GetRef();
    #region Preload
    public EnemyData PreloadEnemies(int zoneIndex, bool isLoadAll) {
        LoadEnemyPrefab(zoneIndex);
        if (zoneEnemies[zoneIndex] != null)
            zoneEnemies[zoneIndex].Preload(isLoadAll);
        return this;
    }
    public EnemyData PreloadEnemies(int zoneIndex, int[] enemyIds) {
        LoadEnemyPrefab(zoneIndex);
        if (zoneEnemies[zoneIndex] != null)
            zoneEnemies[zoneIndex].Preload(enemyIds);
        return this;
    }
    public EnemyData PreloadBoss(int[] bossIds, int numberPreload) {
        foreach (int id in bossIds) {
            BossDatas.GetBoss(id - 1).PreloadIngame();
            BossDatas.GetBoss(id - 1).RegisterPool(numberPreload);
        }
        return this;
    }
    public EnemyData PreloadMiniboss(int[] minibossIds, int numberPreload) {
        foreach (int id in minibossIds) {
            MinibossDatas.GetMiniboss(id - 1).PreloadIngame();
            MinibossDatas.GetMiniboss(id - 1).RegisterPool(numberPreload);
        }
        return this;
    }
    public EnemyData PreloadTrap(int[] trapIds, int numberPreload) {
        foreach (int id in trapIds) {
            foreach (var t in Traps[id - 1].traps) {
                t.PreloadIngame();
                t.RegisterPool(numberPreload);
            }
        }
        return this;
    }
    public EnemyData PreloadChest(int[] chestIds, int numberPreload) {
        foreach (int id in chestIds) {
            GetChest(id - 1).PreloadIngame();
            GetChest(id - 1).RegisterPool(numberPreload);
        }
        return this;
    }
    #endregion
    private void LoadEnemyPrefab(int zoneIndex) {
        if (zoneEnemies == null || zoneEnemies.Length == 0)
            zoneEnemies = new ZoneEnemies[Constant.ZoneCount];
        if (zoneEnemies[zoneIndex] == null) {
            zoneEnemies[zoneIndex] = Resources.Load<ZoneEnemies>(pathEnemy + (zoneIndex + 1));
        }
    }


    public EnemyBase GetEnemyBaseRandom(int[] indexs, EnemyType type, int zoneIndex) {
        LoadEnemyPrefab(zoneIndex);
        if (zoneEnemies[zoneIndex] != null)
            return zoneEnemies[zoneIndex].GetEnemyBaseRandom(indexs, type);
        return null;
    }
    public EnemyBase GetEnemyBaseRandom(int[] indexs, EnemyType type, int zoneIndex, int maxLength) {
        LoadEnemyPrefab(zoneIndex);
        if (zoneEnemies[zoneIndex] != null)
            return zoneEnemies[zoneIndex].GetEnemyBaseRandom(indexs, type, maxLength);
        return null;
    }

    public BossBase GetBossByIndex(int index) {
        return BossDatas.GetBoss(index);
    }

    public Color GetBossBGColor(BossBase boss) {
        return BossDatas.GetBossBGColor(boss.BossIndex);
    }

    public MinibossBase GetMiniBossByIndex(int index) {
        return MinibossDatas.GetMiniboss(index);
    }

    public Color GetMiniBossBGColor(MinibossBase miniboss) {
        return MinibossDatas.GetMinibossBGColor(miniboss.MinibossIndex);
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
    private ObstacleSpecies GetObstacleSpeciesRandom(MaterialWaveObstacle[] obstacleDatas) {
        List<ObstacleSpecies> es = GetObstacleSpeciesByIndex(obstacleDatas);
        return RandomHelper.RandomInCollection(es);
    }
    public List<ObstacleSpecies> GetObstacleSpeciesByIndex(MaterialWaveObstacle[] obstacleDatas) {
        List<ObstacleSpecies> es = new List<ObstacleSpecies>();
        for (int i = 0; i < obstacleDatas.Length; ++i) {
            es.Add(obstacles[0]);
        }
        return es;
    }
    public TrapBase GetTrapRandom(int[] indexs, EnemyType type) {
        TrapSpecies species = GetTrapSpeciesRandom(indexs);
        foreach (TrapBase enemyBase in species.traps) {
            if (enemyBase.Type == type) {
                return enemyBase;
            }
        }
        return null;
    }
    public ObstacleBase GetObstaclesRandom(MaterialWaveObstacle[] obstacleDatas, MaterialModeBuffShape shape) {
        ObstacleSpecies obstacle = GetObstacleSpeciesRandom(obstacleDatas);
        return obstacle.ObstacleBases[(int)shape];
    }

    public ChestBase GetChest(int index) {
        if (index >= chests.Length)
            index = 0;
        return chests[index];
    }

    [Serializable]
    public class TrapSpecies {
        public TrapBase[] traps;
    }
    [Serializable]
    public class ObstacleSpecies {
        public ObstacleBase[] ObstacleBases;
    }


    [Serializable]
    public class MinibossData {
        public Color minibossBG;
        public MinibossBase minibossBase;
    }
}
