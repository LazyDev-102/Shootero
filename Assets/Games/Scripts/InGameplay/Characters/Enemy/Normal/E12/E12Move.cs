using DG.Tweening;
using Helper;
using UnityEngine;

public class E12Move : EnemyMove {
    [SerializeField] private Area moveArea;
    [SerializeField] private float minTimeMove = 3;
    [SerializeField] private float maxTimeMove = 6;
    [SerializeField] private AnimationCurve moveCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    [SerializeField] private float timeDelay = 1f;
    [SerializeField] private float distanceMin = 3f;
    private E12Base e12Base;
    public E12Base E12Base {
        get {
            if (e12Base == null) {
                e12Base = EnemyBase as E12Base;
            }
            return e12Base;
        }
    }
    public override void StartMoveAppear() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(IEStartMove());
        else
            StartMove();
    }
    private System.Collections.IEnumerator IEStartMove() {

        Vector2 pointAppear = GetRandomInArea(appearArea);
        targetMovePoint = pointAppear;
        float duration = 0f;
        while (duration < timeDelay) {
            duration += Time.deltaTime;
            LookTarget(targetMovePoint);
            yield return null;
        }

        direction = (pointAppear - (Vector2)transform.position).normalized;
        isEndMove = false;
        LookTarget(pointAppear);
        curMoveTween?.Kill();
        curMoveTween = transform.DOMove(pointAppear, appearMoveSpeed).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(() => {
            if (gameObject.activeInHierarchy)
                StartCoroutine(IEMoveRandom());
        });
    }
    private void StartMove() {
        Vector2 pointAppear = GetRandomInArea(appearArea);
        targetMovePoint = pointAppear;
        direction = (pointAppear - (Vector2)transform.position).normalized;
        isEndMove = false;
        LookTarget(pointAppear);
        curMoveTween?.Kill();
        curMoveTween = transform.DOMove(pointAppear, appearMoveSpeed).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(() => {
            if (gameObject.activeInHierarchy)
                StartCoroutine(IEMoveRandom());
        });
    }
    private System.Collections.IEnumerator IEMoveRandom() {
        //curMoveTween?.Kill();
        //curMoveTween = transform.DOMove(transform.position + transform.up * 2, 1).SetEase(moveCurve);
        //yield return Yielder.Wait(1);
        Vector2 pointAppear;
        int loopTime = 0;
        do {
            pointAppear = GetRandomInArea(moveArea);
            loopTime++;
            if (loopTime > 10)
                break;
        }
        while (Vector2.Distance(pointAppear, transform.position) < distanceMin);
        targetMovePoint = pointAppear;
        float duration = 0f;
        while (duration < timeDelay) {
            duration += Time.deltaTime;
            LookTarget(targetMovePoint);
            yield return null;
        }
        direction = (pointAppear - (Vector2)transform.position).normalized;
        LookTarget(pointAppear);
        curMoveTween?.Kill();
        curMoveTween = transform.DOMove(pointAppear, Random.Range(minTimeMove, maxTimeMove)).SetSpeedBased(true).SetEase(moveCurve).OnComplete(() => {
            if (gameObject.activeInHierarchy)
                StartCoroutine(IEMoveRandom());
        });
    }
    public override void Updating() {

    }
    public override void Destroy() {
        curMoveTween?.Kill();
        base.Destroy();
    }
}
