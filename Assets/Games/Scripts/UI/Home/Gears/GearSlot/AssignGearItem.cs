using UnityEngine;
using UnityEngine.UI;

public class AssignGearItem : MonoBehaviour {
    [SerializeField] private Image icon;
    [SerializeField] private Image frame;

    private GearSoftData data;
    public void UpdateUI(GearSoftData data) {
        this.data = data;
        icon.sprite = data.GearHardData.Icon;
        frame.sprite = data.GearHardData.GetRarety(data.CurrentRank).Frame;
    }
}
