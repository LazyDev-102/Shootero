using System.Collections.Generic;

public static class ItemExtension {
    public static IEnumerable<ItemClaim> Open(this ItemClaim item) {
        ItemClaim itemClaim;
        if (item.Item is RandomItem randomItem) {
            for (int i = 0; i < item.Amount; i++) {
                itemClaim = new ItemClaim(randomItem.GetItem().Id, 1);
                yield return itemClaim;
            }
        }
        else {
            yield return item;
        }
    }

    public static IEnumerable<ItemClaim> Open(this IEnumerable<ItemClaim> items) {
        foreach (var item in items) {
            foreach (var ite in item.Open()) {
                yield return ite;
            }
        }
    }
    public static IEnumerable<Item> Open(this IEnumerable<Item> items) {
        foreach (var item in items) {
            foreach (var ite in item.Open()) {
                yield return ite;
            }
        }
    }
    public static IEnumerable<Item> Open(this Item item, int amount = 1) {
        if (item is RandomItem randomItem) {
            for (int i = 0; i < amount; i++) {
                foreach (var it in randomItem.GetItem(amount)) {
                    yield return it;
                }
            }
        }
        else {
            yield return item;
        }
    }
    public static readonly Dictionary<int, ItemClaim> stackCache = new Dictionary<int, ItemClaim>(16);

    public static IEnumerable<ItemClaim> Stack(this IEnumerable<ItemClaim> items) {
        stackCache.Clear();
        foreach (var item in items) {
            if (!stackCache.ContainsKey(item.Id)) {
                stackCache.Add(item.Id, new ItemClaim(item.Id, 0));
            }
            stackCache[item.Id].Amount += item.Amount;
        }

        foreach (var item in stackCache) {
            yield return item.Value;
        }
    }
}