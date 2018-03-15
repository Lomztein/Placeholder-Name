using Lomztein.PlaceholderName.Weaponary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.Items {

    public interface IAmmo {

        Weapon.AmmoType AmmoType { get; set; }

    }

}
