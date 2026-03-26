using DG.Tweening;
using System.Collections;
using UnityEngine;

public class DroneMove : CharacterMove {
    private Transform target;
    private Vector3 smoothedPosition;
    private float smoothSpeed = 0.125f;
    private bool endFollow;
    [SerializeField] private bool isLeft;
    [SerializeField] private ParticleSystem appearEffect;
    [SerializeField] private GameObject icon;
    private DroneBase droneBase;
    public DroneBase DroneBase {
        get {
            if (droneBase == null) {
                droneBase = CharacterBase as DroneBase;
            }
            return droneBase;
        }
    }
    public override void Initialize() {
        base.Initialize();
        target = isLeft ? DroneBase.ShipBase.DroneLeftPos : DroneBase.ShipBase.DroneRightPos;
        //CharacterBase.transform.DOMove(target.position, 1f).SetEase(Ease.OutSine);
        icon.SetActive(false);
    }
    public override void Updating() {
        if (GameManager.Instance.GameState.Equals(GameState.Playing)) {
            if (DroneBase.ShipBase.ShipMove.IsShipMoving) {
                FollowShip();
                endFollow = false;
            }
            else if (!endFollow) {
                endFollow = true;
                if (gameObject.activeInHierarchy)
                    StartCoroutine(EndFollowShip());
            }
        }
    }

    private void FollowShip() {
        smoothedPosition = Vector3.Lerp(transform.position, target.position, smoothSpeed);
        transform.position = smoothedPosition;
        //transform.LookAt(target);
    }

    private IEnumerator EndFollowShip() {
        var duration = 0f;
        while (duration < 0.5f && !DroneBase.ShipBase.ShipMove.IsShipMoving) {
            duration += Time.deltaTime;
            FollowShip();
            yield return null;
        }
    }
    public void SetFocus(bool isLeft) {
        this.isLeft = isLeft;
        target = isLeft ? DroneBase.ShipBase.DroneLeftPos : DroneBase.ShipBase.DroneRightPos;
        PlayAppearEffect();
        //CharacterBase.transform.DOMove(target.position, 1f).SetEase(Ease.OutSine);
    }
    public void PlayAppearEffect() {
        if (DroneBase == null || DroneBase.transform != null) {
            if (icon)
                icon.SetActive(true);
            if (DroneBase.DroneAttack)
                DroneBase.DroneAttack.StartAttack();
            if (appearEffect)
                appearEffect.Play();
            return;
        }
        DroneBase.transform.position = target.position;
        appearEffect.Play();
        DOVirtual.DelayedCall(appearEffect.main.duration - 0.1f, () => {
            icon.SetActive(true);
            DroneBase.DroneAttack.StartAttack();
        });
    }
    public virtual void LookDirection(Vector2 direction) {
        MyRigi.MoveRotation(Mathf.LerpAngle(MyRigi.rotation, Vector2.SignedAngle(Vector2.up, direction), Time.deltaTime * 20));
    }

    public virtual void LookTarget(Vector2 target) {
        LookDirection(target - (Vector2)transform.position);
    }

}
