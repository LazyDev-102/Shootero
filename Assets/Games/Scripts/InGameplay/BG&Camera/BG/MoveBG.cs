using UnityEngine;

public class MoveBG : MonoBehaviour {
    [SerializeField] private float moveSpeed;



    private Vector2 pointReset;
    private Transform myTransform;
    bool isMoving;

    private void Start() {
        myTransform = transform;
    }

    public bool IsMoving() {
        return isMoving;
    }

    public void StartMove(Vector2 endPoint) {
        pointReset = endPoint;
        isMoving = true;
    }

    private void Update() {
        if (isMoving) {
            myTransform.position = Vector2.MoveTowards(myTransform.position, pointReset, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(myTransform.position, pointReset) < moveSpeed * Time.deltaTime) {
                isMoving = false;
                gameObject.SetActive(false);
            }
        }
    }

}
