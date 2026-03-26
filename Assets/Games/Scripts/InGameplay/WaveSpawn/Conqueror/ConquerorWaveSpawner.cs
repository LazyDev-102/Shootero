using Gemmob;
using UnityEngine;

public abstract class ConquerorWaveSpawner : MonoBehaviour {
    protected ConquerorController controller;
    public abstract void StartSpawn();
    public abstract bool IsWinWave();
    public abstract void OnObjectRemove();
    public abstract void EndSpawn();

    public abstract void OnChangeTypeWave();

    public void SetController(ConquerorController controller) {
        this.controller = controller;
    }
    public virtual void PreStartActionPlay(System.Action onCompleted) {
        WaveCondition[] preStartCondition = controller.CurrentWaveInfo.WaveData.PreStartCondition;
        if (preStartCondition == null) {
            onCompleted?.Invoke();
            return;
        }
        ShipBase ship = GameManager.Instance.GameLoader.Ship;
        for (int i = 0; i < preStartCondition.Length; i++) {
            if (preStartCondition[i] != null) {
                if (preStartCondition[i].Action(ship, onCompleted))
                    return;
            }
        }
        onCompleted?.Invoke();
    }

    public virtual void PreEndActionPlay(System.Action onCompleted) {
        WaveCondition[] preEndCondition = controller.CurrentWaveInfo.WaveData.PreEndCondition;
        if (preEndCondition == null) {
            onCompleted?.Invoke();
            return;
        }
        ShipBase ship = GameManager.Instance.GameLoader.Ship;
        if (ship == null) {
            onCompleted?.Invoke();
            return;
        }
        for (int i = 0; i < preEndCondition.Length; i++) {
            if (preEndCondition[i] != null) {
                if (preEndCondition[i].Action(ship, onCompleted)) {
                    return;
                }
            }
        }
        onCompleted?.Invoke();
    }
}
