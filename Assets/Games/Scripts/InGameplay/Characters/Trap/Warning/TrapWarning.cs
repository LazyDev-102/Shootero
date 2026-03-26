

using UnityEngine;

public class TrapWarning : MonoBehaviour {
    [SerializeField] private Transform triangle;
    [SerializeField] private Transform glow;
    [SerializeField] private RangeFloatValue sizeRange;
    [SerializeField] private float offset;

    private Transform myTransform;

    public void Awake() {
        myTransform = transform;
    }

    public void Updating(Vector2 position, Vector2 normal, float distance) {
        myTransform.localScale = Vector3.one * sizeRange.GetRatioValue(1 - distance / offset);
        myTransform.eulerAngles = Vector3.zero;
        myTransform.position = position;
        glow.localPosition = normal * 0.55f;
        triangle.localPosition = normal * -0.5f;
    }
}
