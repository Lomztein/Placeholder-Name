using Lomztein.PlaceholderName.Items;
using Lomztein.PlaceholderName.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.PlaceholderName.UI {

    public class ItemSlotButton : MonoBehaviour {

        public ItemSlot itemSlot;

        public RawImage iconImage;
        public Text countText;

        public Button button;

        private void Start() {
            UpdateUI ();
            button.onClick.AddListener (() => { itemSlot.MoveItem (PlayerController.itemInHand); });
            itemSlot.OnItemChanged += ItemSlot_OnItemChanged;
        }

        private void ItemSlot_OnItemChanged(ItemSlot itemSlot, Item oldItem, Item newItem) {
            UpdateUI ();
        }

        public virtual void UpdateUI() {
            if (itemSlot.item != null) {
                iconImage.enabled = true;
                iconImage.texture = itemSlot.item.GetIcon ();
                countText.text = itemSlot.count == 1 ? "" : itemSlot.count.ToString ();
            } else {
                iconImage.enabled = itemSlot.emptyIcon != null;
                iconImage.texture = itemSlot.emptyIcon;
                countText.text = "";
            }
        }

        private void OnDestroy () {
            itemSlot.OnItemChanged -= ItemSlot_OnItemChanged;
        }

    }
}
