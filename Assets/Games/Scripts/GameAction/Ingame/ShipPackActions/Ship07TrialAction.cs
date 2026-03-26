using System;
using Gear_Data;
using Gemmob;
using UnityEngine;

[CreateAssetMenu(fileName = "Ship07TrialAction", menuName = "Resource/GameAction/Ship/Ship07TrialAction")]
public class Ship07TrialAction : GameAction {
    [SerializeField] private StatModifier bounceValue;
    public override void Execute(object user, Action onCompleted) {
        EventDispatcher.Instance.Dispatch((int)EventKey.StatEvent.Bounce, new StatValueParam { isAdd = true, value = bounceValue });
    }
}
