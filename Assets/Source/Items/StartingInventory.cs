using Lomztein.PlaceholderName.Characters;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.PlaceholderName.Items {

    public class StartingInventory : MonoBehaviour, IContainer {

        public Piece [ ] pieces;

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

            foreach (Piece piece in pieces) {
                if (piece.item is IEquipable) {
                    ItemSlot slot = ItemSlot.CreateSlot (this);
                    slot.SetItem (piece.item.CreateItem (), piece.count);
                    toEquip.Add (slot);
                } else {
                    ItemSlots.Add (ItemSlot.CreateSlot (this));
                    ItemSlots.Last ().SetItem (piece.item.CreateItem (), piece.count);
                }
            }

            GetComponent<Inventory> ().PlaceItems (ItemSlots.ToArray ());
            toEquip.ForEach (x => GetComponent<Character> ().equipment.QuickEquip (x));
        }

        [System.Serializable]
        public class Piece {
            public ItemPrefab item;
            public int count;
        }

    }
}
