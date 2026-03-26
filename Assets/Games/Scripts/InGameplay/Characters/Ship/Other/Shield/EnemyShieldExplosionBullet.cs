using UnityEngine;

public class EnemyShieldExplosionBullet : MonoBehaviour, IHitbox {

    [SerializeField] private CircleCollider2D explosionCollider;
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private int damageInit;
    [SerializeField] private int radiusInit;
    private IntStat damage = new IntStat();
    private FloatStat radius = new FloatStat();
    private bool initialized;
    public EnemyShieldExplosionBullet InitData() {
        damage.SetBaseValue(damageInit);
        radius.SetBaseValue(radiusInit);
        initialized = true;
        gameObject.SetActive(true);
        if (explosionEffect != null)
            explosionEffect.Play();
        return this;
    }
    public void SetExplosionDamage(StatModifier damage) {
        this.damage.AddModifier(damage);
    }

    public void SetExplosionDamage(int damage) {
        this.damage.SetBaseValue(damage);
    }

    public void SetExplosionRadius(StatModifier radius) {
        this.radius.AddModifier(radius);
        explosionCollider.radius = this.radius.Value;
        if (explosionEffect != null) {
            SetExplosionEffectSize(explosionEffect.main.startSize.constant + explosionEffect.main.startSize.constant * radius.Value);
        }
    }

    private void SetExplosionEffectSize(float value) {
        var e = explosionEffect.main;
        e.startSize = value;
        var eChilds = explosionEffect.GetComponentsInChildren<ParticleSystem>();
        foreach (var item in eChilds) {
            var eChild = item.main;
            eChild.startSize = value;
        }
    }
    public void TakeHit(HitInfor hit, Vector2 positionCollider, HitType type = HitType.Normal) {
        if (!initialized || hit == null || hit.Causer == null || hit.Causer.GetComponent<ShipBase>() == null)
            return;
        ShipBase ship = (ShipBase)hit.Causer;
        ship.ShipHealth.AddHp(-damage.Value);
    }

    public Transform Transform() {
        return transform;
    }
}
