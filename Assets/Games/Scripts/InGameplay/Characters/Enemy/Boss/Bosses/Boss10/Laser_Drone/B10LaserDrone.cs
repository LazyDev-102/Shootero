using Gemmob;
using System;
using UnityEngine;


public class B10LaserDrone : MonoBehaviour {
    [SerializeField] private BasicLaser laser;
    [SerializeField] private float deltaShot;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float damagePercent;

    private bool isBeamLaser;
    private bool isMove;

    private Countdowner deltaShotCountdowner = new Countdowner();
    private CharacterBase character;

    private Vector2 targetMove;


    private Action<B10LaserDrone> onMoveCompleted;

    public void AddOnMoveCompleted(Action<B10LaserDrone> onMoveCompleted) {
        this.onMoveCompleted = onMoveCompleted;
    }

    public void RemoveOnMoveCompleted() {
        this.onMoveCompleted = null;
    }
    public void SetCharacter(CharacterBase character) {
        this.character = character;
    }
    public void StartLaser() {
        isBeamLaser = true;
        laser.gameObject.SetActive(true);
        laser.StartBeam();
        deltaShotCountdowner.StartCountdown(deltaShot);
    }

    public void EndLaser() {
        isBeamLaser = false;
        laser.EndBeam();
        laser.gameObject.SetActive(false);
    }

    public void StartMove(Vector2 target) {
        isMove = true;
        targetMove = target;
        EndLaser();
    }

    private void EndMove() {
        onMoveCompleted?.Invoke(this);
        RemoveOnMoveCompleted();
        isMove = false;
        StartLaser();
    }

    private void Update() {
        if (isBeamLaser) {
            deltaShotCountdowner.Countdowning(Time.deltaTime);
            if (deltaShotCountdowner.IsTimeOut()) {
                laser.SetInfor((int)(character.CharacterStat.Atk.Value * damagePercent), null);
                laser.SetCharacterBase(character);
                laser.Beaming(true);
                deltaShotCountdowner.StartCountdown(deltaShot);
            }
            else {
                laser.Beaming(false);
            }
        }
        if (isMove) {
            transform.position = Vector2.MoveTowards(transform.position, targetMove, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, targetMove) <= moveSpeed * Time.deltaTime) {
                EndMove();
            }
        }
    }

    public void Destroy() {
        RemoveOnMoveCompleted();
        this.Recycle();
    }
}
