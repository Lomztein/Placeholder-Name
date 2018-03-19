using Lomztein.PlaceholderName.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lomztein.PlaceholderName.Characters.Attributes;

namespace Lomztein.PlaceholderName.Characters.PhysicalEquipment {

    public class Equipment : MonoBehaviour, IEquipment, IHasAttributes {

        public CharacterEquipment.Slot parentSlot;
        public Character parentCharacter;
        public Attribute.Entry [ ] equipmentAttributes;

        public Attribute.Entry [ ] GetAttributes() {
            return equipmentAttributes;
        }

        public virtual void OnEquip(Character character, CharacterEquipment.Slot itemSlot) {
            parentCharacter = character;
            parentSlot = itemSlot;

            foreach (Attribute.Entry attribute in equipmentAttributes)
                attribute.Activate (parentCharacter, this);
        }

        public virtual void OnUnequip(Character character, CharacterEquipment.Slot itemSlot) {
            foreach (Attribute.Entry attribute in equipmentAttributes)
                attribute.Deactivate (parentCharacter, this);
        }

    }
}
