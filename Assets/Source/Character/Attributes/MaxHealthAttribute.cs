using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.Characters.Attributes {

    [CreateAssetMenu (fileName = "New Max Health Attribute", menuName = "Attributes/Max Health")]
    public class MaxHealthAttribute : Attribute {

        public override void Activate(Character toCharacter, object source, float multiplier) {
            toCharacter.health.maxHealth.AddModifier (source, multiplier);
        }

        public override void Deactivate(Character toCharacter, object source) {
            toCharacter.health.maxHealth.RemoveModifier (source);
        }
    }

}