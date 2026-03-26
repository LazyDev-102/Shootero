using UnityEngine;

public class SinBounceBullet : SinBullet {
    [SerializeField] private int bounceMax;
    [SerializeField] protected LayerMask enemyMask;
    protected int bounceCount;
    private float speedInit;
    private Collider2D currentCollider;
    public override void Initalize() {
        base.Initalize();
        bounceCount = bounceMax;
    }
    protected override void RemoveMe() {
        base.RemoveMe();
        if (bounceCount > 0 && currentCollider && currentCollider.CompareTag(GameTag.Enemy)) {
            bounceCount--;
            (bool next, Vector2 target) = FindNewTarget();
            if (next) {
                var bullet = GameManager.Instance.GameLoader.SpawnBullet(this, transform.position + Vector3.up * 0.5f);
                bullet = ChangingBullet(bullet, GameManager.Instance.GameLoader.Ship);
                bullet.SetBounceCount(bounceCount);
                bullet.Shoot(speedInit, target);
            }
            else
                bounceCount = 0;
        }
    }
    public override SinBullet Shoot(float speed, Vector2 direction, float amplitude, float cycles, bool r2l = true) {
        speedInit = speed;
        return base.Shoot(speed, direction, amplitude, cycles, r2l);
    }
    public void SetBounceCount(int bounceCount) {
        this.bounceCount = bounceCount;
    }
    public (bool, Vector2) FindNewTarget() {
        if (GameManager.Instance.GameLoader.Enemies.Count == 0) {
            return (false, Vector2.up);
        }
        var radius = 5f;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, radius, Vector2.up, 5f, enemyMask);
        for (int i = 0; i < hits.Length; i++) {
            if (hits[i].collider != currentCollider) {
                return (true, (hits[i].collider.transform.position - transform.position).normalized);
            }
        }
        return (false, Vector2.up);
    }

    private SinBounceBullet ChangingBullet(SinBounceBullet bullet, ShipBase ship) {
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
}
