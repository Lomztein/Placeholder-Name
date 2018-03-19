using Lomztein.PlaceholderName.Characters.Extensions;
using Lomztein.PlaceholderName.Characters.SerializableStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.Characters {

    [System.Serializable]
    public class Health {

        public float health;
        public FloatStat maxHealth;

        public Health (FloatStat _maxHealth) {
            maxHealth = _maxHealth;
            health = maxHealth.GetAdditiveValue ();
        }

        public bool TakeDamage(float damage) {
            health -= damage;
            if (health > maxHealth.GetAdditiveValue ())
                health = maxHealth.GetAdditiveValue ();

            return health < 0f;
        }


    }
}