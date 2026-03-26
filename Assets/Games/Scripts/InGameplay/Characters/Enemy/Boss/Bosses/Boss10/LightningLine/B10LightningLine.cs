using UnityEngine;

public class B10LightningLine : MonoBehaviour {
    [SerializeField] private LightningLine lightningLine;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float length;
    [SerializeField] private float speedMove;
    [SerializeField] private B10Base causer;

    private State curState = State.Idle;
    private Vector2 targetStartPoint;
    private Vector2 targetEndPoint;


    public void SetDamage(int damage, ObjectBase causer) {
        lightningLine.SetInfor(damage, causer);
    }

    public void Show() {
        lightningLine.SetInfor((int)(causer.B10Stat.Atk.Value * 0.1f), causer);
        curState = State.Idle;
        startPoint.transform.localPosition = Vector3.zero;
        endPoint.transform.localPosition = Vector3.zero;
        targetStartPoint = transform.position + transform.up * length;
        targetEndPoint = transform.position + -1 * transform.up * length;
        lightningLine.SetActive(true);
        gameObject.SetActive(true);

    }

    public void Hide() {
        curState = State.Idle;
        startPoint.transform.localPosition = Vector3.zero;
        endPoint.transform.localPosition = Vector3.zero;
        lightningLine.SetActive(false);
        gameObject.SetActive(false);
    }

    public void StartMoveOut() {
        curState = State.MoveOut;
    }

    public void StartMoveIn() {
        curState = State.MoveIn;
    }

    private void Update() {
        switch (curState) {
            case State.Idle: {

                break;
            }
            case State.MoveOut: {
                MovingOut();
                break;
            }
            case State.MoveIn: {
                MovingIn();
                break;
            }
        }
        lightningLine.UpdatePosition(startPoint.position, endPoint.position);
    }

    private void MovingOut() {
        startPoint.position = Vector3.MoveTowards(startPoint.position, targetStartPoint, Time.deltaTime * speedMove);
        endPoint.position = Vector3.MoveTowards(endPoint.position, targetEndPoint, Time.deltaTime * speedMove);
        if (Vector3.Distance(startPoint.position, targetStartPoint) < 0.5f) {
            curState = State.Idle;
        }

    }

    private void MovingIn() {
        startPoint.position = Vector3.MoveTowards(startPoint.position, transform.position, Time.deltaTime * speedMove);
        endPoint.position = Vector3.MoveTowards(endPoint.position, transform.position, Time.deltaTime * speedMove);
        if (Vector3.Distance(startPoint.position, transform.position) < 0.5f) {
            curState = State.Idle;
            Hide();
        }
    }

    private enum State { MoveOut, MoveIn, Idle }
}
