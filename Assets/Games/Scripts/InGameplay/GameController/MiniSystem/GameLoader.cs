using System.Collections.Generic;
using UnityEngine;
using Gemmob;
using System.Collections;

public class GameLoader : MonoBehaviour {
    [SerializeField] private Transform content;
    [SerializeField] private ShipBase ship;
    [SerializeField] private DroneBase drone1;
    [SerializeField] private DroneBase drone2;
    [SerializeField] private List<EnemyBase> enemies = new List<EnemyBase>();
    [SerializeField] private List<TrapBase> traps = new List<TrapBase>();
    [SerializeField] private List<ChestBase> chests = new List<ChestBase>();
    [SerializeField] private List<ObstacleBase> obstacles = new List<ObstacleBase>();

    [SerializeField] private List<BaseDropController> drops = new List<BaseDropController>();




    private List<BulletBase> bullets = new List<BulletBase>();
    private List<ParticleSystem> effectExplosions = new List<ParticleSystem>();
    private List<Explosioner> objectExplosioners = new List<Explosioner>();

    private bool isDestroy;
    private GameManager gameManager;
    private bool isBlockSpawn;

    private GameManager GameManager {
        get {
            if (gameManager == null) {
                gameManager = GameManager.Instance;
            }
            return gameManager;
        }

    }

    public ShipBase Ship {
        get => ship;
    }
    public DroneBase Drone1 {
        get => drone1;
    }
    public DroneBase Drone2 {
        get => drone2;
    }
    public List<EnemyBase> Enemies {
        get => enemies;
    }

    public void Initialize() {
        isDestroy = false;
        isBlockSpawn = false;
    }
    public void Destroy() {
        isDestroy = true;
        StopAllCoroutines();
    }
    public void DeSpawnAll() {
        isBlockSpawn = true;
        DespawnAllDropItem();
        DespawnAllTrap();
        DespawnAllBullet();
        DespawnAllEffectExplosion();
        DespawnAllExplosion();
        //DespawnAllObstacle();
    }
    public void DespawnEndSeason() {
        isBlockSpawn = true;
        if (drone1 != null)
            drone1.SelfDestruction();
        if (drone2 != null)
            drone2.SelfDestruction();
        ship.SelfDestruction();
        DespawnAllEnemy(false);
    }
    public void SpawnShip(ShipBase shipPrefab, Vector2 pos) {
        if (isBlockSpawn)
            return;
        ShipBase ship = shipPrefab.Spawn(transform, pos, false);
        ship.Initialize();
        this.ship = ship;
    }
    public DroneBase SpawnDrone1(DroneBase dronePrefab, Vector2 pos, GearSoftData data) {
        if (isBlockSpawn)
            return null;
        DroneGearHardData droneHardData = data.GearHardData as DroneGearHardData;
        int level = GameResources.Instance.GearInventory.DroneLSlot.CurrentLevel;
        int damage = droneHardData.GetDamage(level);
        int hp = droneHardData.GetHP(level);
        float cooldown = droneHardData.GetCooldown();
        DroneBase drone1 = dronePrefab.Spawn(transform, pos, false);
        drone1.DroneStat.AddModifier(damage, hp, drone1.DroneStat.FireRateInit, cooldown);
        drone1.Initialize();
        drone1.RemoveAllOnDie();
        drone1.AddOnDie(drone1.CalculateSpawnDrone);
        this.drone1 = drone1;
        DroneManager.Instance.SetDroneLeft(drone1);
        return this.drone1;
    }
    public DroneBase SpawnDrone2(DroneBase dronePrefab, Vector2 pos, GearSoftData data) {
        if (isBlockSpawn)
            return null;
        DroneGearHardData droneHardData = data.GearHardData as DroneGearHardData;
        int level = GameResources.Instance.GearInventory.DroneRSlot.CurrentLevel;
        int damage = droneHardData.GetDamage(level);
        int hp = droneHardData.GetHP(level);
        float cooldown = droneHardData.GetCooldown();
        DroneBase drone2 = dronePrefab.Spawn(transform, pos, false);
        drone2.DroneStat.AddModifier(damage, hp, drone2.DroneStat.FireRateInit, cooldown);
        drone2.Initialize();
        drone2.RemoveAllOnDie();
        drone2.AddOnDie(drone2.CalculateSpawnDrone);
        this.drone2 = drone2;
        DroneManager.Instance.SetDroneRight(drone2);
        return this.drone2;
    }
    public EnemyBase GetRandomEnemy() {
        if (enemies.Count != 0) {
            return Helper.RandomHelper.RandomInCollection(enemies);
        }
        return null;
    }
    public Transform GetNearestEnemy(Vector2 origin) {
        if (enemies.Count != 0) {
            float min = 100;
            int index = 0;
            for (int i = 0; i < enemies.Count; i++) {
                float distance = Vector2.Distance(origin, enemies[i].transform.position);
                if (min > distance) {
                    index = i;
                    min = distance;
                }
            }
            return enemies[index].transform;
        }
        return null;
    }
    public int EnemyCount() {
        if (enemies != null) {
            return enemies.Count;
        }
        return -1;
    }
    public int ChestCount() {
        if (chests != null) {
            return chests.Count;
        }
        return -1;
    }

