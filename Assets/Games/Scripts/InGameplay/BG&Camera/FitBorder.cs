using Helper;
using UnityEngine;

public class FitBorder : MonoBehaviour {
    [SerializeField] private float offset = 1;
    [SerializeField] private BoxCollider2D top;
    [SerializeField] private BoxCollider2D bot;
    [SerializeField] private BoxCollider2D right;
    [SerializeField] private BoxCollider2D left;
    [SerializeField] private BoxCollider2D cameraBox;

    void Start() {
        float w = ConfigIngameData.borderW;
        float h = ConfigIngameData.borderH;

        Vector2 pointMax, pointMin;
        pointMax.x = w / 2 + left.size.x / 2 + offset;
        pointMax.y = h / 2 + top.size.y / 2 + offset;

        pointMin.x = -pointMax.x;
        pointMin.y = -pointMax.y;

        top.transform.position = new Vector2(0, pointMax.y);
        bot.transform.position = new Vector2(0, pointMin.y - 2);
        right.transform.position = new Vector2(pointMax.x, 0);
        left.transform.position = new Vector2(pointMin.x, 0);

        if (cameraBox) {
            Vector2 cameraBoxSize = new Vector2(CameraHelper.GetWidth, CameraHelper.GetHeight);
            cameraBox.size = cameraBoxSize;
        }
    }
}
