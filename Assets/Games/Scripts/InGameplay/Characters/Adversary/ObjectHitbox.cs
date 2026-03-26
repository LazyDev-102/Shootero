

using Gemmob;
using UnityEngine;

public abstract class ObjectHitbox : MonoBehaviour {

    private ObjectBase objectBase;
    protected ObjectBase ObjectBase {
        get {
            if (objectBase == null) {
                objectBase = GetComponent<ObjectBase>();
            }
            return objectBase;
        }
    }

    protected GameState gameState;

    public virtual void Initialize() {
        EventDispatcher.Instance.AddListener<EventKey.GameStateChangedParam>(OnGameStateChanged);
        gameState = GameManager.Instance.GameState;
    }

    public virtual void Destroy() {
        EventDispatcher.Instance.RemoveListener<EventKey.GameStateChangedParam>(OnGameStateChanged);
    }

    public virtual void Updating() {
    }

    private void OnGameStateChanged(EventKey.GameStateChangedParam param) {
        gameState = param.gameState;
    }

    protected virtual bool IsBlockTakeHit() {
        return gameState != GameState.Playing && !GameManager.Instance.isTest;
    }
}
