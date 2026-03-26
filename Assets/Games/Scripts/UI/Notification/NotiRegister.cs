using Gemmob;
using UnityEngine;
using UnityEngine.Events;

public abstract class NotiRegister : ScriptableObject {
    [SerializeField] private UnityEvent onUpdate;

    public UnityEvent OnUpdate { get => onUpdate; }

    protected virtual void OnEnable() {
        hideFlags |= HideFlags.DontUnloadUnusedAsset;
    }
}



public abstract class NotiRegister<T> : ScriptableObject where T : IEventParams {
    public abstract UnityEvent<T> OnUpdate { get; }

    protected virtual void OnEnable() {
        hideFlags |= HideFlags.DontUnloadUnusedAsset;
        EventDispatcher.Instance.AddListener<T>(OnNotificationUpdated);
    }

    protected virtual void OnNotificationUpdated(T param) {
        OnUpdate?.Invoke(param);
    }
}