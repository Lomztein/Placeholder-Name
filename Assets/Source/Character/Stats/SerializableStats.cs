using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.Characters.SerializableStats {

    [System.Serializable]
    public class FloatStat : Stat<float> {

        public FloatStat(string _name, float _baseValue) : base (_name, _baseValue) { }

    }

    [System.Serializable]
    public class HealthStat : Stat<Health> {

        public HealthStat(string _name, Health _baseValue) : base (_name, _baseValue) { }

    };

}
