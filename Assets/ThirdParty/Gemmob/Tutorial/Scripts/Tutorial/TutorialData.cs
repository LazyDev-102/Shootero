using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gemmob.Tutorial {

    //[CreateAssetMenu(fileName = "TutorialData", menuName = "Datas/TutorialData")]
    public class TutorialData : ScriptableObject {
        public List<TutorialInfor> Data;
    }

    [Serializable]
    public class TutorialInfor {
        public string Key;
        public string NeedToDoneKey = string.Empty;
        public string NotShowWhenHasKey = string.Empty;
        public TutorialDescriptInfor[] DescriptInfor = new TutorialDescriptInfor[0];
        public CharacterTutorial[] Character;
        public bool IsSkip = false;
        public bool SaveEndStage = true;
        public bool SaveWhenStart = false;
        public int SaveAtStep = -1;
#if UNITY_EDITOR
        public bool IsShow = true;
#endif
    }

    [Serializable]
    public class TutorialDescriptInfor {
        public string Description = "New description";
        public GameObject Target;

        public TargetType TargetType;
        public bool IsPause;
        public HighlightType HighlightType;
        public DescriptType DescriptType;
        public PointerPos PointerPos;
        public CharacterTutorial MainCharacter;
#if UNITY_EDITOR
        public bool IsShow = true;
#endif

        public TutorialDescriptInfor() {
            TargetType = TargetType.None;
            HighlightType = HighlightType.None;
            DescriptType = DescriptType.TapToNext;
            PointerPos = PointerPos.None;
        }

        public TutorialDescriptInfor(TutorialDescriptInfor d) {
            Description = d.Description;
            Target = d.Target;

            TargetType = d.TargetType;
            IsPause = d.IsPause;
            HighlightType = d.HighlightType;
            DescriptType = d.DescriptType;
            PointerPos = d.PointerPos;
            MainCharacter = d.MainCharacter;
        }
    }

    public enum DescriptType {
        TapToNext, ClickTarget
    }

    public enum HighlightType {
        Target, Detail, None
    }

    public enum TargetType {
        NeedTarget, KeepOldTarget, None
    }

    /// <summary>
    /// Can be edit for each project
    /// </summary>
    public enum TutorialKey {
        TutorialEquipment,
        TutorialIntroduce,
        TutorialOpenChest,
        TutorialPlayGame,
        TutorialPlayInfinity,
        TutorialEquipSkills,
        TutorialOpenSkill,
        None
    }

    /// <summary>
    /// Can be edit for each project
    /// </summary>
    public enum CharacterTutorial {
        None,
    }
}
