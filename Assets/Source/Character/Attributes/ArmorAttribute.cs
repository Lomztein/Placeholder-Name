using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.Characters.Attributes {

    [CreateAssetMenu (fileName = "New Armor Attribute", menuName = "Attributes/Armor")]
    public class ArmorAttribute : Attribute {

        public float armorRating;

        public override void Enable(Character toCharacter, object source) {
            toCharacter.armor.AddModifier (source, armorRating);
        }

        public override void Disable(Character toCharacter, object source) {
            toCharacter.armor.RemoveModifier (source);
        }
    }

}
