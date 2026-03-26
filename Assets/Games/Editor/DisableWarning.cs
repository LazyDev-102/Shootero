//using UnityEditor;
//using System.IO;
//using System.Text.RegularExpressions;

//public class WarningDisable : AssetPostprocessor {
//    /// add more warning type to here. Some common warning
//    /// 0169 : var nerver use
//    /// 0649 : var nerver assign (will annoying with [SerializableField] vars)
//    /// Eg: warnings = "0169, 0649"
//    static readonly string warnings = "0649, 0108, 0414, 0067, 0067,1717, 0168";

//    /// Secret method called by unity after it generates the solution
//    public static void OnGeneratedCSProjectFiles() {
//        string currentDir = Directory.GetCurrentDirectory();
//        string[] csprojFiles = Directory.GetFiles(currentDir, "*.csproj");
//        for (int i = 0; i < csprojFiles.Length; i++) {
//            FixProject(csprojFiles[i]);
//        }
//    }

//    static bool FixProject(string filePath) {
//        string content = File.ReadAllText(filePath);
//        // default .csproj is already disable 0169 warning
//        string searchString = "<NoWarn>0169</NoWarn>";
//        string replaceString = string.Format("<NoWarn>{0}</NoWarn>", warnings);

//        if (content.IndexOf(searchString) != -1) {
//            content = Regex.Replace(content, searchString, replaceString);
//            File.WriteAllText(filePath, content);
//            return true;
//        }
//        else {
//            return false;
//        }
//    }
//}