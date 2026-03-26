using Google.GData.Spreadsheets;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DocShipWindow : EditorWindow {
#if UNITY_EDITOR

    //select ship
    int selectedShip;
    string[] nameShips;
    int[] idShips;
    ShipData shipData;
    ShipInfor curShip;
    //
    bool enableEditSheet;

    // basic info
    CellPosition namePos = new CellPosition(2, 2);
    CellPosition desPos = new CellPosition(3, 2);
    CellPosition passivePos = new CellPosition(4, 2);
    // level info
    CellPosition chipStartPos = new CellPosition(7, 2);
    CellPosition hpStartPos = new CellPosition(7, 3);
    CellPosition atkStartPos = new CellPosition(7, 4);

    [MenuItem("Window/DocShipWindow")]
    static void Init() {
        // Get existing open window or if none, make a new one:
        DocShipWindow window = (DocShipWindow)EditorWindow.GetWindow(typeof(DocShipWindow));
        window.Show();
    }

    private void OnEnable() {
        shipData = GameResources.Instance.Ship;
        nameShips = new string[shipData.Datas.Count];
        idShips = new int[shipData.Datas.Count];
        for (int i = 0; i < shipData.Datas.Count; ++i) {
            nameShips[i] = shipData.Datas[i].Name;
            idShips[i] = shipData.Datas[i].ID;
        }
        selectedShip = idShips[0];
    }

    void OnGUI() {
        GUILayout.Label("Select Ship", EditorStyles.boldLabel);
        selectedShip = EditorGUILayout.IntPopup("Select Ship: ", selectedShip, nameShips, idShips);
        foreach (var s in shipData.Datas) {
            if (s.ID == selectedShip) {
                curShip = s;
                break;
            }
        }

        enableEditSheet = EditorGUILayout.BeginToggleGroup("Sheet Name", enableEditSheet);
        GUILayout.Label("Choose Sheet", EditorStyles.boldLabel);
        curShip.spreadSheetName = EditorGUILayout.TextField("Spread Sheet", curShip.spreadSheetName);
        curShip.workSheetName = EditorGUILayout.TextField("Work Sheet", curShip.workSheetName);
        EditorGUILayout.EndToggleGroup();

        GUILayout.Label("Basic Info Position", EditorStyles.boldLabel);
        namePos.r = EditorGUILayout.IntField("Name Row", namePos.r);
        namePos.c = EditorGUILayout.IntField("Name Col", namePos.c);
        desPos.r = EditorGUILayout.IntField("Description Row", desPos.r);
        desPos.c = EditorGUILayout.IntField("Description Col", desPos.c);
        passivePos.r = EditorGUILayout.IntField("Passive Row", passivePos.r);
        passivePos.c = EditorGUILayout.IntField("Passive Col", passivePos.c);

        GUILayout.Label("Level Info Position", EditorStyles.boldLabel);
        chipStartPos.r = EditorGUILayout.IntField("Chip Row", chipStartPos.r);
        chipStartPos.c = EditorGUILayout.IntField("Chip Col", chipStartPos.c);
        hpStartPos.r = EditorGUILayout.IntField("HP Row", hpStartPos.r);
        hpStartPos.c = EditorGUILayout.IntField("HP Col", hpStartPos.c);
        atkStartPos.r = EditorGUILayout.IntField("ATK Row", atkStartPos.r);
        atkStartPos.c = EditorGUILayout.IntField("ATK Col", atkStartPos.c);

        if (GUILayout.Button("Read")) {
            Read();
            EditorUtility.SetDirty(shipData);
            AssetDatabase.SaveAssets();
        }

        EditorGUILayout.ObjectField(shipData, typeof(ShipData), false);
    }

    private void Read() {

        var docData = ReadGoogleSheetHelper.DoCellQuery(curShip.spreadSheetName, curShip.workSheetName);
        if (docData == null) {
            return;
        }
        ReadBasicInfo(docData);
        ReadLevelInfo(docData);
    }

    private void ReadBasicInfo(List<CellEntry> docData) {
        curShip.Name = docData.GetStringCell(namePos.r, namePos.c);
        curShip.Description = docData.GetStringCell(desPos.r, desPos.c);
        curShip.ExtDescription = docData.GetStringCell(passivePos.r, passivePos.c);

    }

    private void ReadLevelInfo(List<CellEntry> docData) {
        int maxRow = docData.GetMaxRow(chipStartPos.c);
        int maxLevel = maxRow - chipStartPos.r + 1;
        curShip.Levels.Clear();
        for (int i = 0; i < maxLevel; ++i) {
            ShipInfor.ShipLevelInfor levelInfor = new ShipInfor.ShipLevelInfor();
            levelInfor.Price = new ItemStack(ConstantItemID.ChipId, docData.GetIntFromCell(chipStartPos.r + i, chipStartPos.c));
            levelInfor.Attack = new StatModifier(docData.GetIntFromCell(atkStartPos.r + i, atkStartPos.c), StatModType.Flat);
            levelInfor.HP = new StatModifier(docData.GetIntFromCell(hpStartPos.r + i, hpStartPos.c), StatModType.Flat);
            curShip.Levels.Add(levelInfor);
        }
    }
#endif
}
