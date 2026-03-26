using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.Android;

public class TestClassssssss : MonoBehaviour, IComparer {
    [SerializeField] private Color[] color;
    [SerializeField] private float min;
    [SerializeField] private float max;

    private ArrayList array = new ArrayList();
    private Hashtable hashtable = new Hashtable();

    private void Update() {
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            Vision();
        }
    }
    public void Vision() {
        Debug.LogError(VisionUtility.GetColorBlindSafePalette(color, min, max));
        // Nhập vào 1 danh sách màu + độ chói => danh sách màu sắc người bị mù màu nhìn thấy và 1 biến int(không biết để làm gì)
    }
    public void AndroidDevideExample() {
        AndroidDevice.SetSustainedPerformanceMode(true);
    }

    public void TestArray() {
        List<string> temp = new List<string>();
        temp.Add("2");
        temp.Add("3");
        array.Add(1);
        array.AddRange(temp);
    }
    public void TestHashtable() {
        hashtable.Add(HashtableKey.Key1, 1);
        hashtable.Add(HashtableKey.Key2, 3);
        hashtable.Add(HashtableKey.Key3, 2);
    }

    public int Compare(object x, object y) {
        return 0;
    }

    [System.Serializable]
    public enum HashtableKey {
        Key1,
        Key2,
        Key3,
    }
}
