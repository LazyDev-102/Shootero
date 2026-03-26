using UnityEngine;
using Gemmob;
using System.Collections;

public class MB06Skill02AttackComponent : MinibossAttackComponent<MB06Attack> {
    [SerializeField] private float delayAttack;
    [SerializeField] private float damagePercent;
    [SerializeField] private int numberShot;
    [SerializeField] private float deltaShot;
    [SerializeField] private FrontBullet bullet;
    [SerializeField] private float speedBullet;
    [SerializeField] private int numberBullet;
    [SerializeField] private float spreadAngle;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private int numberPreload;

    private GameLoader gameLoader1;

    public override void PreloadIngame() {
        if (bullet) {
            bullet.PreloadIngame();
            bullet.RegisterPool(numberPreload);
        }

    }


    public override void Initialize() {
        base.Initialize();
        bullet.RegisterPool(10);
        gameLoader1 = GameManager.Instance.GameLoader;
    }

    public override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(Shoting());
    }

    private IEnumerator Shoting() {
        yield return Yielder.Wait(delayAttack);
        for (int i = 0; i < numberShot; ++i) {
            if (muzzle) {
                muzzle.Play();
            }
            Vector2 directionShot = transform.up;
            FrontBullet centerBullet = gameLoader1.SpawnBullet(bullet, transform.position);
            if (centerBullet) {
                centerBullet = ChangingBullet(centerBullet);
                centerBullet.Shoot(speedBullet, directionShot);
            }
            for (int ibullet = 0; ibullet < numberBullet / 2; ++ibullet) {
                Vector2 leftDirectionShot = Helper.GamePlayHelper.RotateDirection(directionShot, spreadAngle * (ibullet + 1));
                FrontBullet leftBullet = gameLoader1.SpawnBullet(bullet, transform.position);
                if (leftBullet) {
                    leftBullet = ChangingBullet(leftBullet);
                    //leftBullet.SetSize(minibossAttack.MB06Base.MB06Stat.Size.Value);
                    leftBullet.Shoot(speedBullet, leftDirectionShot);
                }

                Vector2 rightDirectionShot = Helper.GamePlayHelper.RotateDirection(directionShot, -1 * spreadAngle * (ibullet + 1));
                FrontBullet rightBullet = gameLoader1.SpawnBullet(bullet, transform.position);
                if (rightBullet) {
                    rightBullet = ChangingBullet(rightBullet);
                    //rightBullet.SetSize(minibossAttack.MB06Base.MB06Stat.Size.Value);
                    rightBullet.Shoot(speedBullet, rightDirectionShot);
                }
            }
            yield return Yielder.Wait(deltaShot);
        }
        EndAttack();
    }

    public void AimTarget() {
        minibossAttack.MB06Base.LookTarget();
    }


    public override void Updating() {

    }

    private int atk;
    public override void StartAttack() {
        atk = (int)(minibossAttack.MB06Base.MB06Stat.Atk.Value * damagePercent);
    }

    public T ChangingBullet<T>(T bullet) where T : BulletBase {
        bullet.SetHitInfor(atk, null, minibossAttack.MB06Base);
        return bullet;
    }
}
