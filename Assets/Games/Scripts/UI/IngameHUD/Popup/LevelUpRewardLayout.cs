using Gemmob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpRewardLayout : MonoBehaviour, ILayout<LevelUpRewardItem, ItemStack> {
    public List<LevelUpRewardItem> Items { get; set; } = new List<LevelUpRewardItem>();

    [SerializeField] private LevelUpRewardItem itemPrefab;
    [SerializeField] private Transform container;
    private ItemStack[] data;
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
                    var itemClone = itemPrefab.Spawn(container);
                    Items.Add(itemClone);
                }
                Items[i].Initialized(data[i]);
                Items[i].gameObject.SetActive(true);
            }
        }
    }
    public void UpdateUI(ItemStack[] data) {
        if (data == null)
            return;
        this.data = data;
        GenerateItem();
    }
    public void PlayEffect() {
        foreach (var item in Items) {
            if (item != null) {
                item.PlayEffect();
            }
        }
    }
    public IEnumerator PlayWhiteEffect(float deltaTime, System.Action onComplete) {
        foreach (var item in Items) {
            if (item != null) {
                item.PlayEffect();
            }
            yield return Yielder.Wait(deltaTime);
        }
        onComplete?.Invoke();
    }
}
