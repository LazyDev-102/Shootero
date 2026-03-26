using DG.Tweening;
using Gemmob;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowModInfoDisplay : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI modName;
    [SerializeField] private TextMeshProUGUI modDescription;
    [SerializeField] private RectTransform newModTrans;

    private List<ModData> modInfors;
    private List<GameObject> modInforItems;
    private bool finishShowNewMod;
    public void Assign() {
        modInfors = new List<ModData>();
        modInforItems = new List<GameObject>();
    }
    public void AddModInfor(ModData mod) {
        if (mod.ModId == 1001 || mod.ModId == 1002 || mod.ModId == 1003 || mod.ModId == 1004)
            return;
        modInfors.Add(mod);
    }
    public void RemoveAllModInfor() {
        lock (modInforItems) {
            foreach (var item in modInforItems) {
                Destroy(item);
            }
        }
        modInforItems.Clear();
        modInfors.Clear();
    }
    [SerializeField] private NewModInforItemDisplayer modInforItemDisplayerPrefab;
    [SerializeField] private Transform modInforParent;
    public void ShowNewModInfo() {
        if (modInfors.Count == 0)
            return;
        CreateModInforItems1();
        ModInforAnimation(800, 52 + 90 * modInfors.Count);
    }
    private void CreateModInforItems() {
        for (int i = 0; i < modInfors.Count; i++) {
            var item = modInforItemDisplayerPrefab.Spawn(modInforParent);
            item.gameObject.SetActive(true);
            item.SetInfor(modInfors[i].NameMod, modInfors[i].ModDescription)
                .PlayAnimation(1.5f + i);
            modInforItems.Add(item.gameObject);
        }
    }
    private void CreateModInforItems1() {
        modInforParent.GetComponent<VerticalLayoutGroup>().enabled = false;
        for (int i = 0; i < modInfors.Count; i++) {
            var item = modInforItemDisplayerPrefab.Spawn(modInforParent);
            item.gameObject.SetActive(true);
            item.SetInfor(modInfors[i].NameMod, modInfors[i].ModDescription)
                .PlayAnimation1(1.5f + i, i);
            modInforItems.Add(item.gameObject);
        }
    }
    private void ModInforAnimation(int lengthX, int lengthY) {
        newModTrans.gameObject.SetActive(true);
        newModTrans.DOSizeDelta(new Vector2(lengthX, lengthY), 0.5f).OnComplete(() => {
            DOVirtual.DelayedCall(5f, () => {
                newModTrans.DOSizeDelta(new Vector2(lengthX, 0), 0.5f).OnComplete(() => {
                    newModTrans.gameObject.SetActive(false);
                    newModTrans.sizeDelta = new Vector2(0, lengthY);
                    RemoveAllModInfor();
                });
            });
        });
    }
    public void ShowNewModInfo(ModData mod) {
        if (finishShowNewMod) {
            DOVirtual.DelayedCall(4f, () => ShowNewModInfo(mod));
            return;
        }
        if (mod.ModId == 1001 || mod.ModId == 1002 || mod.ModId == 1003 || mod.ModId == 1004)
            return;
        finishShowNewMod = true;
        modName.text = mod.NameMod;
        modDescription.text = mod.ModDescription;
        newModTrans.gameObject.SetActive(true);
        newModTrans.DOSizeDelta(new Vector2(720, 140), 0.5f).OnComplete(() => {
            DOVirtual.DelayedCall(3f, () => {
                newModTrans.DOSizeDelta(new Vector2(720, 0), 0.5f).OnComplete(() => {
                    newModTrans.gameObject.SetActive(false);
                    newModTrans.sizeDelta = new Vector2(0, 140);
                    finishShowNewMod = false;
                });
            });
        });
    }
    public void ShowNewMod() {
        if (modInfors.Count == 0)
            return;
        modName.text = modInfors[0].NameMod;
        modDescription.text = modInfors[0].ModDescription;
        newModTrans.gameObject.SetActive(true);
        newModTrans.DOSizeDelta(new Vector2(720, 140), 0.5f).OnComplete(() => {
            DOVirtual.DelayedCall(3f, () => {
                newModTrans.DOSizeDelta(new Vector2(720, 0), 0.5f).OnComplete(() => {
                    newModTrans.gameObject.SetActive(false);
                    newModTrans.sizeDelta = new Vector2(0, 140);
                    modInfors.RemoveAt(0);
                    ShowNewMod();
                });
            });
        });
    }
}
