using UnityEngine;

public static class RectTransformExtension {
    public static RectTransform rectTransform(this Component comp) {
        return comp.GetComponent<RectTransform>();
    }

    public static UnityEngine.UI.Graphic SetAlpha(this UnityEngine.UI.Graphic img, float alpha) {
        var temp = img.color;
        temp.a = alpha;
        img.color = temp;
        return img;
    }
    public static UnityEngine.UI.Graphic SetColor(this UnityEngine.UI.Graphic graphic, Color color) {
        graphic.color = color;
        return graphic;
    }
    public static SpriteRenderer SetAlpha(this SpriteRenderer spriteRenderer, float alpha) {
        var temp = spriteRenderer.color;
        temp.a = alpha;
        spriteRenderer.color = temp;
        return spriteRenderer;
    }
    public static RectTransform SetAnchorXPosition(this RectTransform rect, float posX) {
        var temp = rect.anchoredPosition;
        temp.x = posX;
        rect.anchoredPosition = temp;
        return rect;
    }
}
