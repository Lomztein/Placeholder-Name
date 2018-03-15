using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Lomztein.PlaceholderName.Characters.Extensions {

    public static class StatExtensions {

        public static float GetAdditiveValue (this Stat<float> stat) {
            return stat.GetAllValues ().Sum ();
        }

        // I think multiplicative stats would cause the stats to increase too exponentially. I want this game to be more liniar in progression, since it's more natural.
        /*public static float GetMultiplicativeValue (this Stat<float> stat) {
            float result = stat.GetBaseValue ();
            stat.GetModifiers ().ForEach (x => result *= x.value);
            return result;
        }*/

        public static bool TakeDamage (this Stat<Health> stat, float damage) {
            // Get the last health pool that still has any health left in it.
            Health lastNonEmpty = stat.GetAllValues ().LastOrDefault (x => x.health > 0f);
            if (lastNonEmpty == null)
                return true; // There are no pools without damage, health is drained.

            // Calculate remainder after taking damage.
            float rest = lastNonEmpty.health - damage;
            lastNonEmpty.TakeDamage (damage);

            // If there was excess damage, recursively damage next pool.
            if (rest < 0f)
                if (stat.TakeDamage (rest))
                    return true; // This was last pool, health is drained.

            return false;
        }

    }

}
