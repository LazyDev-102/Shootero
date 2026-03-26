using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Gemmob.Tutorial {
	public class EditorHelper : Editor {
		public static GUIStyle Header(int fontSize = 25) {
			GUIStyle style = new GUIStyle();
			style.fontSize = fontSize;
			style.normal.textColor = Color.Lerp(Color.white, Color.gray, 0.5f);
			style.fontStyle = FontStyle.BoldAndItalic;
			style.alignment = TextAnchor.MiddleCenter;
			return style;
		}

		public static GUIStyle HeaderBold() {
			GUIStyle style = new GUIStyle();
			style.alignment = TextAnchor.MiddleLeft;
			style.fontStyle = FontStyle.Bold;
			return style;
		}

		public static GUIStyle Background() {
			GUIStyle style = new GUIStyle(EditorStyles.textArea);
			style.overflow.left = 50;
			style.overflow.right = 50;
			style.overflow.top = 0;
			style.overflow.bottom = 0;
			return style;
		}

		public static GUILayoutOption[] SquareOption(float size) {
			return new GUILayoutOption[] { GUILayout.Height(size), GUILayout.Width(size) };
		}

		public static void DrawUILine(Color color, int thickness = 2, int padding = 10) {
			Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
			r.height = thickness;
			r.y += padding / 2;
			r.x -= 2;
			r.width += 6;
			EditorGUI.DrawRect(r, color);
		}
	}
}
