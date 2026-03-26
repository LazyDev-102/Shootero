using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Gemmob;

public abstract class ContainerBase<T> : MonoBehaviour {
    public GameObject itemPrefab;
    public Transform parent;
    protected List<ItemBase<T>> items = new List<ItemBase<T>>();
    public List<ItemBase<T>> Items => items;

    protected abstract IEnumerable<T> GetData();

    protected virtual void OnEnable() {
        Generate();
    }


    public ItemBase<T> GetItem(int index) {
        if (index < 0 || index >= Items.Count) {
            return null;
        }
        return items[index];
    }

    protected virtual void Generate() {

        var data = GetData();
        if (data != null) {
            for (int i = 0; i < data.Count(); i++) {

                if (items.Count == i) {
                    items.Add(CreateItem());
                }

                ItemBase<T> item = GetItem(i);
                if (item) {
                    item.UpdateUI(this, data.ElementAt(i));
                    item.gameObject.SetActive(true);
                }
            }

            for (int i = data.Count(); i < items.Count; i++) {
                ItemBase<T> item = GetItem(i);
                if (item) {
                    item.gameObject.SetActive(false);
                }
            }

            //for (int i = 0; i < data.Count(); i++) {
            //    GameObject obj = itemPrefab.Spawn(parent);
            //    obj.transform.localScale = Vector3.one;
            //    ItemBase<T> itemComponent = obj.GetComponent<ItemBase<T>>();
            //    itemComponent.UpdateUI(this, data.ElementAt(i));
            //    obj.SetActive(true);
            //    items.Add(itemComponent);
            //}
        }
    }


       
    private ItemBase<T> CreateItem() {
        GameObject obj = Instantiate(itemPrefab, parent);
        obj.transform.localScale = Vector3.one;
        ItemBase<T> itemComponent = obj.GetComponent<ItemBase<T>>();
        return itemComponent;
    }

       protected virtual void ResetItem() {
        //foreach (var item in items) {
        //    Destroy(item.gameObject);
        //    //DestroyImmediate(item.gameObject);
        //}

        //items = new List<ItemBase<T>>();
    }
}
