using UnityEngine;

public class B13ShotController : UbhShotCtrl {
    private int attackIndex;
    public void SetAttack(int index) {
        attackIndex = index;
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
                nowShotInfo.m_shotObj.SetPercentDamage(1);
                nowShotInfo.m_shotObj.Shot();
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
}
