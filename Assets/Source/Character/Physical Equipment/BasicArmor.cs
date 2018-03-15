using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.Characters.PhysicalEquipment {

    public class BasicArmor : Equipment {

        public float armorRating;

        public override void OnEquip(Character character, CharacterEquipment.Slot slot) {
            base.OnEquip (character, slot);
            parentCharacter.armor.AddModifier (this, armorRating);
        }

        public override void OnUnequip(Character character, CharacterEquipment.Slot slot) {
            base.OnUnequip (character, slot);
            parentCharacter.armor.RemoveModifier (this);
        }
    }

}
