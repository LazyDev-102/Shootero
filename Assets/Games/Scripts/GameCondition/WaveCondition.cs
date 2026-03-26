
public abstract class WaveCondition : GameCondition {
    [UnityEngine.SerializeField] protected GameAction action;
    public abstract bool Action(object target, System.Action onComplete);
}
public abstract class WaveCondition<T> : WaveCondition {
    public override bool Action(object target, System.Action onComplete) {
        if (target is T t) {
            return Action(t, onComplete);
        }
        return false;
    }

    public abstract bool CheckCondition(T target);
    public abstract bool Action(T target, System.Action onComplete);
}