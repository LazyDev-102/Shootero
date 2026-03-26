using SimpleJSON;

[System.Serializable]
public class ItemSlot : ItemStack {
    public ItemSlot(int itemId, int amount) : base(itemId, amount) {

    }

    public void Stack(int amount) {
        this.a += amount;
    }

    public void Destack(int amount) {
        this.a -= amount;
        if (this.a < 0) {
            this.a = 0;
        }
    }
    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.ItemId, Id);
        node.Add(JsonKey.Amount, Amount);
        return node;
    }
}
