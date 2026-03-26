using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipContainer : ContainerBase<ShipInfor> {

    private List<ShipInfor> data;
    private ShipPanel shipPanel;
    protected override IEnumerable<ShipInfor> GetData() {
        data = GameResources.Instance.Ship.Datas;
        foreach (var item in data) {
            yield return item;
        }
    }
    public void ShowNotify(int level, Transform shipItemTrans) {
        shipPanel.ShowLockBarNotify(level, shipItemTrans);
    }
    public void SetParent(ShipPanel shipPanel) {
        this.shipPanel = shipPanel;
    }
    public void Reload() {
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}
