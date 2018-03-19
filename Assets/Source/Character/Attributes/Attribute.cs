using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.Characters.Attributes {

    // Possibly consider redoing the stat system to use an array of named stats instead of hardcoded stats.
    // While this would be slower, especially with a lot of stats, it might not be noticeable even with hundreds of stats.
    // Additionally, this would make attributes easier to maintain, since a single FloatAttribute type object could
    // Handle most FloatStat stats, instead of a hardcoded Attribute type per stat.
    public abstract class Attribute : ScriptableObject {

        protected const string DescValueIdentifier = "{VALUE}";

        public new string name;
        public string description;

        public virtual string GetDescription(object value, float multiplier) {
            return description.Replace (DescValueIdentifier, multiplier.ToString ());
        }

        public abstract void Activate(Character toCharacter, object source, float multiplier);

        public abstract void Deactivate(Character toCharacter, object source);

        [System.Serializable]
        public class Entry {

            public Attribute attribute;
            public float multiplier;

            public void Activate (Character toCharacter, object source) {
                attribute.Activate (toCharacter, source, multiplier);
            }

            public void Deactivate (Character fromCharacter, object source) {
                attribute.Deactivate (fromCharacter, source);
            }

        }
    }
}
