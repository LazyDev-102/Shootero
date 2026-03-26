using DG.Tweening;
using UnityEngine;

public class ModItemMoveDot : MonoBehaviour {
    [SerializeField, Range(0f, 5f)] private float timeMovePerEdge = 0.2f;
    [SerializeField] private Transform originPos;
    [SerializeField] private Transform pos12;
    [SerializeField] private Transform pos23;
    [SerializeField] private Transform pos34;
    [SerializeField] private Transform pos41;
    [SerializeField] private RotateState cState = RotateState.Move12;
    [SerializeField] private Ease moveCurve = Ease.Linear;
    private RotateState originState;
    private bool canMove;
    private bool initialize;
    private void Start() {
        originState = cState;
        initialize = true;
    }
    private void OnEnable() {
        transform.localPosition = originPos.localPosition;
        canMove = true;
        if (initialize)
            cState = originState;
    }
    private void Update() {
        switch (cState) {
            case RotateState.Move12:
                Move12();
                break;
            case RotateState.Move23:
                Move23();
                break;
            case RotateState.Move34:
                Move34();
                break;
            case RotateState.Move41:
                Move41();
                break;
        }
    }
    private void Move12() {
        if (!canMove)
            return;
        canMove = false;
        transform.DOLocalMove(pos23.localPosition, timeMovePerEdge).SetUpdate(true).SetEase(moveCurve).OnComplete(() => { cState = RotateState.Move23; canMove = true; });
    }
    private void Move23() {
        if (!canMove)
            return;
        canMove = false;
        transform.DOLocalMove(pos34.localPosition, timeMovePerEdge).SetUpdate(true).SetEase(moveCurve).OnComplete(() => { cState = RotateState.Move34; canMove = true; });
    }
    private void Move34() {
        if (!canMove)
            return;
        canMove = false;
        transform.DOLocalMove(pos41.localPosition, timeMovePerEdge).SetUpdate(true).SetEase(moveCurve).OnComplete(() => { cState = RotateState.Move41; canMove = true; });
    }
    private void Move41() {
        if (!canMove)
            return;
        canMove = false;
        transform.DOLocalMove(pos12.localPosition, timeMovePerEdge).SetUpdate(true).SetEase(moveCurve).OnComplete(() => { cState = RotateState.Move12; canMove = true; });
    }
    private enum RotateState {
        Move12 = 1,
        Move23 = 2,
        Move34 = 3,
        Move41 = 4,
    }
}
