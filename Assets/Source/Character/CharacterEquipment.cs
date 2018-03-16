using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Lomztein.PlaceholderName.Items;
using System.Linq;
using Lomztein.PlaceholderName.Characters.PhysicalEquipment;

namespace Lomztein.PlaceholderName.Characters {

    public class CharacterEquipment : ScriptableObject {

        public Character parentCharacter;

        public delegate void OnEquipmentChangedEvent (Slot itemSlot, Type equipmentType, Item oldItem, Item newItem);
        public event OnEquipmentChangedEvent OnEquipmentChanged;

        public List<Slot> equipmentSlots = new List<Slot> ();

        public enum Type {
            HumanoidHead, HumanoidTorso, HumanoidLegs, HumanoidFeet, Tool
        }

        public void QuickEquip (ItemSlot sourceSlot) {
            Type type = (sourceSlot.item.prefab as IEquipable).Type;
            Slot slot = GetSlot (type);
            if (slot != null)
                sourceSlot.MoveItem (slot);
        }

        public Slot GetSlot (Type equipmentType) {
            return equipmentSlots.FirstOrDefault (x => x.definition.equipmentType == equipmentType);
        }

        public void Initialize (Character _parentCharacter, params SlotDefinition[] definitions) {
            parentCharacter = _parentCharacter;

            foreach (var def in definitions) {

                Slot slot = Slot.CreateSlot (def);
                equipmentSlots.Add (slot);

                slot.OnItemChanged += (ItemSlot itemSlot, Item oldItem, Item newItem) => {
                    OnSlotChanged (slot, def.equipmentType, oldItem, newItem);
                };
            }
        }

        private void OnSlotChanged (Slot slot, Type type, Item oldItem, Item newItem) {
            if (OnEquipmentChanged != null)
                OnEquipmentChanged (slot, type, oldItem, newItem);

            if (slot.currentObject) {
                slot.currentObject.GetComponent<Equipment> ().OnUnequip (parentCharacter, slot);
                Destroy (slot.currentObject);
            }

            if (newItem != null) {
                slot.currentObject = Instantiate (newItem.prefab.gameObject, slot.definition.parentTransform, false);
                slot.currentObject.GetComponent<Equipment> ().OnEquip (parentCharacter, slot);
            }
        }

        public class Slot : ItemSlot {

            public SlotDefinition definition;
            public GameObject currentObject;

            public static Slot CreateSlot (SlotDefinition _definition) {
                Slot slot = CreateInstance<Slot> ();
                slot.definition = _definition;
                slot.emptyIcon = _definition.emptySlotIcon;

                return slot;
            }

            public override bool AllowItem(ItemPrefab prefab) {
                IEquipable equipable = prefab as IEquipable;
                return (equipable != null && equipable.Type == definition.equipmentType);
            }
        }

        [System.Serializable]
        public class SlotDefinition {

            public Type equipmentType;
            public Transform parentTransform;
            public Texture2D emptySlotIcon;

        }
    }

}
