using UnityEngine;

public abstract class View<TModel> : MonoBehaviour {
    public TModel Model { get; private set; }

    public abstract void Show();

    public View<TModel> SetModel(TModel model) {
        Model = model;
        return this;
    }
}

