
using System;
using UnityEngine;
public abstract class GameAction : ScriptableObject {

    public void Execute() {
        Execute(null);
    }

    public void Execute(object user) {
        Execute(user, null);
    }

    public abstract void Execute(object user, Action onCompleted);
}
public abstract class GameAction<T> : ScriptableObject {

    public void Execute(T target) {
        Execute(target, null, null);
    }
    public void RemoveExecute(T target) {
        RemoveExecute(target, null, null);
    }
    public abstract void Execute(T target, object user, Action onCompleted);
    public abstract void RemoveExecute(T target, object user, Action onCompleted);
    public abstract void Execute(T target, Action onCompleted);
}
