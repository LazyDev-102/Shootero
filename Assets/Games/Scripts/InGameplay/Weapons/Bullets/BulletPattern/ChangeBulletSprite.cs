

using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class ChangeBulletSprite : MonoBehaviour {
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private bool random;
    [SerializeField] private float deltaChange;

    private Countdowner deltaChangeCountdower = new Countdowner();
    private SpriteRenderer sr;
    private int currentIndex;

    public void Start() {
        sr = GetComponent<SpriteRenderer>();
        deltaChangeCountdower.StartCountdown(deltaChange);
    }

    private void Update() {
        deltaChangeCountdower.Countdowning(Time.deltaTime);
        if (deltaChangeCountdower.IsTimeOut()) {
            deltaChangeCountdower.StartCountdown(deltaChange);
            ChangeSprite();
        }
    }

    private void ChangeSprite() {
        if (random) {
            int randomIndex = 0;
            int count = 0;
            do {
                randomIndex = Random.Range(0, sprites.Length);
                count++;
            } while (randomIndex == currentIndex && count < 10);
            currentIndex = randomIndex;
        }
        else {
            currentIndex++;
            if (currentIndex == sprites.Length) {
                currentIndex = 0;
            }
        }
        sr.sprite = sprites[currentIndex];
    }
}
