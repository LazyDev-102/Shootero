using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler {
    public bool interactable = true;
    public UnityEvent onClick;
    public UnityEvent onHold;
    public UnityEvent onRelease;

    bool invoked = false;
    bool pointerDown = false;

    protected void OnEnable() {
        ResetInvokeState();
    }

    public void ResetInvokeState() {
        invoked = false;
    }

    public virtual void SetState(bool enable) {
        interactable = enable;
    }

    public void AddListener(UnityAction action, bool resetAll = false) {
        if (!resetAll)
            onClick.RemoveListener(action);
        else
            onClick.RemoveAllListeners();
        onClick.AddListener(action);
    }

    public void OnPointerDown(PointerEventData eventData) {
        pointerDown = true;
        InvokeOnHold();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (interactable && (!invoked)) {
            invoked = true;
            InvokeOnClick();
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (!pointerDown)
            return;
        pointerDown = false;
        InvokeOnRelease();
    }

    protected virtual void InvokeOnClick() {
        if (onClick != null)
            onClick.Invoke();
    }
    protected virtual void InvokeOnHold() {
        if (onHold != null)
            onHold.Invoke();
    }
    protected virtual void InvokeOnRelease() {
        if (onRelease != null)
            onRelease.Invoke();
    }
    public void AddHoldEvent(UnityAction action) {
        this.onHold.AddListener(action);
    }
    public void AddReleaseEvent(UnityAction action) {
        this.onRelease.AddListener(action);
    }
}
