using System.Collections;
using UnityEngine;

public class ShipMoveUI : MonoBehaviour {
    [SerializeField] private bool canMove;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D myRigi;
    [SerializeField] private float amplitude = 1f;
    [SerializeField] private float cyclesPerSecond = 1f;
    [SerializeField] private float maxSpeed;

    float curTime = 0;
    private Vector3 normal = Vector3.forward;
    private Vector3 amplitudeDirection;
    private Vector2 direction;

    private Vector2 translatePosition;
    private Vector2 sinPosition;
    private bool isLoad;
    private bool isAdd;
    private void FixedUpdate() {
        if (canMove) {
            Shoot();
            translatePosition = direction * speed * Time.deltaTime;
            sinPosition = amplitudeDirection * amplitude * (Mathf.Sin(cyclesPerSecond * curTime * 2 * Mathf.PI) - Mathf.Sin(cyclesPerSecond * (curTime - Time.deltaTime) * 2 * Mathf.PI));
            myRigi.MovePosition(myRigi.position + translatePosition + sinPosition);
            curTime += Time.deltaTime;
        }
    }
    public void Shoot() {
        if (isLoad)
            return;
        isLoad = true;
        this.direction = transform.up.normalized;
        curTime = 0;
        amplitudeDirection = Vector3.Cross(normal, direction).normalized;
        StartCoroutine(ChangeSpeed());
    }

    private IEnumerator ChangeSpeed() {
        while (true) {
            if (speed >= maxSpeed) {
                isAdd = false;
            }
            else if (speed <= -maxSpeed) {
                isAdd = true;
            }

            if (isAdd) {
                speed += Time.deltaTime;
            }
            else {
                speed -= Time.deltaTime;
            }
            yield return null;
        }
    }
}
