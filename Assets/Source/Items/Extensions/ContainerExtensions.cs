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
                while (loc.count != 0) {
                    ItemSlot s = container.FindAvailableSlot (loc.item);
                    if (s) {
                        loc.MoveItem (s);
                    } else
                        break;
                }
            }
        }

        public static ItemSlot FindAvailableSlot(this IContainer container, Item movingItem = null) {
            foreach (ItemSlot s in container.ItemSlots) {

                // If the slot already has an item, compare to moving item and return if stackable.
                if (s.item && movingItem && s.count != movingItem.prefab.maxStackSize) {
                    if (Equals (s.item, movingItem)) {
                        return s;
                    }
                } else {
                    if (!s.item)
                        return s;
                }
            }

            return null;
        }

        public static ItemSlot FindItemByPredicate(this IContainer container, Func<ItemSlot, bool> predicate) {
            return container.ItemSlots.FirstOrDefault (predicate);
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
