using System;
using Gear_Data;
using Gemmob;
using UnityEngine;

[CreateAssetMenu(fileName = "Ship08TrialAction", menuName = "Resource/GameAction/Ship/Ship08TrialAction")]
public class Ship08TrialAction : GameAction {
    [SerializeField] private StatModifier timeValue;
    public override void Execute(object user, Action onCompleted) {
        EventDispatcher.Instance.Dispatch((int)EventKey.StatEvent.BulletTimeLife, new StatValueParam { isAdd = true, value = timeValue });
    }
}
