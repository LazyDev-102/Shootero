using UnityEngine;
using UnityEngine.UI;

public class ShipSkin : MonoBehaviour {
    [SerializeField] private SpriteRenderer shipIcon;

    public void Initialized() {
        shipIcon.sprite = GameResources.Instance.Ship.GetCurrentShip().GetIcon();
    }
}
