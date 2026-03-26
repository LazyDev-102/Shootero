using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewGearRegister", menuName = "Resource/Notifications/Registers/New Gear Register")]
public class NewGearRegister : NotiRegister<GearSoftData> {
    [System.Serializable] public class UnityEventItem : UnityEvent<GearSoftData> { }

    [SerializeField] private UnityEventItem onUpdate;
    public override UnityEvent<GearSoftData> OnUpdate { get => onUpdate; }
}
