using UnityEngine;

public abstract class MinibossAttackComponent : MonoBehaviour {
    private GameLoader gameLoader;
    public GameLoader GameLoader {
        get {
            if (gameLoader == null) {
                gameLoader = GameManager.Instance.GameLoader;
            }
            return gameLoader;
        }
    }
    public virtual void Initialize() {

    }
    public abstract void StartAttack();

    public abstract void Updating();

    public abstract void Attacking();

    public virtual void StopAttack() {
        StopAllCoroutines();
    }

    public virtual void PreloadIngame() {

    }
}

public abstract class MinibossAttackComponent<T> : MinibossAttackComponent where T : MinibossAttack {

    [SerializeField] protected T minibossAttack;

    public T MinibossAttack {
        get {
            return minibossAttack;
        }
    }

    public virtual void EndAttack() {
        minibossAttack.EndAttack();
    }

}