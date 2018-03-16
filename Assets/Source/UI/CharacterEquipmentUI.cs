using Lomztein.PlaceholderName.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.UI {

    public class CharacterEquipmentUI : MonoBehaviour {

        public CharacterEquipment equipment;

        public RectTransform buttonParent;
        public GameObject slotButtonPrefab;
        public ItemSlotButton [ ] slotButtons;

        public void CreateUI(CharacterEquipment _equipment) {

            equipment = _equipment;

            slotButtons = new ItemSlotButton [ equipment.equipmentSlots.Count ];
            for (int i = 0; i < slotButtons.Length; i++) {
                CharacterEquipment.Slot slot = equipment.equipmentSlots [ i ];
                ItemSlotButton button = Instantiate (slotButtonPrefab, buttonParent).GetComponent<ItemSlotButton> ();
                button.itemSlot = slot;
            }

        }

        public static CharacterEquipmentUI DisplayEquipment (CharacterEquipment equipment, Vector3 position) {
            CharacterEquipmentUI ui = UIManager.CreateFromResource<CharacterEquipmentUI> ("UI/CharacterEquipmentPanel", position);
            ui.CreateUI (equipment);
            return ui;
        }
    }

}