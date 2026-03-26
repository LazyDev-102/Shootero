using UnityEngine;
using Gemmob;
using System.Collections;

public class MB03Skill02AttackComponent : MinibossAttackComponent<MB03Attack> {

    [SerializeField] private float delayAttack;
    [SerializeField] private float damagePercent;
    [SerializeField] private int numberShot;
    [SerializeField] private float deltaShot;
    [SerializeField] private FrontBullet bullet;
    [SerializeField] private float speedBullet;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private int numberPreload;

    private Countdowner aimCountdowner = new Countdowner();


    public override void PreloadIngame() {
        if (bullet) {
            bullet.PreloadIngame();
            bullet.RegisterPool(numberPreload);
        }

    }


    public override void Initialize() {
        base.Initialize();
        bullet.RegisterPool(10);
    }

    public void StartAimTarget() {
        aimCountdowner.StartCountdown(1);
    }

    public override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(IShotting());
    }

    private IEnumerator IShotting() {
        if (muzzle) {
            muzzle.Play();
        }
        yield return Yielder.Wait(delayAttack);
        if (muzzle) {
            muzzle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
        FrontBullet newBullet = null;
        for (int i = 0; i < numberShot; ++i) {

            newBullet = GameManager.Instance.GameLoader.SpawnBullet(bullet, transform.position);
            if (newBullet) {
                newBullet = ChangingBullet(newBullet);
                newBullet.SetSize(minibossAttack.MB03Base.MB03Stat.Size.Value);
                newBullet.Shoot(speedBullet, transform.up);
            }
            yield return Yielder.Wait(deltaShot);
        }
        EndAttack();
    }

    public override void Updating() {
        aimCountdowner.Countdowning(Time.deltaTime);
    }

    public void AimTarget() {
        minibossAttack.MB03Base.LookTarget();
    }

    private int atk;
    public override void StartAttack() {
        atk = (int)(minibossAttack.MB03Base.MB03Stat.Atk.Value * damagePercent);
    }

    public T ChangingBullet<T>(T bullet) where T : BulletBase {
        bullet.SetHitInfor(atk, null, minibossAttack.MB03Base);
        return bullet;
    }
}
