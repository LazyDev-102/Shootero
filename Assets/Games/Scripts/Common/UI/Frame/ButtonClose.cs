using GameSystem.Common.UI;
using UnityEngine;

[RequireComponent(typeof(ButtonBase))]
public class ButtonClose : MonoBehaviour {
    [SerializeField] private Frame frame;

    private ButtonBase button;

    private void Reset() {
        frame = GetComponentInParent<Frame>();
    }

    private void Awake() {
        button = GetComponent<ButtonBase>();
    }

    private void Start() {
        button?.AddEvent(Close);
    }

    private void OnDestroy() {
        if (button != null)
            button.RemoveEvent(Close);
    }

    public void Close() {
        frame?.Hud?.Hide(frame);
    }
}
