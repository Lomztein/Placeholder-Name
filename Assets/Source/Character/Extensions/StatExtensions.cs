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

    }

}
