using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ShipSatellite : MonoBehaviour {
    [SerializeField] private GameObject oldSprite;
    [SerializeField] private GameObject newSprite;
    [SerializeField] private GameObject fireBallGO;
    [SerializeField] private AutomaticSatelliteBullet attackBullet;
    ShipBase ship;


    private bool isHitEnemy;
    private HitInfor hitInfo;
    private int rangeUpCount = 0;
    private bool isFireball;
    private float damageCollider = 0;
    protected HitInfor HitInfo {
        get {
            if (hitInfo == null) {
                hitInfo = new HitInfor();
            }
            hitInfo.SetInfor((int)(ship.ShipStat.Atk.Value * damageCollider), null, ship);
            return hitInfo;
        }
    }
    public void ChangeColliderDamage(float percent, bool reset = false) {
        damageCollider += percent;
        if (reset)
            damageCollider = percent;
    }
    public void SetShip(ShipBase ship) {
        this.ship = ship;
    }


    public void EnableAssault(float percentDamage) {
        isHitEnemy = true;
        ChangeColliderDamage(percentDamage);
    }
    public IEnumerator EnableAutomatic(float speed) {
        (RaycastHit2D hit, bool finded) = FindEnemy();
        if (finded) {
            if (isFireball)
                fireBallGO.SetActive(false);
            else
                oldSprite.SetActive(false);
            var bullet = GameManager.Instance.GameLoader.SpawnBullet(attackBullet, transform.position);
            AttackAutomatic(bullet, speed, hit, OnAutomaticAttackComplete, isFireball);
        }

        yield return null;
    }
    private void OnAutomaticAttackComplete() {
        if (isFireball)
            fireBallGO.SetActive(true);
        else
            oldSprite.SetActive(true);
    }
    public void EnableFireBall() {
        isFireball = true;
        oldSprite.SetActive(false);
        newSprite.SetActive(true);
        newSprite.transform.localPosition = oldSprite.transform.localPosition;
        newSprite.transform.localScale = Vector3.zero;
        newSprite.transform.DOScale(Vector3.one, 0.5f).SetUpdate(true);
    }

    public void EnableSatelliteRange(float[] range) {
        if (rangeUpCount >= range.Length)
            return;
        transform.localPosition = transform.localPosition * (1 + range[rangeUpCount]);
        //transform.DOMove(transform.position * range[rangeUpCount], 2f);
        rangeUpCount++;
    }
    public void SatelliteRangeSetBaseValue(float value) {
        transform.localPosition = transform.localPosition * value;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (isHitEnemy) {
            if (collision.CompareTag(GameTag.Enemy) || collision.CompareTag(GameTag.EnemyBlockPierce)) {
                IHitbox takeHit = collision.GetComponent<IHitbox>();
                if (takeHit != null) {
                    takeHit.TakeHit(HitInfo, transform.position);
                    if (isFireball) {
                        FireballSatelliteModInfor fireInfo = ship.ShipSkill.FireballMod;//.GetModInfor<FireballSatelliteModInfor>(fireballSatelliteMod.ModId);
                        if (fireInfo != null)
                            fireInfo.FireBallBlast(HitInfo.Causer, ship.ShipStat.Atk.Value, transform.position);
                    }
                }
            }
        }
    }

    private void AttackAutomatic(AutomaticSatelliteBullet bullet, float speed, RaycastHit2D hit, System.Action onComplete, bool isFireBall) {
        bullet.HitInfor.Damage.SetBaseValue(Mathf.CeilToInt(ship.ShipStat.Atk.Value * damageCollider));
        bullet.SetData(hit.transform, speed, onComplete, isFireBall);
    }
    [SerializeField] private LayerMask enemyMask;
    private (RaycastHit2D, bool) FindEnemy() {
        if (GameManager.Instance.GameLoader.Enemies.Count == 0) {
            return (default, false);
        }
        var radius = 3f;
        RaycastHit2D hit = default;
        while (hit == default && radius < 10) {
            hit = Physics2D.CircleCast(transform.position, radius, Vector2.up, 10f, enemyMask);
            radius += 1;
        }
        return (hit, hit != default);
    }

}
