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
        public Texture2D emptyIcon;
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

        /// <summary>
        /// Supposed to be overwritten by specialized slots that only take in certain items, for example.
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        public virtual bool AllowItem (ItemPrefab prefab) {
            if (lockedType != null)
                return lockedType.IsInstanceOfType (prefab);
            return true;
        }

        public int GetAvailableSpace() {
            if (item) {
                return GetMaxItems () - count;
            } else
                return GetMaxItems ();
        }

        public int GetMaxItems () {
            if (item)
                return Mathf.Min (maxItems, item.prefab.maxStackSize);
            else
                return maxItems;
        }

        public void ChangeCount (int addition) {
            ChangeCount (addition, true);
        }

        private void ChangeCount(int addition, bool fireEvent) {
            count += addition;

            if (count < 0)
                Debug.LogWarning ("Tried to remove a larger count that was present.");

            if (count == 0)
                RemoveItem ();

            if (fireEvent)
                CallOnChangedEvent (item, item);
        }

        public void RemoveItem() {
            item = null;
            count = 0;
        }

        public void SetItem (Item newItem, int amount) {
            RemoveItem ();

            Item oldItem = item;

            item = newItem;
            ChangeCount (amount, false);

            CallOnChangedEvent (oldItem, newItem);
        }

        private void CallOnChangedEvent (Item oldItem, Item newItem) {
            if (OnItemChanged != null)
                OnItemChanged (this, oldItem, newItem);

            parentContainer.CallOnItemChangedEvent (oldItem, newItem);
        }

        // This entire method could use a rewrite, partly because it's strange to use in practice, second to figure out how it works.
        public void MoveItem(ItemSlot newSlot, int transferCount = -1) {
            // Remember: Clicking an inventory button moves the buttons slot into the hand, ergo it calls this function with the hand slot as newSlot.

            Item oldItem = item;
            Item newItem = newSlot.item;

            int otherCount = newSlot.count;

            if (oldItem && !newSlot.AllowItem (oldItem.prefab))
                return;
            if (newItem && !AllowItem (newItem))
                return;

            if (Item.Equals (oldItem, newItem)) {

                // If they are equal, transferCount should limit to the amount of space remaining in the newSlot.
                if (transferCount == -1)
                    transferCount = Mathf.Min (count, newSlot.GetAvailableSpace ());

                // Both sides have items, are the same item and metadata.
                int total = otherCount + transferCount;
                int max = Mathf.Min (oldItem.prefab.maxStackSize, GetMaxItems ());

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

                // Other slot has enough room for this slots items, so it adds
                // this slots item and removes item from this slot.
            } else {
                // Both sides are not the same item, or any side doesn't have an item. Simply swap spaces.
                // If recieving side is empty, only the transferCount should be moved, if not, and transferCount
                // isn't the entire stack, do nothing.

                // If they are not equal, transferCount should be limited to the maximum amount of space in the newSlot.
                if (transferCount == -1)
                    transferCount = Mathf.Min (count, newSlot.GetMaxItems ());

                if (newSlot.item == null) {

                    newSlot.SetItem (item, transferCount);
                    ChangeCount (-transferCount);

                } else if (transferCount == count) {

                    newSlot.SetItem (oldItem, count);
                    SetItem (newItem, otherCount);

                }
            }
        }
    }
}