using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
namespace Gemmob.Tutorial {
    [CustomEditor(typeof(TutorialRegister))]
    [CanEditMultipleObjects]
    public class TutotialRegisterEditor : Editor {
        private TutorialRegister _register;
        private string[] _tutorialKeys;

        private void Save() {
            EditorUtility.SetDirty(_register);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void OnEnable() {
            ReloadTutorialKey();
            _register = (TutorialRegister)target;
        }

        public override void OnInspectorGUI() {
            //base.OnInspectorGUI();
            Undo.RecordObject(_register, "Tutorial Register");
            //GUILayout.Label("Made by Tan with love");
            if (GUILayout.Button("Save"))
                Save();
            GUI.backgroundColor = Color.gray;
            GUILayout.BeginVertical(EditorHelper.Background());
            GUI.backgroundColor = Color.white;
            GUILayout.Label("Tutorial Register", EditorHelper.Header());
            DisplayMenu();
            ShowAddBtn();
            GUILayout.EndVertical();
            serializedObject.ApplyModifiedProperties();
        }

        private void ReloadTutorialKey() {
            var values = Enum.GetValues(typeof(TutorialKey));
            _tutorialKeys = new string[values.Length + 1];
            _tutorialKeys[0] = "--- Empty ---";
            for (int i = 1; i < _tutorialKeys.Length; i++)
                _tutorialKeys[i] = values.GetValue(i - 1).ToString();
        }

        void DisplayMenu() {
            GUI.backgroundColor = Color.Lerp(Color.black, Color.white, 0.75f);
            EditorStyles.textArea.wordWrap = true;
            foreach (TutorialInfor infor in _register.Infors) {
                GUILayout.BeginVertical(EditorHelper.Background());
                GUILayout.BeginHorizontal();
                infor.Key = SetStringPopup("Key", infor.Key);
                ShowDeleteBtn(infor);
                GUILayout.EndHorizontal();
                infor.NotShowWhenHasKey = SetStringPopup("Not show when has key:", infor.NotShowWhenHasKey);
                infor.NeedToDoneKey = SetStringPopup("Need to done key:", infor.NeedToDoneKey);
                infor.Character = CharacterNeeded(infor.Character);
                if (infor.IsShow) {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Can skip:");
                    infor.IsSkip = EditorGUILayout.Toggle(infor.IsSkip);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Save when end stage:");
                    infor.SaveEndStage = EditorGUILayout.Toggle(infor.SaveEndStage);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Save when start:");
                    infor.SaveWhenStart = EditorGUILayout.Toggle(infor.SaveWhenStart);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Label(new GUIContent("Save at step:", "Default at -1"));
                    infor.SaveAtStep = EditorGUILayout.IntField(infor.SaveAtStep);
                    GUILayout.EndHorizontal();

                    GUILayout.Label("Description infor:", EditorHelper.HeaderBold());
                    ShowAddDescriptInforBtn(ref infor.DescriptInfor);
                    for (int i = 0; i < infor.DescriptInfor.Length; i++) {
                        GUILayout.BeginVertical();

                        GUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(i.ToString());

                        if (i < infor.DescriptInfor.Length - 1)
                            if (GUILayout.Button("down")) {
                                TutorialDescriptInfor d = new TutorialDescriptInfor(infor.DescriptInfor[i]);
                                infor.DescriptInfor[i] = new TutorialDescriptInfor(infor.DescriptInfor[i + 1]);
                                infor.DescriptInfor[i + 1] = new TutorialDescriptInfor(d);
                            }

                        if (i > 0)
                            if (GUILayout.Button("up")) {
                                TutorialDescriptInfor d = new TutorialDescriptInfor(infor.DescriptInfor[i]);
                                infor.DescriptInfor[i] = new TutorialDescriptInfor(infor.DescriptInfor[i - 1]);
                                infor.DescriptInfor[i - 1] = new TutorialDescriptInfor(d);
                            }
                        GUILayout.EndHorizontal();

                        var descriptInfor = infor.DescriptInfor[i];

                        GUILayout.BeginHorizontal();

                        descriptInfor.Description = GUILayout.TextArea(descriptInfor.Description, GUILayout.ExpandHeight(true));
                        ShowDeleteDescriptInforBtn(ref infor.DescriptInfor, descriptInfor);
                        GUILayout.EndHorizontal();

                        IsShowDescriptInforBtn(descriptInfor);
                        if (descriptInfor.IsShow) {
                            GUILayout.BeginHorizontal();
                            {
                                GUILayout.BeginVertical();
                                GUILayout.Label("Target type:");
                                descriptInfor.TargetType = (TargetType)EditorGUILayout.EnumPopup(descriptInfor.TargetType);
                                GUILayout.EndVertical();

                                GUILayout.BeginVertical();
                                GUILayout.Label("Highlight type:");
                                descriptInfor.HighlightType = (HighlightType)EditorGUILayout.EnumPopup(descriptInfor.HighlightType);
                                GUILayout.EndVertical();

                                GUILayout.BeginVertical();
                                GUILayout.Label("Description type:");
                                descriptInfor.DescriptType = (DescriptType)EditorGUILayout.EnumPopup(descriptInfor.DescriptType);
                                GUILayout.EndVertical();

                                GUILayout.BeginVertical();
                                GUILayout.Label("Talker:");
                                descriptInfor.MainCharacter = (CharacterTutorial)EditorGUILayout.EnumPopup(descriptInfor.MainCharacter);
                                GUILayout.EndVertical();

                                GUILayout.BeginVertical();
                                GUILayout.Label("Pointer pos:");
                                descriptInfor.PointerPos = (PointerPos)EditorGUILayout.EnumPopup(descriptInfor.PointerPos);
                                GUILayout.EndVertical();
                            }
                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Pause:");
                            descriptInfor.IsPause = EditorGUILayout.Toggle(descriptInfor.IsPause);
                            GUILayout.EndHorizontal();
                        }

                        GUILayout.EndVertical();
                        EditorHelper.DrawUILine(Color.white);
                    }
                }
                IsShowInforBtn(infor);
                GUILayout.EndVertical();
                EditorHelper.DrawUILine(Color.black);
            }
            GUI.backgroundColor = Color.white;
        }

        private CharacterTutorial[] CharacterNeeded(CharacterTutorial[] characters) {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Main character:");

            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();

            if (characters == null)
                characters = new CharacterTutorial[0];

            if (GUILayout.Button("+", GUILayout.ExpandWidth(false))) {
                CharacterTutorial[] newChar = new CharacterTutorial[characters.Length + 1];

                for (int j = 0; j < newChar.Length && j < characters.Length; j++) {
                    newChar[j] = characters[j];
                }

                characters = newChar;
            }
            if (GUILayout.Button("-", GUILayout.ExpandWidth(false))) {
                CharacterTutorial[] newChar = new CharacterTutorial[characters.Length - 1];

                for (int j = 0; j < newChar.Length; j++) {
                    newChar[j] = characters[j];
                }
                characters = newChar;
            }
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < characters.Length; i++) {
                characters[i] = (CharacterTutorial)EditorGUILayout.EnumPopup(characters[i]);
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            return characters;
        }

        #region Tutorial Infor
        void ShowDeleteBtn(TutorialInfor infor) {
            if (GUILayout.Button("-", EditorHelper.SquareOption(25))) {
                DeleteInfor(infor);
            }
        }

        void ShowAddBtn() {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("+", EditorHelper.SquareOption(25))) {
                AddNewInfor();
            }
            GUILayout.EndHorizontal();
        }

