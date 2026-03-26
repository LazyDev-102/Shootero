using System;

namespace Puppy.Engine.SaveData {
    public class CloudFileReader : IFileReader {
        public string Path => throw new NotImplementedException();

        public bool Exits() {
            throw new NotImplementedException();
        }

        public void Read(Action<string> onCompleted, Action onFailed) {
            throw new NotImplementedException();
        }
    }
}
