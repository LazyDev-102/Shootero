using System;
using UnityEngine;

public class TestEventttttt : MonoBehaviour {

    [ContextMenu("Action")]
    public void Action() {
        Employer em = new Employer();
        em.NameChanged += EmployerNameChanged;
        em.Name = "Ahihi";
        em.Name = "Ohaha";
    }

    private void EmployerNameChanged(object sender, EmployerEventArgs e) {
        Debug.LogError($"New Name= {e.Name}");
    }

    public class Employer {

        private event EventHandler<EmployerEventArgs> nameChanged;
        public event EventHandler<EmployerEventArgs> NameChanged {
            add {
                nameChanged += value;
            }
            remove {
                nameChanged -= value;
            }
        }

        private string name;
        public string Name {
            get => name;
            set {
                name = value;
                OnNameChanged();
            }
        }


        private void OnNameChanged() {
            if (nameChanged != null) {
                nameChanged(this, new EmployerEventArgs(Name));
            }
        }

    }

    public class EmployerEventArgs : EventArgs {
        public string Name;
        public EmployerEventArgs(string name) {
            Name = name;
        }
    }
}
