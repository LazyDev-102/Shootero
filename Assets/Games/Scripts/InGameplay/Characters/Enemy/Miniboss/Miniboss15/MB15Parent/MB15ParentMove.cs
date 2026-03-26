using UnityEngine;
using DG.Tweening;

public class MB15ParentMove : MinibossMove {

    [SerializeField, Range(0f, 5f)] protected float timeOneRoundRotation = 1f;
    [SerializeField] protected DG.Tweening.DOTweenAnimation anim;
    public override void Initialize() {
        base.Initialize();
        if (anim) {
            anim.duration = timeOneRoundRotation;
            anim.DOPlay();
        }
    }
    public override void Destroy() {
        base.Destroy();
    }

}
