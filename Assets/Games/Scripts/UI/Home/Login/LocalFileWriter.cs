using System;
using System.IO;

namespace Puppy.Engine.SaveData {
    public class LocalFileWriter : IFileWriter {
        private string name;
        private string directory;

        public string Path => System.IO.Path.Combine(directory, name);

        public bool Exits() {
            return File.Exists(Path);
        }

        public LocalFileWriter(string directory, string name) {
            this.directory = directory;
            this.name = name;
        }

        public void Write(string data, Action onCompleted, Action onFailed) {
            if (!Directory.Exists(directory)) {
                Directory.CreateDirectory(directory);
            }

            try {
                using (StreamWriter writer = File.CreateText(Path)) {
                    writer.Write(data);

                    if (Gemmob.Logs.IsEnable) {
                        Gemmob.Logs.Log($"[DATA] Write file completed.\n <path>: {Path}\n <content>: {data}");
                    }
                    onCompleted?.Invoke();
                }
            }
            catch (Exception e) {
                if (Gemmob.Logs.IsEnable) {
                    Gemmob.Logs.LogError($"[DATA] Write file failed.\n <path>: {Path}\n <error>: {e}");
                }
                onFailed?.Invoke();
            }
        }
    }

    public static partial class LocalFileExtensions {
        public static void WriteToLocal(this IFileData fileData, string directory, string name, Action onCompleted = null, Action onFailed = null) {
            string data = fileData.ToStringData();

            LocalFileWriter writer = new LocalFileWriter(directory, name);
            writer.Write(data, onCompleted, onFailed);
        }
    }

}