using UnityEngine;

[System.Serializable]
public class CellPosition {
    public int r;
    public int c;

    public CellPosition() {
        r = c = 0;
    }

    public CellPosition(int r, int c) {
        this.r = r;
        this.c = c;
    }
}
