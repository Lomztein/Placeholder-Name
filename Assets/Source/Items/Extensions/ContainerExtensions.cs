using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.PlaceholderName.Items {

    public static class ContainerExtensions {

        // This will either work or go into infinite loopness.
        public static void PlaceItems(this IContainer container, params ItemSlot [ ] items) {
            for (int i = 0; i < items.Length; i++) {
                ItemSlot loc = items [ i ];

                int maxTries = container.ItemSlots.Count;
                while (loc.count != 0 && maxTries > 1) {

                    maxTries--; // This should force it to quit after trying at least to move one into every single slot.
                    ItemSlot s = container.FindAvailableSlot (loc.item);
                    if (s) {
                        loc.MoveItem (s);
                    } else
                        break;
                }
            }
        }

        public static ItemSlot FindAvailableSlot(this IContainer container, Item movingItem = null) {
            foreach (ItemSlot slot in container.ItemSlots) {

                // If the slot already has an item, compare to moving item and return if stackable.
                if (slot.item && movingItem && slot.GetAvailableSpace () != 0) {
                    if (Item.Equals (slot.item, movingItem)) {
                        return slot;
                    }
                } else {
                    if (!slot.item)
                        return slot;
                }
            }

            return null;
        }

        public static ItemSlot FindItemByPredicate(this IContainer container, Func<ItemSlot, bool> predicate) {
            foreach (ItemSlot slot in container.ItemSlots) {
                if (slot.item != null && predicate (slot)) {
                    return slot;
                }
            }
            return null;
        }

        public static void CallOnItemChangedEvent(this IContainer container, Item oldItem, Item newItem) {
            if (container == null)
                return;

            OnItemsChangedEvent handler = container.OnItemsChanged;
            if (handler != null)
                handler (oldItem, newItem);
        }
    }
}
