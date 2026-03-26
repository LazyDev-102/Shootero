using Gemmob;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Helper {
    public static class UnityHelper {

        public static Vector2 Down = new Vector2(0.03f, -0.97f);
        public static readonly string strNull = "null";
        public static SpriteRenderer ChangeAlpha(this SpriteRenderer g, float newAlpha) {
            var color = g.color;
            color.a = newAlpha;
            g.color = color;
            return g;
        }
        public static T ChangeAlpha<T>(this T g, float newAlpha)
         where T : Graphic {
            var color = g.color;
            color.a = newAlpha;
            g.color = color;
            return g;
        }

        public static ParticleSystem ChangeColorParticle(this ParticleSystem g, Color color) {
            ParticleSystem.MainModule main = g.main;
            ParticleSystem.MinMaxGradient mmColor = main.startColor;
            Color c = mmColor.color;
            float alpha = c.a;
            c = color;
            c.a = alpha;
            mmColor.color = c;
            main.startColor = mmColor;
            //g.main = main;
            return g;
        }

        public static T ChangeColor<T>(this T g, Color color)
             where T : Graphic {
            g.color = color;
            return g;
        }

        public static List<T> Clone<T>(this List<T> listToClone) where T : System.ICloneable {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        public static void HideTrail(this TrailRenderer trail) {
            HideTrail hider = trail.GetComponent<HideTrail>();
            if (hider == null) {
                hider = trail.gameObject.AddComponent<HideTrail>();
            }
            hider.Hide();
        }

        public static void ShowTrail(this TrailRenderer trail) {
            HideTrail hider = trail.GetComponent<HideTrail>();
            if (hider == null) {
                hider = trail.gameObject.AddComponent<HideTrail>();
            }
            hider.Show();
        }

        public static void DelayWait(this MonoBehaviour mono, float delay, Action onComplete) {
            mono.StartCoroutine(IDelayWait(delay, onComplete));
        }

        private static IEnumerator IDelayWait(float delay, Action onComplete) {
            yield return Yielder.Wait(delay);
            onComplete?.Invoke();
        }

        public static void DelayFrame(this MonoBehaviour mono, int numberFrame, Action onComplete) {
            mono.StartCoroutine(IDelayFrame(numberFrame, onComplete));
        }

        private static IEnumerator IDelayFrame(int numberFrame, Action onComplete) {
            for (int i = 0; i < numberFrame; ++i) {
                yield return null;
            }
            onComplete?.Invoke();
        }

        public static void Scale(this Transform transfrom, float scale) {
            transfrom.localScale = Vector3.one * scale;
        }

        public static void RotateLocalEuler(this Transform transform, float angle) {
            transform.localEulerAngles = new Vector3(0, 0, angle);
        }
        public static int ConvertToInt(this double value) {
            int newValue = (int)(value * 10) % 10;
            return newValue < 5 ? (int)value : (int)value + 1;
        }
        public static int ConvertToInt(this float value) {
            int newValue = (int)(value * 10) % 10;
            return newValue < 5 ? (int)value : (int)value + 1;
        }
        public static int Positive(this int value) {
            if (value < 0)
                return 0;
            return value;
        }
        public static double Positive(this double value) {
            if (value < 0)
                return 0;
            return value;
        }
        public static double PositiveOrDefault(this double value, int defaultValue) {
            if (value < 0)
                return defaultValue;
            return value;
        }
        public static void Positive(this double value, Action action) {
            if (value > 0)
                action?.Invoke();
        }
        public static void CompareAB(double value1, double value2, Action action) {
            if (value1 > value2)
                action?.Invoke();
        }
    }
}
