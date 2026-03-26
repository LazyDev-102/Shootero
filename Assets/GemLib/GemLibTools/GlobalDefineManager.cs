using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

namespace Gemmob.Lib.Tools
{
    public static class GlobalDefineManager
    {
#if UNITY_EDITOR
        public static void UpdateDefineSymbols(List<string> addedSymbols, List<string> removedSymbols)
        {
            UpdateDefineSymbols(addedSymbols, removedSymbols, BuildTargetGroup.Android);
            UpdateDefineSymbols(addedSymbols, removedSymbols, BuildTargetGroup.iOS);
        }

        public static void UpdateDefineSymbols(List<string> addedSymbols, List<string> removedSymbols, BuildTargetGroup buildTarget)
        {
            string symbolStr = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTarget);
            List<string> currentSymbols = new List<string>(symbolStr.Split(';'));
            int addCount = 0, removeCount = 0;

            foreach (string symbol in addedSymbols)
                if (!currentSymbols.Contains(symbol))
                {
                    currentSymbols.Add(symbol);
                    addCount++;
                }

            foreach (string symbol in removedSymbols)
                if (currentSymbols.Contains(symbol))
                {
                    currentSymbols.Remove(symbol);
                    removeCount++;
                }

            if (addCount > 0 || removeCount > 0)
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < currentSymbols.Count; i++)
                {
                    sb.Append(currentSymbols[i]);
                    if (i < currentSymbols.Count - 1)
                        sb.Append(";");
                }

                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTarget, sb.ToString());
            }
        }
 #endif

    }
}