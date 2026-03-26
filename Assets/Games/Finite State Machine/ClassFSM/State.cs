

using System;
using UnityEngine;

namespace Class_FSM {
    // You should add Singleton for deriver State<T> class
    public abstract class State<T> where T : ObjectBase {
        public virtual Color SceneGizmoColor { get => Color.white; }

        protected abstract Transition<T>[] GetTransitions();
        protected abstract void DoStartActions(StateController<T> controller);
        protected abstract void DoUpdateActions(StateController<T> controller);
        protected abstract void DoEndActions(StateController<T> controller);
        public void StartState(StateController<T> controller) {
            DoStartActions(controller);
        }
        public void UpdateState(StateController<T> controller) {
            DoUpdateActions(controller);
            CheckTransitions(controller);
        }
        public void EndState(StateController<T> controller) {
            DoEndActions(controller);
        }
        protected void CheckTransitions(StateController<T> controller) {
            GetTransitions()?.CheckTransition(controller);
        }
    }
}
/*
public abstract class AbstractSingleton<T> where T : AbstractSingleton<T> {
    protected AbstractSingleton() { }
    public static bool Initialized { get; protected set; }
    protected static T instance;

    public static T Instance {
        get {
            if(instance != null) {
                return instance;
            }
            instance = Create();
            instance.Initialize();
            Initialized = true;
            return instance;
        }
    }
    protected virtual void Initialize() {
        if(Initialized)
            return;
    }

    public virtual void Preload() { }

    private static T Create() {
        Type t = typeof(T);
        var flags = System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.NonPublic;
        var constructor = t.GetConstructor(flags, null, Type.EmptyTypes,
        null);
        var instance = constructor.Invoke(null);
        return instance as T;
    }
}
*/
