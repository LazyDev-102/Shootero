

using Helper;
using UnityEngine;

public class E01Skin : MonoBehaviour {
    [SerializeField] private SpriteRenderer[] mySRs;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private RangeFloatValue rotateSpeedRange;
    [SerializeField] private Transform rotateContainer;

    private float currentRotateSpeed;

    private float currentAngle;
    private E01Base e01Base;
    public E01Base E01Base {
        get {
            if (e01Base == null) {
                e01Base = GetComponent<E01Base>();
            }
            return e01Base;
        }
    }

    public void Initalize() {
        Sprite randomSprite = Helper.RandomHelper.RandomInCollection(sprites);
        foreach (var s in mySRs) {
            s.sprite = randomSprite;
        }
    }

    public void SetSkin(Sprite sprite) {
        foreach (var s in mySRs) {
            s.sprite = sprite;
        }
    }

    public Sprite GetSkin() {
        return mySRs[0].sprite;
    }

    public void Rotate(float angle) {
        currentAngle = angle;
        rotateContainer.Rotate(transform.forward, currentAngle);
    }

    public void StartRotateSelf() {
        currentRotateSpeed = RandomHelper.RandomInRange(rotateSpeedRange);
    }

    public void RotateSelf() {
        Rotate(currentRotateSpeed * Time.deltaTime);
    }
}
