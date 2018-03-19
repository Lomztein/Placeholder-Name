using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.Characters.Attributes {

    [CreateAssetMenu (fileName = "New Accuracy Attribute", menuName = "Attributes/Accuracy")]
    public class AccuracyAttribute : Attribute {

        public override void Activate(Character toCharacter, object source, float multiplier) {
            toCharacter.accuracyMul.AddModifier (source, multiplier);
        }

        public override void Deactivate(Character toCharacter, object source) {
            toCharacter.accuracyMul.RemoveModifier (source);
        }
    }

}