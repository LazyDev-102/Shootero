using Gear_Data;
using Google.GData.Spreadsheets;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DocGearWindow : EditorWindow {
    // tab
    int curTab;

    // data
    List<GearHardData> gears;
    List<DroneGearHardData> droneGears;

    // basic info
    CellPosition idPos = new CellPosition(1, 2);
    CellPosition namePos = new CellPosition(2, 2);
    CellPosition desPos = new CellPosition(3, 2);
    CellPosition typePos = new CellPosition(4, 2);
    CellPosition orderPos = new CellPosition(5, 2);
    // level info
    CellPosition currencyIdPos = new CellPosition(9, 2);
    CellPosition materialIdPos = new CellPosition(9, 3);
    CellPosition statValueIdPos = new CellPosition(9, 4);
    CellPosition hpStatValueIdPos = new CellPosition(9, 4);
    CellPosition atkStatValueIdPos = new CellPosition(9, 5);


    //
    private GearType[] types = new GearType[] { GearType.Drone1, GearType.Propulsion, GearType.Reactor, GearType.Shield, GearType.Weapon };

    [MenuItem("Window/DocGearWindow")]
    static void Init() {
        // Get existing open window or if none, make a new one:
        DocGearWindow window = (DocGearWindow)EditorWindow.GetWindow(typeof(DocGearWindow));
        window.Show();
    }

    private void OnEnable() {
        curTab = 0;
        if (gears == null) {
            gears = new List<GearHardData>();
        }
        if (droneGears == null) {
            droneGears = new List<DroneGearHardData>();
        }
    }

    void OnGUI() {
        //Tab
        curTab = GUILayout.Toolbar(curTab, new string[] { "Gear", "Drone Gear" });

        switch (curTab) {
            case 0: {
                int newCount = Mathf.Max(0, EditorGUILayout.DelayedIntField("size", gears.Count));
                while (newCount < gears.Count)
                    gears.RemoveAt(gears.Count - 1);
                while (newCount > gears.Count)
                    gears.Add(null);

                for (int i = 0; i < gears.Count; i++) {
                    gears[i] = (GearHardData)EditorGUILayout.ObjectField(gears[i], typeof(GearHardData), false);
                }
                break;
            }
            case 1: {
                int newCount = Mathf.Max(0, EditorGUILayout.IntField("size", droneGears.Count));
                while (newCount < droneGears.Count)
                    droneGears.RemoveAt(droneGears.Count - 1);
                while (newCount > droneGears.Count)
                    droneGears.Add(null);

                for (int i = 0; i < droneGears.Count; i++) {
                    droneGears[i] = (DroneGearHardData)EditorGUILayout.ObjectField(droneGears[i], typeof(DroneGearHardData), false);
                }
                break;
            }
        }

        switch (curTab) {
            case 0: {
                if (GUILayout.Button("Read Gears")) {
                    foreach (var g in gears) {
                        ReadGear(g);
                    }
                }
                break;
            }
            case 1: {
                if (GUILayout.Button("Read Drone Gears")) {
                    foreach (var g in droneGears) {
                        ReadDroneGear(g);
                    }
                }
                break;
            }
        }
        AssetDatabase.SaveAssets();
    }


    private void ReadGear(GearHardData gear) {
        var docData = ReadGoogleSheetHelper.DoCellQuery(gear.spreadSheetName, gear.workSheetName);
        if (docData == null) {
            return;
        }
        ReadBasicInfo(gear, docData);
        ReadGearLevelInfo(gear, docData);
        EditorUtility.SetDirty(gear);
    }

    private void ReadDroneGear(DroneGearHardData gear) {
        var docData = ReadGoogleSheetHelper.DoCellQuery(gear.spreadSheetName, gear.workSheetName);
        if (docData == null) {
            return;
        }
        ReadBasicInfo(gear, docData);
        ReadDroneGearLevelInfo(gear, docData);
        EditorUtility.SetDirty(gear);
    }

    private void ReadBasicInfo(GearHardData gear, List<CellEntry> docData) {
        gear.Id = docData.GetIntFromCell(idPos.r, idPos.c);
        gear.Name = docData.GetStringCell(namePos.r, namePos.c);
        gear.Description = docData.GetStringCell(desPos.r, desPos.c);
        string typeName = docData.GetStringCell(typePos.r, typePos.c);
        gear.GearType = GetType(typeName);
        gear.Order = docData.GetIntFromCell(orderPos.r, orderPos.c);
    }

    private void ReadGearLevelInfo(GearHardData gear, List<CellEntry> docData) {
        int maxRow = docData.GetMaxRow(currencyIdPos.c);
        int maxLevel = maxRow - currencyIdPos.r;
        int idMat = docData.GetIntFromCell(materialIdPos.r, materialIdPos.c);
        int idCurrency = docData.GetIntFromCell(currencyIdPos.r, currencyIdPos.c);

        gear.Levels.Clear();
        if (gear.PrimaryStatDatas.Count == 0) {
            gear.PrimaryStatDatas.Add(new LevelStatData());
        }
        gear.PrimaryStatDatas[0].Values = new StatModifier[maxLevel];
        for (int i = 0; i < maxLevel; ++i) {
            LevelGear levelGear = new LevelGear();
            levelGear.SellPrices = new ItemClaim[0];
            levelGear.EnhanceRequire = new ItemStack[1];
            levelGear.EnhanceRequire[0] = new ItemStack(idMat, docData.GetIntFromCell(materialIdPos.r + i + 1, materialIdPos.c));
            levelGear.PriceUpgrade = new ItemStack(idCurrency, docData.GetIntFromCell(currencyIdPos.r + i + 1, currencyIdPos.c));
            gear.Levels.Add(levelGear);
            gear.PrimaryStatDatas[0].Values[i] = new StatModifier(docData.GetIntFromCell(statValueIdPos.r + i + 1, statValueIdPos.c), StatModType.Flat);
        }
    }

    private void ReadDroneGearLevelInfo(DroneGearHardData gear, List<CellEntry> docData) {
        int maxRow = docData.GetMaxRow(currencyIdPos.c);
        int maxLevel = maxRow - currencyIdPos.r;
        int idMat = docData.GetIntFromCell(materialIdPos.r, materialIdPos.c);
        int idCurrency = docData.GetIntFromCell(currencyIdPos.r, currencyIdPos.c);

        gear.Levels.Clear();
        if (gear.PrimaryStatDatas.Count == 0) {
            gear.PrimaryStatDatas.Add(new LevelStatData());
        }
        if (gear.PrimaryStatDatas.Count == 1) {
            gear.PrimaryStatDatas.Add(new LevelStatData());
        }
        gear.PrimaryStatDatas[0].Values = new StatModifier[maxLevel];
        gear.PrimaryStatDatas[1].Values = new StatModifier[maxLevel];

        for (int i = 0; i < maxLevel; ++i) {
            LevelGear levelGear = new LevelGear();
            levelGear.SellPrices = new ItemClaim[0];
            levelGear.EnhanceRequire = new ItemStack[1];
            levelGear.EnhanceRequire[0] = new ItemStack(idMat, docData.GetIntFromCell(materialIdPos.r + i + 1, materialIdPos.c));
            levelGear.PriceUpgrade = new ItemStack(idCurrency, docData.GetIntFromCell(currencyIdPos.r + i + 1, currencyIdPos.c));
            gear.Levels.Add(levelGear);
            gear.PrimaryStatDatas[0].Values[i] = new StatModifier(docData.GetIntFromCell(hpStatValueIdPos.r + i + 1, hpStatValueIdPos.c), StatModType.Flat);
            gear.PrimaryStatDatas[1].Values[i] = new StatModifier(docData.GetIntFromCell(atkStatValueIdPos.r + i + 1, atkStatValueIdPos.c), StatModType.Flat);
        }
    }

    private GearType GetType(string name) {
        foreach (var t in types) {
            if (name.Equals(t.ToString())) {
                return t;
            }
        }
        return GearType.All;
    }
}
