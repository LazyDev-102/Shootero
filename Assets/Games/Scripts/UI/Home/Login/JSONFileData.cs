using System.Collections.Generic;
using SimpleJSON;

namespace Puppy.Engine.SaveData {
    public class JSONFileData : IFileData<JSONNode> {

        private JSONNode node;

        public JSONFileData() {
            this.node = new JSONObject();
        }

        public JSONFileData(JSONNode node) {
            if (node.IsObject) {
                this.node = node;
            }
            else {
                this.node = new JSONObject();
            }
        }

        public IFileData Clone() {
            return new JSONFileData(node.Clone());
        }

        public bool HasKey(string key) {
            return node.HasKey(key);
        }

        public void DeleteAll() {
            node.Clear();
        }

        public void DeleteKey(string key) {
            node.Remove(key);
        }

        public IEnumerable<string> GetKeys() {
            foreach (string key in node.Keys) {
                yield return key;
            }
        }

        public string ToStringData() {
            string data = node.ToString();
            return data;
        }

        public void FromStringData(string data) {
            node = JSONNode.Parse(data);

            if (!node.IsObject) {
                node = new JSONObject();
            }
        }

        #region Raw
        public JSONNode GetRawData(string key) {
            return node[key];
        }

        public void SetRawData(string key, JSONNode value) {
            node[key] = value;
        }
        #endregion

        #region String
        public string GetString(string key, string defaultValue = null) {
            if (node.HasKey(key) && node[key].IsString) {
                return node[key].ToString();
            }

            return defaultValue;
        }

        public void SetString(string key, string value) {
            node[key] = new JSONString(value);
        }
        #endregion

        #region Int
        public int GetInt(string key, int defaultValue = 0) {
            if (node.HasKey(key) && node[key].IsNumber) {
                return node[key].AsInt;
            }

            return defaultValue;
        }

        public void SetInt(string key, int value) {
            node[key] = new JSONNumber(value);
        }
        #endregion

        #region Float
        public float GetFloat(string key, float defaultValue = 0) {
            if (node.HasKey(key) && node[key].IsNumber) {
                return node[key].AsFloat;
            }

            return defaultValue;
        }

        public void SetFloat(string key, float value) {
            node[key] = new JSONNumber(value);
        }
        #endregion

        #region Bool
        public bool GetBool(string key, bool defaultValue = false) {
            if (node.HasKey(key) && node[key].IsBoolean) {
                return node[key].AsBool;
            }

            return defaultValue;
        }

        public void SetBool(string key, bool value) {
            node[key] = new JSONBool(value);
        }
        #endregion
    }

}