using UnityEngine;

public class B13T02Laser : T02Laser {
    [SerializeField] private B13T02HitBox[] b13T02HitBoxes;

    public void SetOwner(B13Base owner) {
        for (int i = 0; i < b13T02HitBoxes.Length; i++) {
            b13T02HitBoxes[i].SetOwner(owner);
        }
    }
}
