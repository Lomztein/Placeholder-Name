using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Lomztein.PlaceholderName.Items {

    public class ItemSlot : ScriptableObject {

        public delegate void OnItemSlotChangedEvent(ItemSlot itemSlot, Item oldItem, Item newItem);
        public event OnItemSlotChangedEvent OnItemChanged;

        public Item item;
        public int count;
        public Type lockedType;
        public int maxItems = int.MaxValue;
        public IContainer parentContainer;

        public static ItemSlot CreateSlot(IContainer parentContainer) {
            ItemSlot slot = CreateInstance<ItemSlot> ();
            slot.parentContainer = parentContainer;
            return slot;
        }

        public override string ToString() {
            if (item) {
                return item.prefab.name + ", " + count.ToString ();
            } else {
                return "Empty";
            }
        }

        public void ChangeCount(int addition) {
            count += addition;

            if (count < 0)
                Debug.LogWarning ("Tried to remove a larger count that was present.");

            if (count == 0)
                RemoveItem ();
        }

        public void RemoveItem() {
            item = null;
            count = 0;
        }

        public void SetItem (Item newItem, int amount) {
            Item oldItem = item;
            item = newItem;
            ChangeCount (amount);

            CallOnChangedEvent (oldItem, newItem);
        }

        private void CallOnChangedEvent (Item oldItem, Item newItem) {
            if (OnItemChanged != null)
                OnItemChanged (this, oldItem, newItem);

            parentContainer.CallOnItemChangedEvent (oldItem, newItem);
        }

        public void MoveItem(ItemSlot newSlot, int transferCount = -1, bool oppisiteStack = false) {
            // Remember: Clicking an inventory button moves the buttons slot into the hand, ergo it calls this function with the hand slot as newSlot.

            Item oldItem = item;
            Item newItem = newSlot.item;

            int otherCount = newSlot.count;

            if (transferCount == -1)
                transferCount = Mathf.Min (count, newSlot.maxItems);

            if (oldItem && newSlot.lockedType != null && !newSlot.lockedType.IsInstanceOfType (oldItem.prefab)) {
                return;
            }

            if (Item.Equals (oldItem, newItem)) {

                // Both sides have items, are the same item and metadata.
                int total = otherCount + transferCount;
                int max = Mathf.Min (oldItem.prefab.maxStackSize, maxItems);

                if (total <= max) {
                    newSlot.ChangeCount (transferCount);
                    ChangeCount (-transferCount);
                } else {
                    // Other slot does not have enough room for this slots item,
                    // so it adds what it can and subtracts those from this slot.
                    int remaining = max - otherCount;

                    newSlot.ChangeCount (remaining);
                    ChangeCount (-remaining);
                }

                if (oppisiteStack) {
                    newSlot.MoveItem (this);
                }
                // Other slot has enough room for this slots items, so it adds
                // this slots item and removes item from this slot.
            } else {
                // Both sides are not the same item, or any side doesn't have an item. Simply swap spaces.
                // If recieving side is empty, only the transferCount should be moved, if not, and transferCount
                // isn't the entire stack, do nothing.

                if (newSlot.item == null) {
                    newSlot.count = transferCount;
                    newSlot.item = item;

                    ChangeCount (-transferCount);
                } else if (transferCount == count) {
                    newSlot.count = count;
                    newSlot.item = item;

                    item = newItem;
                    count = otherCount;
                }
            }

            CallOnChangedEvent (oldItem, newItem);
            newSlot.CallOnChangedEvent (newItem, oldItem);
        }
    }
}