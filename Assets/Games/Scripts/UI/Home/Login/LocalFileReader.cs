using System;
using System.IO;

namespace Puppy.Engine.SaveData {
    public class LocalFileReader : IFileReader {
        private string name;
        private string directory;

        public string Path => System.IO.Path.Combine(directory, name);

        public bool Exits() {
            return File.Exists(Path);
        }

        public LocalFileReader(string directory, string name) {
            this.directory = directory;
            this.name = name;
        }

        public void Read(Action<string> onCompleted, Action onFailed) {
            try {
                if (Exits()) {
                    using (StreamReader reader = File.OpenText(Path)) {
                        string data = reader.ReadToEnd();

                        if (Gemmob.Logs.IsEnable) {
                            Gemmob.Logs.Log($"[DATA] Read file completed.\n <path>: {Path}\n <content>: {data}");
                        }
                        onCompleted?.Invoke(data);
                    }
                }
                else {
                    if (Gemmob.Logs.IsEnable) {
                        Gemmob.Logs.Log($"[DATA] Read file no found.\n <path>: {Path}");
                    }
                    onCompleted?.Invoke(string.Empty);
                }
            }
            catch (Exception e) {
                if (Gemmob.Logs.IsEnable) {
                    Gemmob.Logs.LogError($"[DATA] Read file failed.\n <path>: {Path}\n <error>: {e}");
                }
                onFailed?.Invoke();
            }
        }
    }
    public static partial class LocalFileExtensions {
        public static void ReadFromLocal(this IFileData fileData, string directory, string name, Action onCompleted = null, Action onFailed = null) {
            LocalFileReader reader = new LocalFileReader(directory, name);
            reader.Read((data) => {
                fileData.FromStringData(data);
                onCompleted?.Invoke();
            }, onFailed);
        }
    }

}