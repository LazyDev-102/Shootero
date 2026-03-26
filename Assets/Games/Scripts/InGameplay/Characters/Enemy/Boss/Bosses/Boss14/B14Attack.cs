using Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B14Attack : BossAttack {
    [SerializeField] private GameObject[] b14ShotController = null;
    private int m_nowIndex = 0;
    private IEnumerator coroutine;
    private bool lockEndAttack;
    private B14Base b14Base;

    public B14Base B14Base {
        get {
            if (b14Base == null) {
                b14Base = BossBase as B14Base;
            }
            return b14Base;
        }
    }

    public override void ChooseAttack() {
        BossAttackComponent randomAttack = null;
        int index = 0;
        if (skillAttacks.Length == 1) {
            randomAttack = skillAttacks[0];
            ChangeShot(index);
        }
        else {
            do {
                randomAttack = RandomHelper.RandomInCollection(skillAttacks, out index);
            }
            while (randomAttack == preAttack);
            ChangeShot(index);
        }
        SetCurrentAttack(randomAttack);
    }

    private void Start() {
        if (b14ShotController != null) {
            for (int i = 0; i < b14ShotController.Length; i++) {
                b14ShotController[i].SetActive(false);
            }
        }
    }

    public void ChangeShot(int index) {
        if (b14ShotController == null) {
            return;
        }

        StopAllCoroutines();

        if (0 <= m_nowIndex && m_nowIndex < b14ShotController.Length) {
            b14ShotController[m_nowIndex].SetActive(false);
        }

        m_nowIndex = index;

        if (0 <= m_nowIndex && m_nowIndex < b14ShotController.Length) {
            b14ShotController[m_nowIndex].SetActive(true);
            coroutine = StartShot();
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator StartShot() {
        float cntTimer = 0f;
        while (cntTimer < 1f) {
            cntTimer += UbhTimer.instance.deltaTime;
            yield return null;
        }

        yield return null;

        B14ShotController shotCtrl = b14ShotController[m_nowIndex].GetComponent<B14ShotController>();
        if (shotCtrl != null) {
            shotCtrl.SetBossAttack(this);
            shotCtrl.SetData();
            shotCtrl.StartShotRoutine();
        }
    }
    public void StopShotAttack() {
        if (coroutine != null)
            StopCoroutine(coroutine);
        b14ShotController[m_nowIndex].SetActive(false);
    }
    public void SetLockEndAttack(bool status) {
        lockEndAttack = status;
    }
    public override void EndAttack() {
        if (lockEndAttack)
            return;
        base.EndAttack();
    }
}
