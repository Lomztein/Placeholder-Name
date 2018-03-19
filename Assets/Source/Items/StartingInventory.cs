using Lomztein.PlaceholderName.Characters;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.PlaceholderName.Items {

    public class StartingInventory : MonoBehaviour, IContainer {

        public Piece [ ] startingInventory;
        public Piece [ ] startingEquipment;

        public List<ItemSlot> ItemSlots {
            get;
            set;
        }

        public OnItemsChangedEvent OnItemsChanged { get; set; }

        public IEnumerator GetEnumerator() {
            return ItemSlots.GetEnumerator ();
        }

        // Use this for initialization
        void Start () {
            ItemSlots = new List<ItemSlot> ();
            List<ItemSlot> toEquip = new List<ItemSlot> ();

            foreach (Piece piece in startingInventory) {
                    ItemSlots.Add (ItemSlot.CreateSlot (this));
                    ItemSlots.Last ().SetItem (piece.item.CreateItem (), piece.count);
            }

            foreach (Piece piece in startingEquipment) {
                ItemSlot slot = ItemSlot.CreateSlot (this);
                slot.SetItem (piece.item.CreateItem (), piece.count);
                toEquip.Add (slot);
            }

            if (startingInventory.Length > 0)
                GetComponent<Inventory> ().PlaceItems (ItemSlots.ToArray ());

            if (startingEquipment.Length > 0)
                toEquip.ForEach (x => GetComponent<Character> ().equipment.QuickEquip (x));
        }

        [System.Serializable]
        public class Piece {
            public ItemPrefab item;
            public int count;
        }

    }
}
