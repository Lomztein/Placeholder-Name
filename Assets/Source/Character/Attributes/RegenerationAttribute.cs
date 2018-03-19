using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.Characters.Attributes {

    [CreateAssetMenu (fileName = "New Regeneration Attribute", menuName = "Attributes/Regeneration")]
    public class RegenerationAttribute : Attribute {

        public override void Activate(Character toCharacter, object source, float multiplier) {
            toCharacter.regeneration.AddModifier (source, multiplier);
        }

        public override void Deactivate(Character toCharacter, object source) {
            toCharacter.regeneration.RemoveModifier (source);
        }
    }

}
