using SimpleJSON;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "XmasShopData", menuName = "Resource/Modes/Xmas/XmasShopData")]
public class XmasShopData : ScriptableObject {
    [SerializeField] private XmasPackItemData[] packs;
    [SerializeField] private int day;
    [SerializeField] private int year;

    public XmasPackItemData[] Packs { get => packs; }

    private void ResetPack() {
        foreach (var pack in packs) {
            pack.ResetData();
        }
    }

    public bool Exchangeable() {
        for (int i = 0; i < packs.Length; i++) {
            if (packs[i].Exchangebale)
                return true;
        }
        return false;
    }

    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.Day, day);
        node.Add(JsonKey.Year, year);

        JSONNode buyableNode = new JSONArray();
        for (int i = 0; i < packs.Length; i++) {
            buyableNode.Add(packs[i].BuyableRemain);
        }
        node.Add(JsonKey.ProgressS, buyableNode);

        return node;
    }

    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            day = DateTime.Now.DayOfYear - 1;
            year = DateTime.Now.Year;
            for (int i = 0; i < packs.Length; i++) {
                packs[i].ResetData();
            }
        }
        else {
            day = json[JsonKey.Day].AsInt;
            year = json[JsonKey.Year].AsInt;
            JSONArray buyable = json[JsonKey.ProgressS].AsArray;
            for (int i = 0; i < buyable.Count; i++) {
                packs[i].LoadData(buyable[i].AsInt);
            }

        }
        //CheckResetData();
    }

    public void ResetData() {
        year = DateTime.Now.Year;
        day = DateTime.Now.DayOfYear;
        ResetPack();
    }
}
