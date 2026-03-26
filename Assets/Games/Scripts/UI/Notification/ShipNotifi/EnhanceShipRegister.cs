using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "EnhanceShipRegister", menuName = "Resource/Conditions/Ship/Enhance Ship Register")]
public class EnhanceShipRegister : NotiRegister<ShipInfor> {
    [System.Serializable] public class UnityEventItem : UnityEvent<ShipInfor> { }

    [SerializeField] private UnityEventItem onUpdate;
    public override UnityEvent<ShipInfor> OnUpdate { get => onUpdate; }
}
