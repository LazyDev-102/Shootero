
using System;
using UnityEngine;

public class XB01ShotController : UbhShotCtrl {
    [Header("--------Data--------")]
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    private AttackData ad;
    Action onCompleted;

    private AttackData currentAttackData {
        get {
            if (IngameData.currentGameMode != GameMode.EventBoss)
                return attackDatas[bossAttack.BossBase.CurrentPhaseIndex];
            else
                return bossModeAttackDatas[bossAttack.BossBase.CurrentPhaseIndex];
        }
    }
    private int attackIndex;
    public void SetAttack(int index) {
        attackIndex = index;
    }
    public void SetData() {
        ad = currentAttackData;
    }
    public void SetComplete(Action onCompleted) {
        this.onCompleted = onCompleted;
    }
    public override void UpdateShot(float deltaTime) {
        if (m_shooting == false) {
            return;
        }

        if (m_updateStep == UpdateStep.StartDelay) {
            if (m_delayTimer > 0f) {
                m_delayTimer -= deltaTime;
                return;
            }
            else {
                m_delayTimer = 0f;
                m_updateStep = UpdateStep.StartShot;
            }
        }

        ShotInfo nowShotInfo = m_shotList[attackIndex];

        if (m_updateStep == UpdateStep.StartShot) {
            if (nowShotInfo.m_shotObj != null) {
                nowShotInfo.m_shotObj.SetShotCtrl(this);
                nowShotInfo.m_shotObj.SetBossAttack(bossAttack);
                nowShotInfo.m_shotObj.SetPercentDamage(currentAttackData.PercentDamage);
                nowShotInfo.m_shotObj.SetBulletInfo(ad.NumberBullet, ad.BulletSpeed, ad.AccelerationSpeed, ad.UseMaxSpeed, ad.MaxSpeed, ad.UseMinSpeed, ad.MinSpeed, ad.AccelerationSpeed, ad.UsePauseAndResume, ad.PauseTime, ad.ResumeTime, ad.UseAutoRelease, ad.AutoReleaseTime);
                nowShotInfo.m_shotObj.Shot();
                nowShotInfo.m_shotObj.SetCompleted(onCompleted);
            }

            m_delayTimer = 0f;
            m_updateStep = UpdateStep.WaitDelay;
        }

        if (m_updateStep == UpdateStep.WaitDelay) {
            if (nowShotInfo.m_afterDelay > 0 && nowShotInfo.m_afterDelay > m_delayTimer) {
                m_delayTimer += deltaTime;
            }
            else {
                m_delayTimer = 0f;
                m_updateStep = UpdateStep.UpdateIndex;
            }
        }

        if (m_updateStep == UpdateStep.UpdateIndex) {
            if (m_atRandom) {
                m_randomShotList.RemoveAt(m_nowIndex);

                if (m_loop && m_randomShotList.Count <= 0) {
                    m_randomShotList.AddRange(m_shotList);
                }

                if (m_randomShotList.Count > 0) {
                    m_nowIndex = UnityEngine.Random.Range(0, m_randomShotList.Count);
                    m_updateStep = UpdateStep.StartShot;
                }
                else {
                    m_updateStep = UpdateStep.FinishShot;
                }
            }
            else {
                if (m_loop || m_nowIndex < m_shotList.Count - 1) {
                    m_nowIndex = (int)Mathf.Repeat(m_nowIndex + 1f, m_shotList.Count);
                    m_updateStep = UpdateStep.StartShot;
                }
                else {
                    m_updateStep = UpdateStep.FinishShot;
                }
            }
        }

        if (m_updateStep == UpdateStep.StartShot) {
            UpdateShot(deltaTime);
        }
        else if (m_updateStep == UpdateStep.FinishShot) {
            m_shooting = false;
            m_shotRoutineFinishedCallbackEvents.Invoke();
        }
    }
    [System.Serializable]
    private class AttackData {
        [SerializeField] private float percentDamage;
        [SerializeField] private int numberBullet;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float accelerationSpeed;
        [SerializeField] private bool useMaxSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private bool useMinSpeed;
        [SerializeField] private float minSpeed;
        [SerializeField] private int accelerationTurn;
        [SerializeField] private bool usePauseAndResume;
        [SerializeField] private float pauseTime;
        [SerializeField] private float resumeTime;
        [SerializeField] private bool useAutoRelease = true;
        [SerializeField] private float autoReleaseTime = 5;

        public float PercentDamage { get => percentDamage; }
        public int NumberBullet { get => numberBullet; }
        public float BulletSpeed { get => bulletSpeed; }
        public float AccelerationSpeed { get => accelerationSpeed; }
        public bool UseMaxSpeed { get => useMaxSpeed; }
        public float MaxSpeed { get => maxSpeed; }
        public bool UseMinSpeed { get => useMinSpeed; }
        public float MinSpeed { get => minSpeed; }
        public int AccelerationTurn { get => accelerationTurn; }
        public bool UsePauseAndResume { get => usePauseAndResume; }
        public float PauseTime { get => pauseTime; }
        public float ResumeTime { get => resumeTime; }
        public bool UseAutoRelease { get => useAutoRelease; }
        public float AutoReleaseTime { get => autoReleaseTime; }
    }
}
