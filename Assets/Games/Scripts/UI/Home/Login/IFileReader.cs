using System;

namespace Puppy.Engine.SaveData {
    public interface IFileReader {
        string Path { get; }

        bool Exits();

        void Read(Action<string> onCompleted, Action onFailed);
    }
}
