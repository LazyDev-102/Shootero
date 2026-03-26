using UnityEngine;
namespace Helper {
    public static class GamePlayHelper {
        public static Vector2 RotateDirection(this Vector2 orgin, float angle) {
            if (Mathf.Abs(Vector2.SignedAngle(orgin, Vector2.down)) < 2) {
                orgin = UnityHelper.Down;
            }
            return (Quaternion.AngleAxis(angle, Vector3.back) * orgin).normalized;
        }

        public static Vector2 RotateDirection(this Vector3 orgin, float angle) {
            if (Mathf.Abs(Vector3.SignedAngle(orgin, Vector3.down, Vector3.back)) < 2) {
                orgin = UnityHelper.Down;
            }
            return (Quaternion.AngleAxis(angle, Vector3.back) * orgin).normalized;
        }
    }
}