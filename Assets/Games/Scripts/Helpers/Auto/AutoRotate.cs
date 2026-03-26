using UnityEngine;

public class AutoRotate : MonoBehaviour {
    // Rotate speed
    [SerializeField] private RangeFloatValue speedRange;
    [SerializeField] private bool igroneScaleTime;
    [SerializeField] private bool canChangeOnEnable;

    private Transform myTransform;
    private Vector3 rotateSpeed;

    void Awake() {
        myTransform = transform;
        rotateSpeed = Vector3.back * speedRange.GetRandomValue();
    }

    private void OnEnable() {
        if (canChangeOnEnable) {
            ChangeSpeedRandom();
        }
    }

    void Update() {
        if (igroneScaleTime) {
            myTransform.Rotate(rotateSpeed * Time.fixedDeltaTime);
        }
        else {
            myTransform.Rotate(rotateSpeed * Time.deltaTime);
        }
    }

    public void ChangeSpeedRandom() {
        rotateSpeed = Vector3.back * speedRange.GetRandomValue();
    }
}