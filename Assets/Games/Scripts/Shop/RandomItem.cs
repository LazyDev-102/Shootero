using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomItem", menuName = "Resource/Item/RandomItem", order = 2)]
public class RandomItem : Item {
    [SerializeField] private ItemCollector materialCollector;

    public ItemCollector MaterialCollector { get => materialCollector; }

    public override void Claim(int amount) {
        //int loop = 0;
        //Item item;
        //for (int i = 0; i < amount; i++) {
        //    loop = 0;
        //    do {
        //        item = Helper.RandomHelper.RandomInCollection(materialCollector.Items);
        //        loop++;
        //        if (loop > 10) {
        //            item = materialCollector.Items[0];
        //            break;
        //        }
        //    } while (item as RandomItem != null);
        //    item.Claim(amount);
        //}
    }
    public virtual IEnumerable<Item> GetItem(int amount) {
        for (int i = 0; i < amount; i++) {
            int loop = 0;
            Item item;
            do {
                item = Helper.RandomHelper.RandomInCollection(materialCollector.Items);
                loop++;
                if (loop > 10) {
                    item = materialCollector.Items[0];
                    break;
                }
            } while (item as RandomItem != null);
            yield return item;
        }
    }
    public virtual Item GetItem() {
        int loop = 0;
        Item item;
        do {
            item = Helper.RandomHelper.RandomInCollection(materialCollector.Items);
            loop++;
            if (loop > 10) {
                item = materialCollector.Items[0];
                break;
            }
        } while (item as RandomItem != null);
        item.Claim(1);
        if (GameManager.Initialized && GameManager.Instance.GameLoader != null) {
            GameManager.Instance.AddClaimedItem(item.Id, 1);
        }
        return item;
    }
}
