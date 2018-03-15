using Lomztein.PlaceholderName.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.PlaceholderName.UI {

    public class InventoryUI : MonoBehaviour {

        public Text headerText;

        public Inventory inventory;
        public ContainerUI containerUI;

        public int columns;

        public void CreateUI(Inventory _inventory) {
            inventory = _inventory;
            headerText.text = inventory.name;

            containerUI.CreateUI (inventory, GridLayoutGroup.Constraint.FixedColumnCount, columns);
        }
    }

}
