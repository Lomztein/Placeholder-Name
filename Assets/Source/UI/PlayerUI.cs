using Lomztein.PlaceholderName.Items;
using Lomztein.PlaceholderName.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.UI {

    public class PlayerUI : MonoBehaviour {

        public PlayerController player;

        public InventoryUI playerInventoryUI;
        public CharacterEquipmentUI playerEquipmentUI;
        public HealthBar playerHealthBar;

        // Use this for initialization
        void Start() {
            playerInventoryUI.CreateUI (player.character.inventory, UnityEngine.UI.GridLayoutGroup.Constraint.FixedRowCount, 2);
            playerEquipmentUI.CreateUI (player.character.equipment);
            playerHealthBar.parent = player.character;
        }
    }

}
