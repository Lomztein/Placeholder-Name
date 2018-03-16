using Lomztein.PlaceholderName.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.PlaceholderName.UI {

    public class InventoryUI : UIWindow {

        public Text headerText;

        public Inventory inventory;
        public ContainerUI containerUI;

        public void CreateUI(Inventory _inventory, GridLayoutGroup.Constraint constraint = GridLayoutGroup.Constraint.FixedColumnCount, int constraintCount = 6) {
            inventory = _inventory;
            headerText.text = inventory.name;

            containerUI.CreateUI (inventory, constraint, constraintCount);
        }

        public static InventoryUI DisplayInventory(Inventory inventory, Vector2 screenPosition) {
            InventoryUI ui = UIManager.CreateFromResource<InventoryUI> ("UI/InventoryPanel", screenPosition);
            ui.CreateUI (inventory);
            return ui;
        }
    }

}
