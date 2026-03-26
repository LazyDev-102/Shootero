using System;
using Gemmob;
using UnityEngine;

[CreateAssetMenu(fileName = "Ship09TrialAction", menuName = "Resource/GameAction/Ship/Ship09TrialAction")]
public class Ship09TrialAction : GameAction {
    [SerializeField] private StatModifier lifeStealValue;
    public override void Execute(object user, Action onCompleted) {
        EventDispatcher.Instance.Dispatch((int)EventKey.StatEvent.LifeSteal, new Gear_Data.StatValueParam { isAdd = true, value = lifeStealValue });
    }
}
