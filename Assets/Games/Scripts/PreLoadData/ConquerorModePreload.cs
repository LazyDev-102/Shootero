using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ConquerorModePreload", menuName = "Resource/HardData/Preload/ConquerorModePreload")]
public class ConquerorModePreload : GameAction {
    public override void Execute(object user, Action onCompleted) {
        int curZoneIndex = IngameData.currentZoneIndex;
        var shipData = GameResources.Instance.Ship;
        var conquerorData = GameResources.Instance.ConquerorData;
        bool isTutorial = !GameResources.Instance.TutorialSytemData.FinishTutorialIntroduce;
        if (shipData.Trial) {
            conquerorData.TrialZone.PreloadIngame(curZoneIndex);
        }
        else
        if (isTutorial) {
            conquerorData.TutorialZone.PreloadIngame(0);
        }
        else {
            conquerorData.ZoneDatas[curZoneIndex].PreloadIngame(curZoneIndex);
        }
    }
}
