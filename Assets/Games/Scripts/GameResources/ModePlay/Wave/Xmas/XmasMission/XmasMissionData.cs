using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System;

[CreateAssetMenu(fileName = "XmasMissionData", menuName = "Resource/Modes/Xmas/XmasMissionData")]
public class XmasMissionData : ScriptableObject {
    [SerializeField] private List<XmasMissionItemData> datas;

    public List<XmasMissionItemData> Missions { get => datas; }

    #region SaveLoad

    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            LoadDataInit();
        }
        else {
            LoadOwnerData(json);
        }
    }

    private void LoadDataInit() {
        foreach (var item in datas) {
            item.SetProgress(0);
            item.SetOnComplete(false);
            item.Assign();
        }
    }

    private void LoadOwnerData(JSONNode json) {
        JSONArray progressNode = json[JsonKey.ProgressS].AsArray;
        JSONArray isCompleteNode = json[JsonKey.IsCompleted].AsArray;
        for (int i = 0; i < progressNode.Count; i++) {
            if (i >= datas.Count)
                continue;
            datas[i].SetProgress(progressNode[i].AsInt);
            datas[i].SetOnComplete(isCompleteNode[i].AsBool);
            datas[i].Assign();
        }
    }

    public JSONNode Save2Json() {
        JSONNode json = new JSONObject();
        JSONNode progressNode = new JSONArray();
        JSONNode isCompleteNode = new JSONArray();
        for (int i = 0; i < datas.Count; i++) {
            progressNode.Add(datas[i].PointProgress);
            isCompleteNode.Add(datas[i].IsComplete);
        }
        json.Add(JsonKey.ProgressS, progressNode);
        json.Add(JsonKey.IsCompleted, isCompleteNode);
        return json;
    }

    public void ResetData() {
        for (int i = 0; i < datas.Count; i++) {
            datas[i].SetProgress(0);
            datas[i].SetOnComplete(false);
            datas[i].Unassign();
        }
        LoadDataInit();
    }
    #endregion
}
