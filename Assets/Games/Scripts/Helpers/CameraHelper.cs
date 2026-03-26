using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helper {
    public static class CameraHelper {
        private static Camera camera;

        public static Camera Camera {
            get {
                if (camera == null) {
                    camera = Camera.main;
                }
                return camera;
            }
        }

        public static Vector2 LeftPosition {
            get {
                return (Vector2)Camera.ViewportToWorldPoint(new Vector3(0, 0.5f, camera.nearClipPlane));
            }
        }

        public static Vector2 RightPosition {
            get {
                return (Vector2)Camera.ViewportToWorldPoint(new Vector3(1, 0.5f, camera.nearClipPlane));
            }
        }

        public static Vector2 TopPosition {
            get {
                return (Vector2)Camera.ViewportToWorldPoint(new Vector3(0.5f, 1f, camera.nearClipPlane));
            }
        }

        public static Vector2 BotPosition {
            get {
                return (Vector2)Camera.ViewportToWorldPoint(new Vector3(0.5f, 0f, camera.nearClipPlane));
            }
        }

        public static float GetHeight {
            get {
                return Camera.orthographicSize * 2.0f;
            }
        }

        public static float GetWidth {
            get {
                return GetHeight * Camera.aspect;
            }
        }

        public static bool WorldPointInsideCameraView(Vector2 point) {
            Vector2 pointTopRight = Camera.ViewportToWorldPoint(new Vector3(1, 1, Camera.nearClipPlane));
            Vector2 pointBotLeft = Camera.ViewportToWorldPoint(new Vector3(0, 0, Camera.nearClipPlane));
            return point.x <= pointTopRight.x && point.x >= pointBotLeft.x && point.y <= pointTopRight.y && point.y >= pointBotLeft.y;
        }

        public static bool ObjectInsideCameraView(Vector2 position) {
            Vector2 positionInScreen = Camera.WorldToViewportPoint(position);
            if (positionInScreen.x > 0 && positionInScreen.x < 1 && positionInScreen.y > 0 && positionInScreen.y < 1) {
                return true;
            }
            return false;
        }
    }
}
