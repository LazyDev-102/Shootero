using UnityEngine;

public class B13Shields : Shield {
    [SerializeField] private GameObject iceShieldUi;
    [SerializeField] private GameObject fireShieldUi;
    [SerializeField] private Collider2D iceHitBox;

    public override void TurnOff() {
        base.TurnOff();
        if (iceHitBox != null)
            iceHitBox.enabled = false;
        iceShieldUi.SetActive(false);
        fireShieldUi.SetActive(false);
    }
    public override void TurnOn() {
        base.TurnOn();
        if (iceHitBox != null)
            iceHitBox.enabled = true;
        iceShieldUi.SetActive(true);
        fireShieldUi.SetActive(true);
    }
}
