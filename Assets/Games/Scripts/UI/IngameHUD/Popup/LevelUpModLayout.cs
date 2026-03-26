using Gemmob;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelUpModLayout : MonoBehaviour, ILayout<LevelUpModItem, ModData> {
    public List<LevelUpModItem> Items { get; set; } = new List<LevelUpModItem>();

    [SerializeField] private LevelUpModItem itemPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private AlternateGridLayoutGroup gridLayout;
    [SerializeField] private ButtonExplorer backgroundButton;
    private ModData[] data;
    private void Awake() {
        backgroundButton.AddEvent(OnBackgroundClick);
    }
    public void GenerateItem() {
        if (Items != null && Items.Count > data.Length) {
            for (int i = 0; i < Items.Count; i++) {
                if (i < data.Length) {
                    Items[i].Initialized(data[i]);
                }
                Items[i].gameObject.SetActive(i < data.Length);
            }
        }
        else {
            for (int i = 0; i < data.Length; i++) {
                if (Items == null || i >= Items.Count) {
                    var modItem = itemPrefab.Spawn(container);
                    modItem.OnSelect(SetBlackBackgroundStatus);
                    Items.Add(modItem);
                }
                Items[i].Initialized(data[i]);
                Items[i].gameObject.SetActive(true);
            }
        }
    }
    public void UpdateUI(ModData[] data) {
        if (data == null)
            return;
        this.data = data;
        GenerateItem();
        //switch (Items.Count) {
        //    case int n when n > 8:
        //        gridLayout.constraintCount = 3;
        //        break;
        //    case int n when (n > 4 && n < 9):
        //        gridLayout.constraintCount = 2;
        //        gridLayout.spacing = new Vector2(70, 100);
        //        break;
        //    case int n when (n > 2 && n < 5):
        //        gridLayout.constraintCount = 1;
        //        gridLayout.spacing = new Vector2(70, 40);
        //        break;
        //    default:
        //        gridLayout.constraintCount = 1;
        //        gridLayout.spacing = new Vector2(150, 40);
        //        break;
        //}
    }

    public void PlayEffect() {
        foreach (var item in Items) {
            if (item != null) {
                item.PlayEffect();
            }

        }
    }
    public IEnumerator PlayWhiteEffect(float deltaTime) {
        foreach (var item in Items) {
            if (item != null) {
                item.PlayEffect();
            }
            yield return Yielder.Wait(deltaTime);
        }
    }
    private void SetBlackBackgroundStatus(bool status) {
        backgroundButton.gameObject.SetActive(status);
    }
    private void OnBackgroundClick() {
        backgroundButton.gameObject.SetActive(false);
        foreach (var item in Items) {
            if (item != null && item.gameObject.activeInHierarchy)
                item.OnDeSelect();
        }
    }
}
