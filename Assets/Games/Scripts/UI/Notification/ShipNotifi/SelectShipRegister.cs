using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SelectShipRegister", menuName = "Resource/Notifications/Registers/Select Ship Register")]
public class SelectShipRegister : NotiRegister<ShipInfor> {
    [System.Serializable] public class UnityEventItem : UnityEvent<ShipInfor> { }

    [SerializeField] private UnityEventItem onUpdate;
    public override UnityEvent<ShipInfor> OnUpdate { get => onUpdate; }
}
