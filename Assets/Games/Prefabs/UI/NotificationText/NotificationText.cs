using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NotificationText : MonoBehaviour {
    public static NotificationText Instance { get; private set; }
    [SerializeField] private Text text;
    [SerializeField] private Color errorColor, infoColor;

    private Outline textOutline;
    float textPosYOrigin;

    public enum NoticeType { Error, Info };

    protected void Awake() {
        Instance = this;
        DontDestroyOnLoad(this);
        textPosYOrigin = (text.transform as RectTransform).anchoredPosition.y;
        textOutline = text.GetComponent<Outline>();
    }

    public NotificationText Show(string title, NoticeType type = NoticeType.Info) {
        if (text.gameObject.activeSelf)
            Hide();

        text.text = title;
        text.gameObject.SetActive(true);
        if (textOutline) {
            textOutline.enabled = true;
            textOutline.effectColor = type == NoticeType.Error ? errorColor : infoColor;
        }
        else
            text.color = type == NoticeType.Error ? Color.white : Color.green;

        StartCoroutine(IEMoveUp(0.8f, 0.2f));
        StartCoroutine(IEFadeOut(0.2f, 0.8f));
        return this;
    }
    public NotificationText Show(string title, NoticeType type, bool log) {
        if (text.gameObject.activeSelf)
            Hide();
        if (log)
            Gemmob.Logs.Log(title);
        text.text = title;
        text.gameObject.SetActive(true);
        if (textOutline) {
            textOutline.enabled = true;
            textOutline.effectColor = type == NoticeType.Error ? errorColor : infoColor;
        }
        else
            text.color = type == NoticeType.Error ? Color.white : Color.green;

        StartCoroutine(IEMoveUp(0.8f, 0.2f));
        StartCoroutine(IEFadeOut(0.2f, 0.8f));
        return this;
    }
    public void SetColor(Color color) {
        text.color = color;
        textOutline.enabled = false;
    }
    private void Hide() {
        if (text.gameObject.activeSelf) {
            StopAllCoroutines();
            text.gameObject.SetActive(false);
        }
    }

    private IEnumerator IEMoveUp(float duration, float delayTime) {
        float elapse = 0;
        Vector2 froPos = new Vector2(0, textPosYOrigin);
        Vector2 toPos = new Vector2(0, froPos.y + 50);
        RectTransform rect = text.transform as RectTransform;

        rect.anchoredPosition = froPos;
        //yield return new WaitForSeconds(delayTime);
        while (elapse < delayTime) {
            elapse += Time.unscaledDeltaTime;
            yield return null;
        }
        elapse = 0;

        while (elapse < duration) {
            elapse += Time.unscaledDeltaTime;
            rect.anchoredPosition = Vector3.Lerp(froPos, toPos, elapse / duration);
            yield return null;
        }
    }

    private IEnumerator IEFadeOut(float duration, float delayTime) {
        float elapse = 0;
        Color froColor = text.color;
        Color toColor = froColor;
        froColor.a = 1;
        toColor.a = 0;

        text.color = froColor;

        while (elapse < delayTime) {
            elapse += Time.unscaledDeltaTime;
            yield return null;
        }
        elapse = 0;

        while (elapse < duration) {
            elapse += Time.unscaledDeltaTime;
            text.color = Color.Lerp(froColor, toColor, elapse / duration);
            yield return null;
        }

        Hide();
    }

}
