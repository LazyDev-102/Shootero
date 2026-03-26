using Helper;
using UnityEngine;
using DG.Tweening;

public class ME01Move : EnemyMove {
    private ME01Base me01Base;
    public ME01Base ME01Base {
        get {
            if (me01Base == null) {
                me01Base = EnemyBase as ME01Base;
            }
            return me01Base;
        }
    }


    [SerializeField] private Area topLeftArea;
    [SerializeField] private Area topRightArea;
    [SerializeField] private Area midArea;
    [SerializeField] private float moveDuration;
    [SerializeField] private Ease moveEase;


    private Tween curTween;
    private Vector3[] pathMove;

    public override void Destroy() {
        base.Destroy();
        if (curTween != null) {
            curTween.Kill();
        }
        transform.DOKill();
    }

    private void CreatePathMove() {
        bool isMoveFromLeft = RandomHelper.RandomWithProbability(50);
        Vector2 topLeftPoint = BorderHelper.GetWorldPointInsideArea(topLeftArea);
        Vector2 topRightPoint = BorderHelper.GetWorldPointInsideArea(topRightArea);
        Vector2 midPoint = BorderHelper.GetWorldPointInsideArea(midArea);
        pathMove = new Vector3[3];
        if (isMoveFromLeft) {
            pathMove[0] = topLeftPoint;
            pathMove[1] = midPoint;
            pathMove[2] = topRightPoint;
        }
        else {
            pathMove[0] = topRightPoint;
            pathMove[1] = midPoint;
            pathMove[2] = topLeftPoint;
        }
    }

    public Vector2 GetPointSpawn() {
        CreatePathMove();
        return pathMove[0];
    }

    public void StartMovePath() {
        curTween = transform.DOPath(pathMove, moveDuration, PathType.CatmullRom, PathMode.TopDown2D, 5).SetLookAt(0.01f, Vector3.forward, Vector3.right).SetEase(moveEase).OnComplete(EndMovePath);
    }

    private void EndMovePath() {
        ME01Base.EndBossAttack();
        ME01Base.SelfDestruction();

    }

    //public void a() {
    //    var distance = Math.sqrt(Math.pow((myShip.x - galaxyGate.x), 2) + Math.pow((myShip.y - galaxyGate.y), 2)); 
    //    var time = (playerInfoObject.engine.speed != null) ? distance / playerInfoObject.engine.speed : 0;
    //    time = time * 1000; 
    //    var tween1 = game.add.tween(myShip).to({ x: galaxyGate.x, y: galaxyGate.y}, time, Phaser.Easing.Linear.None, true);
    //    DOTween.To();
    //}
}
