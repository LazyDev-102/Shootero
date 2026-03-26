using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GearDetailItemMaterialContainer : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private ItemCollector materialCollector;

    //public void UpdateUI(Sprite icon, string name, int valueLeft, int valueNeed) {
    //    this.icon.sprite = icon;
    //    valueText.text = valueLeft < valueNeed ? $"{name}: <color='red'>{valueLeft}</color> {valueNeed}" : $"{name}: <color='green'>{valueLeft}</color> {valueNeed}";
    //}
    public void UpdateUI(int matID, int valueLeft, int valueNeed) {
        var matItem = materialCollector.Items[GetIndex(matID)];
        this.icon.sprite = matItem.Icon;
        valueText.text = valueLeft < valueNeed ? $"{matItem.Name}: " + @"<color=""red""> " + $"{valueLeft}</color> /{valueNeed}" : $"{matItem.Name}: " + @"<color=""green"">" + $"{valueLeft}</color> /{valueNeed}";
    }
    private int GetIndex(int matID) {
        switch (matID) {
            case ConstantItemID.WeaponryMatId:
                return 0;
            case ConstantItemID.ShieldMatId:
                return 1;
            case ConstantItemID.ReatorMatId:
                return 2;
            case ConstantItemID.PropulsionMatId:
                return 3;
            case ConstantItemID.DroneMatId:
                return 4;
            default: return 0;
        }
    }
}
