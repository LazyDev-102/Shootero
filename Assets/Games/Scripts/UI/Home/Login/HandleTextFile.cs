using System.IO;

public class HandleTextFile {
    public static void WriteString(string path, string content) {
        if (!File.Exists(path)) {
            File.Create(path).Dispose();
        }
        File.WriteAllText(path, content);
    }

    public static void ReadString(string path, out string result) {
        if (!File.Exists(path)) {
            result = "";
        }
        else {
            result = File.ReadAllText(path).Trim();
        }
    }
}