using Lomztein.PlaceholderName.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.Weaponary {

    public interface IProjectile {

        Character ParentCharacter { get; set; }

        IProjectile[] Create(Weapon fromWeapon, Transform muzzle);

        float Damage { get; }

        float GetArmorPenetration();

    }

}
