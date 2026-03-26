using Gear_Data;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ClaimedItemView : ItemView {
    [SerializeField] private Image imgBorder;
    [SerializeField] private ItemCollector gearCollector;
    public override void Show() {
        base.Show();
        foreach (var g in gearCollector.Items) {
            if (g.Id == Model.Id) {
                if (g is GearHardData gear) {
                    imgBorder.sprite = gear.GetRarety(0).Frame;
                }
            }
        }
    }
    public void SetBorder(int id, int rank) {
        var gear = gearCollector.Items.FirstOrDefault(x => x.Id == id);
        if (gear != null && gear is GearHardData g) {
            imgBorder.sprite = g.GetRarety(rank).Frame;
        }
    }
}
