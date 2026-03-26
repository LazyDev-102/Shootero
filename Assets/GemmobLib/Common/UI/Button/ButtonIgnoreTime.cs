using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonIgnoreTime : ButtonBase {
    protected override IEnumerator StartClick() {
        float tCounter = 0;

        while (tCounter < ZoomOutTime) {
            tCounter += Time.fixedDeltaTime;
            transform.localScale = Vector3.Lerp(originScale, originScale * clickScale, tCounter / ZoomOutTime);
            yield return null;
        }
    }
}
