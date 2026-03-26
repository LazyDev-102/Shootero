using Gemmob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraSystemManager : MonoBehaviour {
    [SerializeField] private AuraOffenseManager auraOffenseManager;

    public AuraOffenseManager AuraOffenseManager { get => auraOffenseManager; }

    [SerializeField] private ShipBase ship;
    private EnemyBase enemy;
    private float percentDamageAuraOffense;
    private HitInfor shipHit;
    private void Awake() {
        EventDispatcher.Instance.AddListener<EventKey.OnAuraHitDamage>(OnAuraHitDamage);
        shipHit = new HitInfor();
    }
    private void OnDestroy() {
        EventDispatcher.Instance.RemoveListener<EventKey.OnAuraHitDamage>(OnAuraHitDamage);
    }

    public AuraSystemManager InitData(ShipBase ship) {
        this.ship = ship;
        return this;
    }

    public void EnableAuraOffense(float deltaAttack, float percentDamage, float radius) {
        auraOffenseManager.InitData(deltaAttack, radius).TurnOn();
        percentDamageAuraOffense = percentDamage;
    }
    public void ChangeRadius(float percentRadiusModifier) {
        auraOffenseManager.ChangeRadius(percentRadiusModifier);
    }
    public void ChangeDamage(float percentDamage) {
        auraOffenseManager.ChangeDamage(percentDamage);
    }
    public void ChangeDeltaShot(float percent) {
        auraOffenseManager.ChangeDeltaShot(percent);
    }
    public void DisableAuraOffense() {
        auraOffenseManager.TurnOff();
    }

    private void OnAuraHitDamage(EventKey.OnAuraHitDamage infor) {
        if (!gameObject.activeInHierarchy || infor.Hit == null || infor.Hit.Causer == null || ship == null)
            return;
        if (infor.Hit.Causer.GetComponent<EnemyBase>() == null)
            return;
        this.enemy = (EnemyBase)infor.Hit.Causer;
        //this.enemy.EnemyHealth.AddHp((int)(PlayerStatManager.Instance.Damage * percentDamageAuraOffense));
        shipHit.SetInfor((int)(ship.ShipStat.Atk.Value * infor.PercentDamage), ship.ShipSkill.EffectAttackMods, ship, ship.ShipStat.CritChance.Value, ship.ShipStat.CritDamage.Value);
        this.enemy.EnemyHitbox.TakeHit(shipHit, transform.position, HitType.Burn);
    }
}
