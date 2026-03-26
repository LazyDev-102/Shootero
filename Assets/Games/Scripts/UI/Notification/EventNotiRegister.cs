using GameSystem.Common.UnityInspector;
using Gemmob;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EventNotiRegister", menuName = "Resource/Notifications/Event Register")]
public class EventNotiRegister : NotiRegister {
    [SerializeField, ConstantField(typeof(EventKey))] private int[] eventRegisters;

    public IEnumerable<int> GetEventRegisters() {
        return eventRegisters;
    }

    protected override void OnEnable() {
        base.OnEnable();

        foreach (int eventRegister in GetEventRegisters()) {
            EventDispatcher.Instance.AddListener(eventRegister, OnNotificationUpdated);
        }
    }

    protected virtual void OnNotificationUpdated() {
        OnUpdate?.Invoke();
    }
}
