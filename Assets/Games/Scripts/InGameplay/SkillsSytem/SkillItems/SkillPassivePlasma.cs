
using Gemmob;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillPassivePlasma", menuName = "Resource/HardData/Skill/SkillPassivePlasma")]
public class SkillPassivePlasma : ItemSkillData {
    [SerializeField] private PierceLaser laserPrefab;
    [SerializeField] private int laserSpeed = 10;
    [SerializeField] private int angle = 30;
    [SerializeField] private ParticleSystem effect;

    private float fireRate = 5f;
    private float duration = 1f;
    private float deltaShot = 0.1f;
    private float percentDamage = 1;

    private PierceLaser laserLeft;
    private PierceLaser laserRight;
    private ParticleSystem leftEffect;
    private ParticleSystem rightEffect;
    private float timeOffPoint;
    private bool isPlayingEffect;
    private bool isIniting;

    private Countdowner fireRateCd = new Countdowner();
    private Countdowner durationCd = new Countdowner();
    private Countdowner deltaShotCd = new Countdowner();

    public override void Preload() {
        if (effect != null) {
            effect.RegisterPool(1);
        }
        if (laserPrefab != null) {
            laserPrefab.RegisterPool(2);
        }
    }

    private void SetStatData() {
        deltaShot = GetStat(SkillRankItemType.DeltaShot);
        percentDamage = GetStat(SkillRankItemType.PercentDamage);
        duration = GetStat(SkillRankItemType.Duration);
        fireRate = GetStat(SkillRankItemType.FireRate);
    }
    public override void StartAttack(ShipBase ship) {
        base.StartAttack(ship);
        SetStatData();
        ResetData();
        SpawnLaser(ship);
        PlayEffect();
        canAttack = true;
    }
    public override void EndAttack(ShipBase ship) {
        base.EndAttack(ship);
        canAttack = false;
        ResetData();
    }
    public override void Updating() {
        if (canAttack) {
            if (fireRateCd.IsTimeOut()) {
                BeamingLaser();
            }
            else {
                fireRateCd.Countdowning(Time.deltaTime);
            }
        }
    }
    public override void ResetData() {
        if (laserLeft != null)
            laserLeft.Recycle();
        if (laserRight != null)
            laserRight.Recycle();
    }

    private void SpawnLaser(ShipBase ship) {
        timeOffPoint = duration * .2f;
        durationCd.StartCountdown(duration);
        SpawnLaser();
    }
    private void BeamingLaser() {
        durationCd.Countdowning(Time.deltaTime);
        InitLaserStat();
        if (durationCd.IsCountdowning()) {
            Draw();
            Shot();
        }
        else {
            EndBeam();
        }
    }
    private void InitLaserStat() {
        if (isIniting)
            return;
        isIniting = true;
        if(laserLeft != null) {
            laserLeft.gameObject.SetActive(true);
            laserLeft.transform.position = ship.transform.position;
        }
        if(laserLeft != null) {
            laserRight.gameObject.SetActive(true);
            laserRight.transform.position = ship.transform.position;
        }
    }
    private void Draw() {
        float timeOffElapsed = durationCd.Countdown;
        if (timeOffElapsed < timeOffPoint) {
            float percentSize = timeOffPoint == 0 ? 1 : timeOffElapsed / timeOffPoint;
            laserLeft.SetPercentSize(percentSize);
            laserRight.SetPercentSize(percentSize);

        }
    }
    private void Shot() {
        PlayEffect();
        if (deltaShotCd.IsTimeOut()) {
            laserLeft.SetInfor((int)(ship.ShipStat.Atk.Value * percentDamage), null);
            laserRight.SetInfor((int)(ship.ShipStat.Atk.Value * percentDamage), null);
            laserLeft.Beaming(true);
            laserRight.Beaming(true);
            deltaShotCd.StartCountdown(deltaShot);
        }
        else {
            deltaShotCd.Countdowning(Time.deltaTime);
            laserLeft.Beaming(false);
            laserRight.Beaming(false);
        }
    }
    private void EndBeam() {
        try {
            durationCd.StartCountdown(duration);
            fireRateCd.StartCountdown(fireRate);
            laserLeft.EndBeam();
            laserRight.EndBeam();
            laserLeft.gameObject.SetActive(false);
            laserRight.gameObject.SetActive(false);
            if (leftEffect != null)
                leftEffect.Recycle();
            if (rightEffect != null)
                rightEffect.Recycle();
            isPlayingEffect = false;
            isIniting = false;
        }
        catch {
            durationCd.StartCountdown(duration);
            fireRateCd.StartCountdown(fireRate);
            laserLeft.EndBeam();
            laserRight.EndBeam();
            isPlayingEffect = false;
            isIniting = false;
        }
    }
    private void PlayEffect() {
        if (isPlayingEffect)
            return;
        isPlayingEffect = true;
        if (effect != null && laserLeft!= null && laserRight != null) {
            leftEffect = effect.Spawn(laserLeft.transform);
            leftEffect.transform.localEulerAngles = Vector3.right * -90;

            rightEffect = effect.Spawn(laserRight.transform);
            rightEffect.transform.localEulerAngles = Vector3.right * -90;

            leftEffect.transform.localPosition = Vector3.zero;
            rightEffect.transform.localPosition = Vector3.zero;
            leftEffect.Play();
            rightEffect.Play();
        }
    }

    private void SpawnLaser() {
        laserLeft = laserPrefab.Spawn(ship.transform.position);
        laserLeft.transform.localEulerAngles = Vector3.forward * -angle;
        laserLeft.gameObject.SetActive(true);
        laserLeft.SetPercentSize(1);
        laserLeft.StartBeam();

        laserRight = laserPrefab.Spawn(ship.transform.position);
        laserRight.transform.localEulerAngles = Vector3.forward * angle;
        laserRight.gameObject.SetActive(true);
        laserRight.SetPercentSize(1);
        laserRight.StartBeam();
    }

    public override string GetDescriptionByIndex(int index) {
        return string.Format(Description,
                            GetStat(SkillRankItemType.PercentDamage, index) * 100,
                            GetStat(SkillRankItemType.DeltaShot, index));
    }
    protected override string GetCurrentDescription() {
        return string.Format(Description,
                            GetStat(SkillRankItemType.PercentDamage) * 100,
                            GetStat(SkillRankItemType.DeltaShot));
    }
    protected override string GetNextDescription() {
        return string.Format(Description,
                            $"{GetStat(SkillRankItemType.PercentDamage) * 100}<color=green>({ GetNextStat(SkillRankItemType.PercentDamage) * 100})</color>",
                            $"{GetStat(SkillRankItemType.DeltaShot)}<color=green>({GetNextStat(SkillRankItemType.DeltaShot)})</color>");
    }
}