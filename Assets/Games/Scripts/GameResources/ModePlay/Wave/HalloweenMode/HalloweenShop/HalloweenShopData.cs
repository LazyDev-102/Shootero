using SimpleJSON;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HalloweenShopData", menuName = "Resource/Modes/Halloween/HalloweenShopData")]
public class HalloweenShopData : ScriptableObject {
    [SerializeField] private HalloweenPackItemData[] packs;
    [SerializeField] private int day;
    [SerializeField] private int year;
    [SerializeField] private int session;

    private bool fixbug_1_3_20;//bug not exchange when change max from 27->81

    public HalloweenPackItemData[] Packs { get => packs; }

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
        node.Add(JsonKey.Progress, session);
        node.Add(JsonKey.Fixbug, fixbug_1_3_20);

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
            session = -1;
            fixbug_1_3_20 = true;
        }
        else {
            fixbug_1_3_20 = json.HasKey(JsonKey.Fixbug) ? json[JsonKey.Fixbug].AsBool : false;
            day = json[JsonKey.Day].AsInt;
            year = json[JsonKey.Year].AsInt;
            session = json[JsonKey.Progress].AsInt;
            JSONArray buyable = json[JsonKey.ProgressS].AsArray;
            for (int i = 0; i < buyable.Count; i++) {
                packs[i].LoadData(buyable[i].AsInt, fixbug_1_3_20);
            }
            fixbug_1_3_20 = true;

        }
        //CheckResetData();
    }

    public void CheckResetData() {
        if(GameResources.Instance.Halloween.Session != session) {
            //if (DateTime.Now.Year * 365 + DateTime.Now.DayOfYear > year * 365 + day) {
            session = GameResources.Instance.Halloween.Session;
            year = DateTime.Now.Year;
            day = DateTime.Now.DayOfYear;
            ResetPack();
        }
    }
}
