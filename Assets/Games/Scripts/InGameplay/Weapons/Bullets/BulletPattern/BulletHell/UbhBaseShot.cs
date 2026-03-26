using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

/// <summary>
/// Ubh base shot.
/// Each shot pattern classes inherit this class.
/// </summary>
public abstract class UbhBaseShot : BulletBase {
    [Header("===== Common Settings =====")]
    [FormerlySerializedAs("_BulletPrefab")]
    public GameObject m_bulletPrefab;
    [FormerlySerializedAs("_BulletNum")]
    public int m_bulletNum = 10;
    [FormerlySerializedAs("_BulletSpeed")]
    public float m_bulletSpeed = 2f;
    [FormerlySerializedAs("_AccelerationSpeed")]
    public float m_accelerationSpeed = 0f;
    public bool m_useMaxSpeed = false;
    public float m_maxSpeed = 0f;
    public bool m_useMinSpeed = false;
    public float m_minSpeed = 0f;
    [FormerlySerializedAs("_AccelerationTurn")]
    public float m_accelerationTurn = 0f;
    [FormerlySerializedAs("_UsePauseAndResume")]
    public bool m_usePauseAndResume = false;
    [FormerlySerializedAs("_PauseTime")]
    public float m_pauseTime = 0f;
    [FormerlySerializedAs("_ResumeTime")]
    public float m_resumeTime = 0f;
    [FormerlySerializedAs("_UseAutoRelease")]
    public bool m_useAutoRelease = false;
    [FormerlySerializedAs("_AutoReleaseTime")]
    public float m_autoReleaseTime = 10f;

    [Space(10)]

    // "Set a callback method fired shot."
    public UnityEvent m_shotFiredCallbackEvents = new UnityEvent();
    // "Set a callback method after shot."
    public UnityEvent m_shotFinishedCallbackEvents = new UnityEvent();

    protected bool m_shooting;

    private UbhShotCtrl m_shotCtrl;
    private BossAttack bossAttack;
    Action onCompleted;
    private float percentDamage = 1;

    public UbhShotCtrl shotCtrl {
        get {
            if (m_shotCtrl == null) {
                m_shotCtrl = transform.GetComponentInParent<UbhShotCtrl>();
            }
            return m_shotCtrl;
        }
    }

    /// <summary>
    /// is shooting flag.
    /// </summary>
    public bool shooting { get { return m_shooting; } }

    /// <summary>
    /// is lock on shot flag.
    /// </summary>
    public virtual bool lockOnShot { get { return false; } }

    /// <summary>
    /// Call from override OnDisable method in inheriting classes.
    /// Example : protected override void OnDisable () { base.OnDisable (); }
    /// </summary>
    protected virtual void OnDisable() {
        m_shooting = false;
    }

    /// <summary>
    /// Abstract shot method.
    /// </summary>
    public abstract void Shot();

    /// <summary>
    /// UbhShotCtrl setter.
    /// </summary>
    public void SetShotCtrl(UbhShotCtrl shotCtrl) {
        m_shotCtrl = shotCtrl;
    }

    /// <summary>
    /// Fired shot.
    /// </summary>
    protected virtual void FiredShot() {
        m_shotFiredCallbackEvents.Invoke();
    }

    /// <summary>
    /// Finished shot.
    /// </summary>
    public virtual void FinishedShot() {
        m_shooting = false;
        m_shotFinishedCallbackEvents.Invoke();
        onCompleted?.Invoke();
    }

    /// <summary>
    /// Get UbhBullet object from object pool.
    /// </summary>
    protected UbhBullet GetBullet(Vector3 position, bool forceInstantiate = false) {
        if (m_bulletPrefab == null) {
            Debug.LogWarning("Cannot generate a bullet because BulletPrefab is not set.");
            return null;
        }

        // get UbhBullet from ObjectPool
        UbhBullet bullet = UbhObjectPool.instance.GetBullet(m_bulletPrefab, position, forceInstantiate);
        if (bullet == null) {
            return null;
        }

        return bullet;
    }

    /// <summary>
    /// Shot UbhBullet object.
    /// </summary>
    protected void ShotBullet(UbhBullet bullet, float speed, float angle,
                               bool homing = false, Transform homingTarget = null, float homingAngleSpeed = 0f,
                               bool sinWave = false, float sinWaveSpeed = 0f, float sinWaveRangeSize = 0f, bool sinWaveInverse = false) {
        if (bullet == null) {
            return;
        }
        bullet.Shot(this,
                    speed, angle, m_accelerationSpeed, m_accelerationTurn,
                    homing, homingTarget, homingAngleSpeed,
                    sinWave, sinWaveSpeed, sinWaveRangeSize, sinWaveInverse,
                    m_usePauseAndResume, m_pauseTime, m_resumeTime,
                    m_useAutoRelease, m_autoReleaseTime,
                    m_shotCtrl.m_axisMove, m_shotCtrl.m_inheritAngle,
                    m_useMaxSpeed, m_maxSpeed, m_useMinSpeed, m_minSpeed);
    }
    public T ChangingBullet<T>(T bullet) where T : BulletBase {
        bullet.SetHitInfor((int)(bossAttack.BossBase.BossStat.Atk.Value * percentDamage), null, bossAttack.BossBase);

        return bullet;
    }
    public void SetBulletInfo(int count, float bulletSpeed, float accelerationSpeed, bool useMaxSpeed, float maxSpeed, bool useMinSpeed, float minSpeed, float accelerationTurn, bool usePauseAndResume, float pauseTime, float resumeTime, bool useAutoRelease, float autoReleaseTime) {
        m_bulletNum = count;
        m_bulletSpeed = bulletSpeed;
        m_accelerationSpeed = accelerationSpeed;
        m_useMaxSpeed = useMaxSpeed;
        m_maxSpeed = maxSpeed;
        m_useMinSpeed = useMinSpeed;
        m_minSpeed = minSpeed;
        m_accelerationTurn = accelerationTurn;
        m_usePauseAndResume = usePauseAndResume;
        m_pauseTime = pauseTime;
        m_resumeTime = resumeTime;
        m_useAutoRelease = useAutoRelease;
        m_autoReleaseTime = autoReleaseTime;
    }
    public void SetBossAttack(BossAttack bossAttack) {
        this.bossAttack = bossAttack;
    }

    public void SetPercentDamage(float percentDamage) {
        this.percentDamage = percentDamage;
    }

    public void SetCompleted(Action onCompleted) {
        this.onCompleted = onCompleted;
    }
}