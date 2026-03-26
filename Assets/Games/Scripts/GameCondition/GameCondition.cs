using UnityEngine;

public abstract class GameCondition : ScriptableObject {
    public abstract bool CheckCondition(object target);
}

public abstract class GameCondition<T> : GameCondition {
    public override bool CheckCondition(object target) {
        if (target is T t) {
            return CheckCondition(t);
        }
        return false;
    }

    public abstract bool CheckCondition(T target);
}