    public T SpawnEnemy<T>(T e, Vector3 position) where T : EnemyBase {
        if (isBlockSpawn)
            return null;
        T newEnemy = e.Spawn(content, position);
        if (newEnemy == null)
            Logs.LogError("Add Enemy Null");
        enemies.Add(newEnemy);
        return newEnemy;
    }
    public void DespawnEnemy<T>(T e, bool cheat = false) where T : EnemyBase {
        if (enemies.Contains(e)) {
            enemies.Remove(e);
        }
        e.Destroy();
        EnemyInfo eInfo = new EnemyInfo() { score = e.Score };
        e.Recycle();
        if (!cheat)
            GameManager.RemoveEnemy(eInfo);
    }
    public void DespawnAllEnemy(bool cheat) {
        for (int i = enemies.Count - 1; i >= 0; --i) {
            DespawnEnemy(enemies[i], cheat);
        }
    }
    public T SpawnDropItem<T>(T item, Vector3 position) where T : BaseDropController {
        if (isBlockSpawn)
            return null;
        T newDrop = item.Spawn(content, position);
        drops.Add(newDrop);
        return newDrop;
    }
    public void DespawnDropItem<T>(T item) where T : BaseDropController {
        if (drops.Contains(item)) {
            drops.Remove(item);
        }
        item.Recycle();
    }
    public void DespawnAllDropItem() {
        for (int i = drops.Count - 1; i >= 0; --i) {
            DespawnDropItem(drops[i]);
        }
    }
    public T SpawnTrap<T>(T trap, Vector2 position) where T : TrapBase {
        if (isBlockSpawn)
            return null;
        T newTrap = trap.Spawn(content, position);
        traps.Add(newTrap);
        return newTrap;
    }
    public void DespawnTrap<T>(T trap) where T : TrapBase {
        if (traps.Contains(trap)) {
            traps.Remove(trap);
        }
        if (trap != null) {
            trap.Destroy();
            trap.Recycle();
        }
    }
    public void DespawnAllTrap() {
        for (int i = traps.Count - 1; i >= 0; --i) {
            DespawnTrap(traps[i]);
        }
    }
    public T SpawnObstacle<T>(T obs, Vector2 position) where T : ObstacleBase {
        if (isBlockSpawn)
            return null;
        T newObs = obs.Spawn(content, position);
        obs.Initialize();
        obstacles.Add(newObs);
        return newObs;
    }
    public void DespawnObstacle<T>(T obs) where T : ObstacleBase {
        if (obstacles.Contains(obs)) {
            obstacles.Remove(obs);
        }
        obs.Destroy();
        obs.Recycle();
    }
    public void DespawnAllObstacle() {
        for (int i = obstacles.Count - 1; i >= 0; --i) {
            DespawnObstacle(obstacles[i]);
        }
    }
    public T SpawnChest<T>(T chest, Vector2 position) where T : ChestBase {
        if (isBlockSpawn)
            return null;
        T newChest = chest.Spawn(content, position);
        chests.Add(newChest);
        return newChest;
    }
    public void DespawnChest<T>(T chest) where T : ChestBase {
        if (chests.Contains(chest)) {
            chests.Remove(chest);
        }
        chest.Destroy();
        chest.Recycle();
        GameManager.RemoveChest();
    }
    public void DespawnAllChest() {
        for (int i = chests.Count - 1; i >= 0; --i) {
            DespawnChest(chests[i]);
        }
    }
    public T Instantiate<T>(string name) where T : Component {
        if (isBlockSpawn)
            return null;
        GameObject newObj = new GameObject(name);
        newObj.transform.parent = content;
        newObj.transform.position = Vector3.zero;
        return newObj.AddComponent<T>();
    }
    public T SpawnBullet<T>(T bullet, Vector3 position) where T : BulletBase {
        if (isBlockSpawn)
            return null;
        T newBullet = bullet.Spawn(content, position);
        if (newBullet) {
            newBullet.Initalize();
            bullets.Add(newBullet);
        }
        return newBullet;
    }
    public void RemoveBullet<T>(T bullet) where T : BulletBase {
        if (bullet) {
            if (bullets.Contains(bullet)) {
                bullets.Remove(bullet);
                bullet.Recycle();
            }
        }
    }
    public void DespawnAllBullet() {
        for (int i = bullets.Count - 1; i >= 0; --i) {
            RemoveBullet(bullets[i]);
        }
    }
    public void DespawnAllEnemyBullet() {
        for (int i = bullets.Count - 1; i >= 0; --i) {
            if (bullets[i].gameObject.layer == GameLayer.EnemyBulletIndex || bullets[i].tag.Equals(GameTag.EnemyBullet))
                RemoveBullet(bullets[i]);
        }
        if (UbhObjectPool.instance != null)
            UbhObjectPool.instance.RemoveAllBullet();
    }
    public void SpawnEffectExplosions(ParticleSystem explosion, Vector2 position, float number, float radius, float delta) {
        if (isBlockSpawn)
            return;
        StartCoroutine(ISpawnEffectExplosions(explosion, position, number, radius, delta));
    }
    private IEnumerator ISpawnEffectExplosions(ParticleSystem explosion, Vector2 position, float number, float radius, float delta) {
        for (int i = 0; i < number; ++i) {
            if (isDestroy || isBlockSpawn) {
                yield break;
            }
            Vector2 pos = position + Random.insideUnitCircle * radius;
            SpawnEffectExplosion(explosion, pos);
            yield return Yielder.Wait(delta);
        }
    }
    public ParticleSystem SpawnEffectExplosion(ParticleSystem explosion, Vector2 position) {
        if (isBlockSpawn)
            return null;
        ParticleSystem newExplosion = explosion.Spawn();
        if (newExplosion) {
            newExplosion.transform.position = position;
            newExplosion.Play();
            effectExplosions.Add(newExplosion);
        }
        return newExplosion;
    }
    public void DeSpawnEffectExplosion(ParticleSystem explosion) {
        if (effectExplosions.Contains(explosion)) {
            effectExplosions.Remove(explosion);
        }
        explosion.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        explosion.Recycle();
    }
    public void DespawnAllEffectExplosion() {
        for (int i = effectExplosions.Count - 1; i >= 0; --i) {
            DeSpawnEffectExplosion(effectExplosions[i]);
        }
    }
    public T SpawnExplosion<T>(T explosion, Vector3 position) where T : Explosioner {
        if (isBlockSpawn)
            return null;
        T newExplosion = explosion.Spawn(content, position);
        if (newExplosion) {
            newExplosion.Initialize();
            objectExplosioners.Add(newExplosion);
        }
        return newExplosion;
    }
    public void DespawnExplosion<T>(T explosion) where T : Explosioner {
        if (explosion) {
            if (objectExplosioners.Contains(explosion)) {
                objectExplosioners.Remove(explosion);
                explosion.Destroyed();
                explosion.Recycle();
            }
        }
    }
    public void DespawnAllExplosion() {
        for (int i = objectExplosioners.Count - 1; i >= 0; --i) {
            DespawnExplosion(objectExplosioners[i]);
        }
    }
}


