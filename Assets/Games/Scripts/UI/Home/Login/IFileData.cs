using System.Collections.Generic;

namespace Puppy.Engine.SaveData {

    public interface IFileData {
        bool HasKey(string key);
        void DeleteKey(string key);
        void DeleteAll();
        IEnumerable<string> GetKeys();
        string ToStringData();
        void FromStringData(string data);
        IFileData Clone();

        #region String
        string GetString(string key, string defaultValue = null);
        void SetString(string key, string value);
        #endregion

        #region Int
        int GetInt(string key, int defaultValue = 0);
        void SetInt(string key, int value);
        #endregion

        #region Float
        float GetFloat(string key, float defaultValue = 0);
        void SetFloat(string key, float value);
        #endregion

        #region Bool
        bool GetBool(string key, bool defaultValue = false);
        void SetBool(string key, bool value);
        #endregion
    }

    public interface IFileData<T> : IFileData {
        #region Raw
        T GetRawData(string key);
        void SetRawData(string key, T value);
        #endregion
    }
}

