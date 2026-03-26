using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBorder : MonoBehaviour {
    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(ConfigIngameData.borderW, ConfigIngameData.borderH, 1));
    }
}
