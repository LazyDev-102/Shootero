using UnityEngine;

public class MB14Health : MinibossHealth {
    private MB14Base mb14Base;
    public MB14Base MB14Base {
        get {
            if (mb14Base == null) {
                mb14Base = CharacterBase as MB14Base;
            }
            return mb14Base;
        }
    }
    public override void HPReduce(int hp) {
        base.HPReduce(hp);
        if (hp > 0) {
            MB14Base.MB14Attack.CheckPhase();
        }
    }
}
