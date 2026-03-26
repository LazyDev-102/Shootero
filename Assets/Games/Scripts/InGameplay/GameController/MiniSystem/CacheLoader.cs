using UnityEngine;

public class CacheLoader<T> where T : ScriptableObject {
    private T cached;
    private readonly string path;
    public CacheLoader() {
        path = typeof(T).Name;
    }
    public CacheLoader(string path) {
        this.path = path;
    }
    public T GetRef() {
        if (cached == null) {
            cached = Resources.Load<T>(path);
        }
        return cached;
    }
}

public class BaseLoader<T, U> where T : Object {
    private T cached;
    public BaseLoader() {
    }
    public T GetRef(U source) {
        if (cached == null) {
            cached = source as T;
        }
        return cached;
    }
}