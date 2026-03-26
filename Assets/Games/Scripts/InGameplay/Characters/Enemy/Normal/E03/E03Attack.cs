using System.Collections;
using UnityEngine;
using Gemmob;

public class E03Attack : EnemyAttack {
    private E03Base e03Base;
    public E03Base E03Base {
        get {
            if (e03Base == null) {
                e03Base = EnemyBase as E03Base;
            }
            return e03Base;
        }
    }

    [SerializeField] private float delayAttack;
    [SerializeField] private int numberShot;
    [SerializeField] private float deltaShot;
    [SerializeField] private FrontBullet bullet;
    [SerializeField] private float speedBullet;
    [SerializeField] private ParticleSystem charge;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private int numberPreload;
    private bool firstAttack;

    private Countdowner aimCountdowner = new Countdowner();


    public override void PreloadIngame() {
        if (bullet) {
            bullet.PreloadIngame();
            bullet.RegisterPool(numberPreload);
        }
    }

    public override void Initialize() {
        base.Initialize();
        isAttacking = false;
        firstAttack = true;
    }

    public void StartAimTarget() {
        aimCountdowner.StartCountdown(firstAttack ? 0.5f : 1f / E03Base.E03Stat.AtkSpeed.Value);
        if (firstAttack) {
            firstAttack = false;
        }
    }

    protected override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(IShotting());
    }

    private IEnumerator IShotting() {
        if (charge) {
            charge.Play();
        }
        yield return Yielder.Wait(delayAttack);
        if (charge) {
            charge.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
        FrontBullet newBullet = null;
        for (int i = 0; i < numberShot; ++i) {
            if (muzzle) {
                muzzle.Play();
            }
            newBullet = GameManager.Instance.GameLoader.SpawnBullet(bullet, transform.position);
            if (newBullet) {
                newBullet = ChangingBullet(newBullet);
                newBullet.SetSize(E03Base.E03Stat.Size.Value);
                newBullet.Shoot(speedBullet, transform.up);
            }
            yield return Yielder.Wait(deltaShot);
        }
        EndAttack();
    }

    public override bool CanAttack() {
        return aimCountdowner.IsTimeOut() && !isAttacking;
    }

    public override void Updating() {
        base.Updating();
        aimCountdowner.Countdowning(Time.deltaTime);
    }

    public void AimTarget() {
        E03Base.LookTarget();
    }

}
