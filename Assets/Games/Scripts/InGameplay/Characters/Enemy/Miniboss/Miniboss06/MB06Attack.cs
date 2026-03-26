using UnityEngine;

public class MB06Attack : MinibossAttack {

    private MB06Base mb06Base;

    public MB06Base MB06Base {
        get {
            if (mb06Base == null) {
                mb06Base = MinibossBase as MB06Base;
            }
            return mb06Base;
        }
    }
}
