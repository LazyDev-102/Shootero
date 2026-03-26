using System;

namespace Puppy.Engine.SaveData {
    public interface IFileWriter {
        string Path { get; }

        bool Exits();

        void Write(string data, Action onCompleted, Action onFailed);
    }
}
