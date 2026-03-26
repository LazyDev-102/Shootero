using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearDropMoveDot : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float timeMovePerEdge = 0.1f;
    [SerializeField] private Transform originPos;
    [SerializeField] private Transform pos61;
    [SerializeField] private Transform pos12;
    [SerializeField] private Transform pos23;
    [SerializeField] private Transform pos34;
    [SerializeField] private Transform pos45;
    [SerializeField] private Transform pos56;
    [SerializeField] private RotateState cState = RotateState.Move12;
    private bool canMove;
    private void OnEnable() {
        transform.localPosition = originPos.localPosition;
        canMove = true;
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
            case RotateState.Move45:
                Move45();
                break;
            case RotateState.Move56:
                Move56();
                break;
            case RotateState.Move61:
                Move61();
                break;
        }
    }
    private void Move12() {
        if(!canMove) return;
        canMove = false;
        transform.DOLocalMove(pos23.localPosition, timeMovePerEdge).SetEase(Ease.Linear).OnComplete(()=>{ cState = RotateState.Move23; canMove = true;});;
    }
    private void Move23() {
        if(!canMove) return;
        canMove = false;
        transform.DOLocalMove(pos34.localPosition, timeMovePerEdge).SetEase(Ease.Linear).OnComplete(()=>{ cState = RotateState.Move34; canMove = true;});;
    }
    private void Move34() {
        if(!canMove) return;
        canMove = false;
        transform.DOLocalMove(pos45.localPosition, timeMovePerEdge).SetEase(Ease.Linear).OnComplete(()=>{ cState = RotateState.Move45; canMove = true;});;
    }
    private void Move45() {
        if(!canMove) return;
        canMove = false;
        transform.DOLocalMove(pos56.localPosition, timeMovePerEdge).SetEase(Ease.Linear).OnComplete(()=>{ cState = RotateState.Move56; canMove = true;});;
    }
    private void Move56() {
        if(!canMove) return;
        canMove = false;
        transform.DOLocalMove(pos61.localPosition, timeMovePerEdge).SetEase(Ease.Linear).OnComplete(()=>{ cState = RotateState.Move61; canMove = true;});;
    }
    private void Move61() {
        if(!canMove) return;
        canMove = false;
        transform.DOLocalMove(pos12.localPosition, timeMovePerEdge).SetEase(Ease.Linear).OnComplete(()=>{ cState = RotateState.Move12; canMove = true;});;
    }
    private enum RotateState {
        Move12 = 1,
        Move23 = 2,
        Move34 = 3,
        Move45 = 4,
        Move56 = 5,
        Move61 = 6,
    }
}
