using Lomztein.PlaceholderName.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.Characters.PhysicalEquipment {

    public class Equipment : MonoBehaviour, IEquipment {

        public CharacterEquipment.Slot parentSlot;
        public Character parentCharacter;

        public virtual void OnEquip(Character character, CharacterEquipment.Slot itemSlot) {
            parentCharacter = character;
            parentSlot = itemSlot;
        }

        public virtual void OnUnequip(Character character, CharacterEquipment.Slot itemSlot) {
            parentCharacter = null;
            parentSlot = null;
        }

    }
}
