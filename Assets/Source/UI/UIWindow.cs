using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.UI {

    public abstract class UIWindow : MonoBehaviour {

        public virtual void Awake() {
            UIManager.AddWindow (this);
        }

        public virtual void Close() {
            UIManager.RemoveWindow (this);
            Destroy (gameObject);
        }

    }

}
