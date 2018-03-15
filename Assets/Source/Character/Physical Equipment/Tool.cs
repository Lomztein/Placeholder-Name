using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.Characters.PhysicalEquipment {

    public abstract class Tool : Equipment {

        public virtual void OnUseHeld() { }

        public virtual void OnUsePressed() { }

        public virtual void OnUseReleased() { }

    }

}