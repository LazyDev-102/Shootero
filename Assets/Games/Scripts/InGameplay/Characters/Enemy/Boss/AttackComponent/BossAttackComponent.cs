

using UnityEngine;

public abstract class BossAttackComponent : MonoBehaviour {

    protected abstract BossAttack GetBossAttack();

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

    public virtual void EndAttack() {
        GetBossAttack().EndAttack();
    }

    public virtual void BossDestroy() {

    }

    public virtual void StopAttack() {
        StopAllCoroutines();
    }

    public virtual void PreloadIngame() {

    }
}
