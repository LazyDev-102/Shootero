

using System.Collections.Generic;
using UnityEngine;

namespace Helper {
    public static class BorderHelper {
        private static List<EdgeBorder> GetListEdgeBorderSpawn(AreaType type, float offset) {
            float w = ConfigIngameData.borderW;
            float h = ConfigIngameData.borderH;
            Vector2 topLeft = new Vector2(-(w / 2.0f + offset), h / 2.0f + offset);
            Vector2 topRight = new Vector2((w / 2.0f + offset), h / 2.0f + offset);
            Vector2 botLeft = new Vector2(-(w / 2.0f + offset), -(h / 2.0f + offset));
            Vector2 botRight = new Vector2(w / 2.0f + offset, -(h / 2.0f + offset));

            List<EdgeBorder> edges = new List<EdgeBorder>();
            EdgeBorder top = new EdgeBorder(topLeft, topRight);
            edges.Add(top);
            float value = 0;
            switch (type) {
                case AreaType.All: {
                    value = 1;
                    EdgeBorder bot = new EdgeBorder(botLeft, botRight);
                    edges.Add(bot);
                    break;
                }
                case AreaType.OneHalf: {
                    value = 0.5f;
                    break;
                }
                case AreaType.OneThirds: {
                    value = 0.33f;
                    break;
                }
                case AreaType.TwoThirds: {
                    value = 0.66f;
                    break;
                }
                case AreaType.OneQuarter: {
                    value = 0.25f;
                    break;
                }
                case AreaType.ThreeQuarter: {
                    value = 0.75f;
                    break;
                }
                case AreaType.RandomTop: {
                    return edges;
                }
            }

            EdgeBorder left = new EdgeBorder();
            left.begin = topLeft;
            EdgeBorder right = new EdgeBorder();
            right.begin = topRight;

            left.end = Vector2.Lerp(topLeft, botLeft, value);
            right.end = Vector2.Lerp(topRight, botRight, value);

            edges.Add(left);
            edges.Add(right);

            return edges;
        }
        public static Vector2 GetRandomPositionBorder(AreaType type, float offset = 1) {
            if (type == AreaType.CenterTop) {
                return new Vector2(0, ConfigIngameData.borderH / 2.0f + offset);
            }
            EdgeBorder edge = GetRandomEdge(type, offset);
            return edge.GetRandomMidPoint();
        }

        public static EdgeBorder GetRandomEdge(AreaType type, float offset) {
            List<EdgeBorder> edges = GetListEdgeBorderSpawn(type, offset);
            return RandomHelper.RandomInCollection(edges);
        }
        private static Vector2 GetPointMinArea(AreaType type) {
            float w = ConfigIngameData.borderW;
            float h = ConfigIngameData.borderH;

            float value = 0;
            switch (type) {
                case AreaType.All: {
                    value = 1;
                    break;
                }
                case AreaType.OneHalf: {
                    value = 0.5f;
                    break;
                }
                case AreaType.OneThirds: {
                    value = 0.33f;
                    break;
                }
                case AreaType.TwoThirds: {
                    value = 0.66f;
                    break;
                }
                case AreaType.OneQuarter: {
                    value = 0.25f;
                    break;
                }
                case AreaType.ThreeQuarter: {
                    value = 0.75f;
                    break;
                }
            }

            return new Vector2(-w / 2, h / 2 - h * value);
        }
        private static Vector2 GetPointMaxArea() {
            float deltaLayoutUI = 1;
            float w = ConfigIngameData.borderW;
            float h = ConfigIngameData.borderH;
            return new Vector2(w / 2, h / 2 - deltaLayoutUI);
        }
        public static Vector2 GetPoinRandomInArea(AreaType type) {
            Vector2 min = GetPointMinArea(type);
            Vector2 max = GetPointMaxArea();
            Vector2 result = new Vector2();
            result.x = Random.Range(min.x, max.x);
            result.y = Random.Range(min.y, max.y);
            return result;
        }

        public static Vector2 GetRandomPointBottomBorder(float offset) {
            return GetWorldPointInsideArea(new Vector2(offset, 0.05f));
        }
        ////////////// renew
        public static Vector2 GetWorldPointInsideArea(Area area) {
            return GetWorldPointInsideArea(GetRandomViewPointInsideArea(area));
        }
        public static Vector2 GetWorldPointInsideArea(Vector2 pointInArea) {
            Camera camera = CameraHelper.Camera;
            Vector2 randomPointInWorld = camera.ViewportToWorldPoint(new Vector3(pointInArea.x, pointInArea.y, camera.nearClipPlane));
            return randomPointInWorld;
        }

        public static Vector2 GetRandomViewPointInsideArea(Area area) {
            Vector2 pointInArea;
            pointInArea.x = Random.Range(area.pointBotLeft.x, area.pointTopRight.x);
            pointInArea.y = Random.Range(area.pointBotLeft.y, area.pointTopRight.y);
            return pointInArea;
        }

        public static bool IsOutBound(Vector2 position) {
            float halfW = ConfigIngameData.borderW / 2 + ConfigIngameData.offsetBorder;
            float halfH = ConfigIngameData.borderH / 2 + ConfigIngameData.offsetBorder;
            if (position.x > halfW || position.x < -halfW) {
                return true;
            }
            if (position.y > halfH || position.y < -halfH) {
                return true;
            }
            return false;
        }

        public static bool IsOutBound(Vector2 position, float offset) {
            float halfW = ConfigIngameData.borderW / 2 + offset;
            float halfH = ConfigIngameData.borderH / 2 + offset;
            if (position.x > halfW || position.x < -halfW) {
                return true;
            }
            if (position.y > halfH || position.y < -halfH) {
                return true;
            }
            return false;
        }
    }
    public struct EdgeBorder {
        public Vector2 begin;
        public Vector2 end;
        public EdgeBorder(Vector2 begin, Vector2 end) {
            this.begin = begin;
            this.end = end;
        }

        public Vector2 GetRandomMidPoint() {
            float lerpValue = Random.value;
            return Vector2.Lerp(begin, end, lerpValue);
        }

        public bool IsVertical(float e) {
            return Mathf.Abs(begin.x - end.x) <= e;
        }

        public bool IsHorizontal(float e) {
            return Mathf.Abs(begin.y - end.y) <= e;
        }
    }

    [System.Serializable]
    public struct Area {
        public Vector2 pointTopRight;
        public Vector2 pointBotLeft;

        public Area(Vector2 tr, Vector2 bl) {
            pointTopRight = tr;
            pointBotLeft = bl;
        }
    }
}
public enum AreaType {
    All = 0, OneHalf = 1, OneThirds = 2, TwoThirds = 3, OneQuarter = 4, ThreeQuarter = 5,
    CenterTop = 6, RandomTop = 7
}
