using Lomztein.PlaceholderName.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.PlaceholderName.UI {

    public class InventoryUI : MonoBehaviour {

        public GameObject buttonPrefab;
        public RectTransform buttonParent;
        public Text headerText;

        public Inventory inventory;
        public ItemSlotButton [ ] buttons;

        public void CreateUI (Inventory _inventory) {
            inventory = _inventory;
            headerText.text = inventory.name;

            buttons = new ItemSlotButton [ inventory.slotCount ];
            for (int i = 0; i < buttons.Length; i++) {
                ItemSlot slot = inventory.ItemSlots [ i ];

                ItemSlotButton button = Instantiate (buttonPrefab, buttonParent).GetComponent<ItemSlotButton> ();
                buttons [ i ] = button;

                button.itemSlot = slot;
            }

            UpdateUI ();

            inventory.OnItemsChanged += OnInventoryChanged;
        }

        private void OnInventoryChanged (Item oldItem, Item newItem) {
            UpdateUI ();
        }

        private void OnDisable() {
            inventory.OnItemsChanged -= OnInventoryChanged;
        }

        // Update is called once per frame
        void UpdateUI() {
            foreach (var butt in buttons) {
                butt.UpdateUI ();
            }
        }
    }

}
