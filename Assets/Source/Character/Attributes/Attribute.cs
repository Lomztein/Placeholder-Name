using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.Characters.Attributes {

    public abstract class Attribute : ScriptableObject {

        public abstract void Enable(Character toCharacter, object source);

        public abstract void Disable(Character toCharacter, object source);

    }

}
