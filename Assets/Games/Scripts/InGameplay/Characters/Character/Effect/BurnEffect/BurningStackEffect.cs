

using UnityEngine;

public class BurningStackEffect : MonoBehaviour {
    [SerializeField] private SpriteRenderer[] sprites;

    private Transform target;
    private Vector3 offset;
    private Transform myTransform;

    private void Start() {
        myTransform = transform;
    }

    public void ShowStack(int stack) {
        for (int i = 0; i < sprites.Length; ++i) {
            sprites[i].gameObject.SetActive(i < stack);
        }
    }

    public void SetTarget(Transform target, Vector2 offset) {
        this.target = target;
        this.offset = offset;
    }

    private void Update() {
        if (target != null) {
            myTransform.position = target.position + offset;
        }
    }
}
