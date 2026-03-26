using UnityEngine;

public class MB16Skill01AttackComponent : MinibossAttackComponent<MB16Attack> {
    [SerializeField] private MB16Base mb16Base;
    [SerializeField] private ParticleSystem effectFire;
    [SerializeField] private PierceLaser laserBullet;
    [SerializeField] private int laserLength = 5;
    [SerializeField] private float damagePercent;
    [SerializeField] private float deltaShot;
    [SerializeField] private float delayAttack;
    [SerializeField] private float duration;
    [SerializeField] private float laserSize;

    private Countdowner delayCD = new Countdowner();
    private Countdowner durantionCD = new Countdowner();
    private Countdowner deltaShotCountdowner = new Countdowner();

    public override void StartAttack() {
    }
    public override void Attacking() {
        delayCD.StartCountdown(delayAttack);
        deltaShotCountdowner.StartCountdown(deltaShot);
        durantionCD.StartCountdown(duration);
    }
    public override void Updating() {
        if (delayCD.IsCountdowning()) {
            mb16Base.LookTarget();
            delayCD.Countdowning(Time.deltaTime);
        }
        else {
            if (!effectFire.isPlaying && effectFire != null) {
                if (effectFire != null)
                    effectFire.Play();
                StartBeamLaser();
            }
            BeamingLaser();
        }
    }

    private void StartBeamLaser() {
        laserBullet.StartBeam();
        laserBullet.SetMaxLength(laserLength);
        laserBullet.gameObject.SetActive(true);
    }
    private void EndBeamLaser() {
        laserBullet.EndBeam();
        laserBullet.gameObject.SetActive(false);
    }
    public void BeamingLaser() {
        if (durantionCD.IsCountdowning()) {
            durantionCD.Countdowning(Time.deltaTime);
            deltaShotCountdowner.Countdowning(Time.deltaTime);
            if (deltaShotCountdowner.IsTimeOut()) {
                laserBullet.SetInfor((int)(mb16Base.MB16Stat.Atk.Value * damagePercent), null);
                laserBullet.SetRadiusSize(laserSize * mb16Base.MB16Stat.Size.Value);
                laserBullet.Beaming(true);
                deltaShotCountdowner.StartCountdown(deltaShot);
            }
            else {
                laserBullet.Beaming(false);
            }
        }
        else {
            EndBeamLaser();
            if (effectFire != null)
                effectFire.Stop();
            EndAttack();
        }
    }
}
