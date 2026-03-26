using UnityEngine;

public class MinibossHitbox : EnemyHitbox {
    [SerializeField] private Shield shield;


    public void TurnOnShield() {
        if (shield) {
            shield.TurnOn();
        }
    }

    public void TurnOffShield() {
        if (shield) {
            shield.TurnOff();
        }
    }
}
