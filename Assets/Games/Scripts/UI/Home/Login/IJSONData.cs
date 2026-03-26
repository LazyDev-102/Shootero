using SimpleJSON;

namespace Puppy.Engine.SaveData {
    public interface IJSONData {
        void Reset();
        void FromJSON(JSONNode node);
        JSONNode ToJSON();
    }
}

