using UnityEngine;

public class MB07Attack : MinibossAttack {

    private MB07Base mb07Base;

    public MB07Base MB07Base {
        get {
            if (mb07Base == null) {
                mb07Base = MinibossBase as MB07Base;
            }
            return mb07Base;
        }
    }
}
