using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.Characters.Attributes {

    [CreateAssetMenu (fileName = "New Speed Attribute", menuName = "Attributes/Speed")]
    public class SpeedAttribute : Attribute {

    public override void Activate(Character toCharacter, object source, float multiplier) {
        toCharacter.speed.AddModifier (source, multiplier);
    }

    public override void Deactivate(Character toCharacter, object source) {
        toCharacter.speed.RemoveModifier (source);
    }
}

}
