using UnityEngine;

public abstract class BaseDrop : ScriptableObject {
    public abstract void Droping(Vector2 position, EnemyBase enemy);
    public abstract void Droping(Vector2 position, EnemyType eType);
    public abstract void Droping(Vector2 position, EnemyType eType, int numberIcon);
    public abstract void PreloadOpenApp();
}
