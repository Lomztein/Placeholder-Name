using Lomztein.PlaceholderName.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lomztein.PlaceholderName.Characters.Attributes;

namespace Lomztein.PlaceholderName.Characters.PhysicalEquipment {

    public class Equipment : MonoBehaviour, IEquipment {

        public CharacterEquipment.Slot parentSlot;
        public Character parentCharacter;
        public Attribute [ ] equipmentAttributes;

        public virtual void OnEquip(Character character, CharacterEquipment.Slot itemSlot) {
            parentCharacter = character;
            parentSlot = itemSlot;

            foreach (Attribute attribute in equipmentAttributes)
                attribute.Enable (parentCharacter, this);
        }

        public virtual void OnUnequip(Character character, CharacterEquipment.Slot itemSlot) {
            foreach (Attribute attribute in equipmentAttributes)
                attribute.Disable (parentCharacter, this);
        }

    }
}
