using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.Characters.Attributes {

    [CreateAssetMenu (fileName = "New Armor Attribute", menuName = "Attributes/Armor")]
    public class ArmorAttribute : Attribute {

        public override void Activate(Character toCharacter, object source, float multiplier) {
            toCharacter.armor.AddModifier (source, multiplier);
        }

        public override void Deactivate(Character toCharacter, object source) {
            toCharacter.armor.RemoveModifier (source);
        }
    }

}
