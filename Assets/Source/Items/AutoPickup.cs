using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lomztein.PlaceholderName.Extensions;

namespace Lomztein.PlaceholderName.Items {

    public class AutoPickup : MonoBehaviour {

        private IContainer disposeIn;
        public Type [ ] filter = new Type[] { typeof (IAmmo) };

        private void Start() {
            disposeIn = this.GetComponentFromRoot<IContainer> ();
        }

        private void OnTriggerEnter(Collider other) {
            PhysicalItem item = other.GetComponent<PhysicalItem> ();
            if (item) {
                disposeIn.PlaceItems (item.slot);
            }
        }

    }

}
