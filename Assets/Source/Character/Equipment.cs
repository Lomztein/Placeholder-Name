using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Lomztein.PlaceholderName.Items;
using System.Linq;

namespace Lomztein.PlaceholderName.Characters {

    public class Equipment : ScriptableObject {

        public delegate void OnEquipmentChangedEvent (Slot itemSlot, Type equipmentType, Item oldItem, Item newItem);
        public event OnEquipmentChangedEvent OnEquipmentChanged;

        public List<Slot> equipmentSlots = new List<Slot> ();

        public enum Type {
            HumanoidHead, HumanoidTorso, HumanoidLegs, HumanoidFeet, Tool
        }

        public void Equip (ItemSlot sourceSlot) {
            Type type = (sourceSlot.item.prefab as IEquipable).Type;
            Slot slot = GetSlot (type);
            if (slot != null)
                sourceSlot.MoveItem (slot);
        }

        public Slot GetSlot (Type equipmentType) {
            return equipmentSlots.FirstOrDefault (x => x.equipmentType == equipmentType);
        }

        public void Initialize (params SlotDefinition[] definitions) {
            foreach (var def in definitions) {

                Slot slot = Slot.CreateSlot (def.equipmentType, def.parentTransform);
                equipmentSlots.Add (slot);

                slot.OnItemChanged += (ItemSlot itemSlot, Item oldItem, Item newItem) => {
                    OnSlotChanged (slot, def.equipmentType, oldItem, newItem);
                };
            }
        }

        private void OnSlotChanged (Slot slot, Type type, Item oldItem, Item newItem) {
            if (OnEquipmentChanged != null)
                OnEquipmentChanged (slot, type, oldItem, newItem);

            if (slot.currentObject)
                Destroy (slot.currentObject);

            if (newItem != null) {
                slot.currentObject = Instantiate (newItem.prefab.gameObject, slot.parentTransform, false);
            }
        }

        public class Slot : ItemSlot {

            public Type equipmentType;
            public Transform parentTransform;
            public GameObject currentObject;

            public static Slot CreateSlot (Type type, Transform parent) {
                Slot slot = CreateInstance<Slot> ();
                slot.equipmentType = type;
                slot.parentTransform = parent;
                return slot;
            }

        }

        [System.Serializable]
        public class SlotDefinition {

            public Type equipmentType;
            public Transform parentTransform;

        }
    }

}
