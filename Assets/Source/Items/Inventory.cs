using Lomztein.PlaceholderName.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Lomztein.PlaceholderName.UI;

namespace Lomztein.PlaceholderName.Items {

    public class Inventory : MonoBehaviour, IContainer {

        public new string name = "Inventory";
        public List<ItemSlot> ItemSlots { get; set; }

        public IEnumerator GetEnumerator() {
            return ItemSlots.GetEnumerator ();
        }

        public int slotCount;

        public OnItemsChangedEvent OnItemsChanged { get; set; }

        public virtual void Initialize() {
            ItemSlots = new List<ItemSlot> ();
            for (int i = 0; i < slotCount; i++)
                ItemSlots.Add (ItemSlot.CreateSlot (this));
        }

        // Use this for initialization
        void Awake() {
            Initialize ();
        }

        public void Display () {
            Vector2 position = Camera.main.WorldToScreenPoint (transform.position);
            InventoryUI.DisplayInventory (this, position);
        }
    }
}
