using System;
using UnityEngine;

[Serializable]
public abstract class ModesAction : GameAction<ObstacleBase> {
    public override void Execute(ObstacleBase target, object user, Action onCompleted) {
    }

    public override void Execute(ObstacleBase target, Action onCompleted) {
    }

    public override void RemoveExecute(ObstacleBase target, object user, Action onCompleted) {
    }
}