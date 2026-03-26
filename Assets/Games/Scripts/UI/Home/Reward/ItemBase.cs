using UnityEngine;

public class ItemBase<T> : MonoBehaviour {
    protected ContainerBase<T> view;
    protected int index;

    public virtual void UpdateUI(ContainerBase<T> view, T data)
    {
        this.view = view;
        this.index = view.Items.Count;
    }
}
