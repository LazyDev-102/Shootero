using Gemmob;
using TMPro;
using UnityEngine;

public class TextIngame : MonoBehaviour {
    [SerializeField] private TextMeshPro txtContent;
    [SerializeField] private DotweenAnimation anim;
    [Header("Preload")]
    [SerializeField] private int numberPreload;

    private void Start() {
        anim.Initialize();
    }


    public void Show(string content) {
        if (txtContent) {
            txtContent.text = content;
        }
        if (anim) {
            anim.Play(() => {
                Hide();
            }, true);
        }
    }

    public void Hide() {
        this.Recycle();
    }

    public void PreloadOpenApp() {
        this.RegisterPool(numberPreload);
    }
}