        void AddNewInfor() {
            List<TutorialInfor> infors = _register.Infors.ToList();
            infors.Add(new TutorialInfor());
            _register.Infors = infors.ToArray();
        }

        void DeleteInfor(TutorialInfor deleteInfor) {
            List<TutorialInfor> infors = _register.Infors.ToList();
            infors.Remove(deleteInfor);
            _register.Infors = infors.ToArray();
        }

        void IsShowInforBtn(TutorialInfor infor) {
            if (GUILayout.Button(infor.IsShow ? "Hide" : "Show")) {
                infor.IsShow = !infor.IsShow;
            }
        }
        #endregion

        #region Tutorial Descript Infor

        void AddNewDescriptInfor(ref TutorialDescriptInfor[] infors) {
            List<TutorialDescriptInfor> lsInfor = infors.ToList();
            lsInfor.Add(new TutorialDescriptInfor());
            infors = lsInfor.ToArray();
        }

        void DeleteDescriptInfor(ref TutorialDescriptInfor[] infors, TutorialDescriptInfor deleteInfor) {
            List<TutorialDescriptInfor> lsInfor = infors.ToList();
            lsInfor.Remove(deleteInfor);
            infors = lsInfor.ToArray();
        }

        void ShowAddDescriptInforBtn(ref TutorialDescriptInfor[] infors) {
            GUILayout.BeginHorizontal();
            //GUILayout.FlexibleSpace();
            if (GUILayout.Button("+", EditorHelper.SquareOption(25))) {
                AddNewDescriptInfor(ref infors);
            }
            GUILayout.EndHorizontal();
            EditorHelper.DrawUILine(Color.white);
        }

        void ShowDeleteDescriptInforBtn(ref TutorialDescriptInfor[] infors, TutorialDescriptInfor deleteInfor) {
            if (GUILayout.Button("-", EditorHelper.SquareOption(25))) {
                DeleteDescriptInfor(ref infors, deleteInfor);
            }
        }

        void IsShowDescriptInforBtn(TutorialDescriptInfor descriptInfor) {
            if (GUILayout.Button(descriptInfor.IsShow ? "Hide" : "Show more detail", GUILayout.ExpandWidth(false))) {
                descriptInfor.IsShow = !descriptInfor.IsShow;
            }
        }

        #endregion

        public int FindIndex(string value) {
            for (int i = 0; i < _tutorialKeys.Length; i++)
                if (String.CompareOrdinal(value, _tutorialKeys[i]) == 0)
                    return i;

            return -1;
        }

        public string SetStringPopup(string label, string value) {
            var index = FindIndex(value);
            index = EditorGUILayout.Popup(label, index, _tutorialKeys);
            return index <= 0 ? string.Empty : _tutorialKeys[index];
        }
    }
}
#endif