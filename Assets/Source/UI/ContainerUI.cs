using Lomztein.PlaceholderName.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.PlaceholderName.UI {

    public class ContainerUI : MonoBehaviour {

        public GameObject buttonPrefab;
        public RectTransform buttonParent;

        public IContainer container;
        public ItemSlotButton [ ] buttons;

        public GridLayoutGroup layout;

        public void CreateUI(IContainer _container, GridLayoutGroup.Constraint constraint = GridLayoutGroup.Constraint.Flexible, int constraintCount = 0) {
            container = _container;

            layout.constraint = constraint;
            layout.constraintCount = constraintCount;

            buttons = new ItemSlotButton [ container.ItemSlots.Count ];
            for (int i = 0; i < buttons.Length; i++) {
                ItemSlot slot = container.ItemSlots [ i ];

                ItemSlotButton button = Instantiate (buttonPrefab, buttonParent).GetComponent<ItemSlotButton> ();
                buttons [ i ] = button;

                button.itemSlot = slot;
            }

            UpdateUI ();
        }

        void UpdateUI() {
            foreach (var butt in buttons) {
                butt.UpdateUI ();
            }
        }
    }
}