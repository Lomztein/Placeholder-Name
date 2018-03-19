using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.Characters.Attributes {

    [CreateAssetMenu (fileName = "New Firerate Attribute", menuName = "Attributes/Firerate")]
    public class FirerateAttribute : Attribute {

        public override void Activate(Character toCharacter, object source, float multiplier) {
            toCharacter.firerateMul.AddModifier (source, multiplier);
        }

        public override void Deactivate(Character toCharacter, object source) {
            toCharacter.firerateMul.RemoveModifier (source);
        }
    }

}