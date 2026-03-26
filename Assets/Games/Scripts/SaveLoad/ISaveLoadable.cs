

public interface ISaveLoadable {
    string SaveToJson();
    void LoadFromJson(string json);
}
