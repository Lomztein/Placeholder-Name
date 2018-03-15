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
        }

        public void UpdateUI() {
            if (itemSlot.item != null) {
                iconImage.enabled = true;
                iconImage.texture = itemSlot.item.GetIcon ();
                countText.text = itemSlot.count == 1 ? "" : itemSlot.count.ToString ();
            } else {
                iconImage.enabled = false;
                countText.text = "";
            }
        }

    }
}
