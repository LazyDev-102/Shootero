using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B14Test : MonoBehaviour
{
    [SerializeField] B14MiniShieldMove[] e;
    int i = 0;
    private void Update() {
        if (Input.GetKeyDown(KeyCode.G)) {
            i++;
            e[i].MoveAttack(GameManager.Instance.GameLoader.Ship.transform, 3);
        }
    }
}
