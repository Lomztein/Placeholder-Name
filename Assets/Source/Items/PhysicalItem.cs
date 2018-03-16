using Lomztein.PlaceholderName.Extensions;
using Lomztein.PlaceholderName.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.Items {

    [RequireComponent (typeof (BoxCollider))]
    [RequireComponent (typeof (Rigidbody))]
    public class PhysicalItem : MonoBehaviour {

        public ItemSlot slot;
        public GameObject model;
        public new BoxCollider collider;
        public new Rigidbody rigidbody;

        private static PhysicalItem Create (Vector3 position, Quaternion rotation) {
            GameObject obj = Resources.Load<GameObject> ("Items/PhysicalItem");
            PhysicalItem physical = Instantiate (obj, position, rotation).GetComponent<PhysicalItem> ();
            physical.slot = ItemSlot.CreateSlot (null);
            physical.slot.OnItemChanged += physical.Slot_OnItemChanged;
            return physical;
        }

        private void Slot_OnItemChanged(ItemSlot itemSlot, Item oldItem, Item newItem) {
            if (newItem == null)
                Destroy (gameObject);
            else
                UpdateGraphics ();
        }

        public static PhysicalItem Create (ItemSlot from, Vector3 position, Quaternion rotation) {
            PhysicalItem physical = Create (position, rotation);
            from.MoveItem (physical.slot);
            physical.UpdateGraphics ();
            return physical;
        }

        public static PhysicalItem Create (Item item, int amount, Vector3 position, Quaternion rotation) {
            PhysicalItem physical = Create (position, rotation);
            physical.slot.SetItem (item, amount);
            physical.UpdateGraphics ();
            return physical;
        }

        public void UpdateGraphics () {
            model = Iconography.GenerateModel (slot.item.GetModelInstance (), transform.position, transform.rotation);
            Bounds bounds = Iconography.GetObjectBounds (model);

            collider.center = bounds.center;
            collider.size = bounds.size;

            model.transform.parent = transform;
            rigidbody.mass = bounds.CalcVolume ();
        }
    }

}
