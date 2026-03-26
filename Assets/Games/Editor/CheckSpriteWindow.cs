using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class CheckSpriteWindow : EditorWindow {

    Sprite selectSprite;

    [MenuItem("Window/Check Sprite")]
    static void Init() {
        // Get existing open window or if none, make a new one:
        CheckSpriteWindow window = (CheckSpriteWindow)EditorWindow.GetWindow(typeof(CheckSpriteWindow));
        window.Show();
    }

    void OnGUI() {
        selectSprite = (Sprite)EditorGUILayout.ObjectField(selectSprite, typeof(Sprite), false);
        if (selectSprite != null) {
            EditorGUI.DrawPreviewTexture(new Rect(25, 60, 100, 100), selectSprite.texture);
        }
        if (GUILayout.Button("Find")) {
            FindSprite();
        }
    }


    private void FindSprite() {
        string guid = string.Empty;
        long file;
        //AssetDatabase.TryGetGUIDAndLocalFileIdentifier(selectSprite, guid, file);



        //SpriteRenderer[] srs = FindObjectsOfType<SpriteRenderer>();
        //if (srs != null) {
        //    foreach (var s in srs) {
        //        if (s.sprite == selectSprite) {
        //            Debug.Log(ShowObjectPath(s.transform));
        //        }
        //    }
        //}

        //Image[] imgs = FindObjectsOfType<Image>();
        //if (imgs != null) {
        //    foreach (var i in imgs) {
        //        if (i.sprite == selectSprite) {
        //            Debug.Log(ShowObjectPath(i.transform));
        //        }
        //    }
        //}
    }

    private string ShowObjectPath(Transform t) {
        if (t.parent == null) {
            return $" => {t.name}";
        }
        return ShowObjectPath(t.parent) + $" => {t.name}";
    }
}
