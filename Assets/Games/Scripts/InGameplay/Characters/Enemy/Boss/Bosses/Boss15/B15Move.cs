
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class B15Move : BossMove {
    public override void StartMoveAfterAttack() {
        isEndMove = true;
        transform.localEulerAngles = Vector3.forward * 180;
    }
    public override void Knockback(Vector2 causer) {

    }
    public override void RageKnockback() {
        BossBase.IsInEffectRage = false;
    }

    protected override void EndMoveAppear() {
        base.EndMoveAppear();

        //transform.localEulerAngles = Vector3.forward * 180;
        transform.DOMoveX(0, 0.5f);
        StartCoroutine(Rotation());
    }
    IEnumerator Rotation() {
        float duration = 3f;
        while (transform.localEulerAngles != Vector3.forward * 180 && duration > 0) {
            Vector3 pos = Vector3.Lerp(transform.localEulerAngles, Vector3.forward * 180, 0.125f);
            transform.localEulerAngles = pos;
            duration -= Time.deltaTime;
            yield return null;
        }
    }
    public override void EndMove() {
        base.EndMove();
        transform.localEulerAngles = Vector3.forward * 180;
    }
}
