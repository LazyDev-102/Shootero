

using UnityEngine;

public class CameraFollowShip : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float deltaTime;
    [SerializeField] private float maxRangeX;
    [SerializeField] private float maxRangeY;
    [SerializeField] private float distanceMultiX = 1;
    [SerializeField] private float distanceMultiY = 1;

    private float z;

    private Vector3 lastPositionTarget;
    private Vector3 curPositionTarget;

    private Vector3 velocity = Vector3.zero;
    private Vector3 left, right, top, bottom;
    Camera cam;
    ShipBase ship;

    private float halfPartW;
    private float halfPartH;

    private bool canFollow;

    private void Start() {
        cam = Camera.main;
        z = transform.position.z;
        left = cam.ViewportToWorldPoint(new Vector3(0, 0.5f, cam.nearClipPlane));
        right = cam.ViewportToWorldPoint(new Vector3(1, 0.5f, cam.nearClipPlane));
        top = cam.ViewportToWorldPoint(new Vector3(0.5f, 1, cam.nearClipPlane));
        bottom = cam.ViewportToWorldPoint(new Vector3(0.5f, 0, cam.nearClipPlane));
        halfPartW = Vector2.Distance(transform.position, left);
        halfPartH = Vector2.Distance(transform.position, top);

        maxRangeX = ConfigIngameData.borderW / 2;
        maxRangeY = ConfigIngameData.borderH / 2;
        canFollow = true;
        ship = GameManager.Instance.GameLoader.Ship;
    }

    // Update is called once per frame
    void LateUpdate() {
        if (!canFollow)
            return;
        if (ship == null)
            ship = GameManager.Instance.GameLoader.Ship;
        else
            target = ship.transform;
        if (target != null) {
            curPositionTarget = target.position;
            Vector3 targetPosition = target.position;
            targetPosition.x *= distanceMultiX;
            targetPosition.y *= distanceMultiY;
            Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, maxSpeed, deltaTime);
            newPosition.z = z;
            newPosition.x = Mathf.Clamp(newPosition.x, -(maxRangeX - halfPartW), (maxRangeX - halfPartW));
            newPosition.y = Mathf.Clamp(newPosition.y, -(maxRangeY - halfPartH), (maxRangeY - halfPartH));

            newPosition.y = transform.position.y; // nochange y
            transform.position = newPosition;
            lastPositionTarget = target.position;
        }
    }
    public void SetCanFollowStatus(bool status) {
        this.canFollow = status;
    }

}


