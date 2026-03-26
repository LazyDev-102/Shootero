using Gemmob;
using UnityEngine;

public class FrontBounceBullet : FrontBullet {
    [SerializeField] private int bounceMax;
    [SerializeField] protected LayerMask enemyMask;
    [SerializeField] protected bool useLighting;
    [SerializeField] private Ship07LightningLineBolt lightningLineBolt;
    [SerializeField] private float durationLightningTime = 0.1f;
    protected int bounceCount;
    private float speedInit;
    private Collider2D currentCollider;
    private HitInfor hitBounce;

    public override void Initalize() {
        base.Initalize();
        bounceCount = bounceMax;
    }
    public override void PreloadIngame() {
        base.PreloadIngame();
        if (useLighting)
            lightningLineBolt.RegisterPool(30);
    }
    protected override void RemoveMe() {
        if (bounceCount > 0 && currentCollider && currentCollider.CompareTag(GameTag.Enemy)) {
            bounceCount--;
            (bool next, Vector2 target, GameObject eNext) = FindNewTarget();
            if (next) {
                if (useLighting) {
                    hitBounce = new HitInfor();
                    hitBounce.SetInfor(HitInfor.Damage.Value, HitInfor.Effects, eNext.GetComponent<EnemyBase>(), HitInfor.CritChance, HitInfor.CritDamage);
                    SpawnBullet(gameObject, eNext.GetComponent<EnemyBase>(), bounceCount);
                }
                else {
                    var bullet = GameManager.Instance.GameLoader.SpawnBullet(this, transform.position + Vector3.up * 0.5f);
                    bullet = ChangingBullet(bullet, GameManager.Instance.GameLoader.Ship);
                    bullet.SetBounceCount(bounceCount);
                    bullet.Shoot(speedInit, target);
                }
            }
            else
                bounceCount = 0;
        }
        base.RemoveMe();
    }
    public override void Shoot(float speed, Vector2 direction, float acceleration = 0, float minSpeed = float.MinValue) {
        base.Shoot(speed, direction, acceleration, minSpeed);
        speedInit = speed;
    }
    public void SetBounceCount(int bounceCount) {
        this.bounceCount = bounceCount;
    }
    public (bool, Vector2, GameObject) FindNewTarget() {
        if (GameManager.Instance.GameLoader.Enemies.Count == 0) {
            return (false, Vector2.up, null);
        }
        var radius = 5f;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, radius, Vector2.up, 5f, enemyMask);
        for (int i = 0; i < hits.Length; i++) {
            if (hits[i].collider != currentCollider) {
                return (true, (hits[i].collider.transform.position - transform.position).normalized, hits[i].collider.gameObject);
            }
        }
        return (false, Vector2.up, null);
    }
    private FrontBounceBullet ChangingBullet(FrontBounceBullet bullet, ShipBase ship) {
        if (ship == null)
            return bullet;
        int damage = Mathf.CeilToInt(ship.ShipStat.GetFinalDamageWeapon);
        bullet.SetHitInfor(damage, ship.ShipSkill.EffectAttackMods, ship, ship.ShipStat.CritChance.Value, ship.ShipStat.CritDamage.Value);
        foreach (var mod in ship.ShipSkill.ChangeBulletMods) {
            mod.ChangeBullet(bullet);
        }
        return bullet;
    }
    protected override void OnTriggerEnter2D(Collider2D collision) {
        currentCollider = collision;
        base.OnTriggerEnter2D(collision);
    }

    #region Demo Lighting

    public void SpawnBullet(GameObject source, EnemyBase destination, int bounceCount) {
        if (destination != null) {
            var clone = lightningLineBolt.Spawn(GameManager.Instance.GameLoader.transform);
            destination.EnemyHitbox.TakeHitDamage(HitInfor, transform.position);
            clone.SetHitInfo(hitBounce);
            clone.SetBounceCount(bounceCount);
            clone.SetPosition(clone, source.transform, destination.transform);
        }
    }

    #endregion
}
