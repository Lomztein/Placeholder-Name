using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.Weaponary {

    public interface IDamageable {

        float GetHealth();

        float GetMaxHealth();

        void TakeDamage(Damage damage);

    }

}
