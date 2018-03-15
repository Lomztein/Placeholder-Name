using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Lomztein.PlaceholderName.Characters {

    [System.Serializable]
    public class Stat<T> {

        [SerializeField] private string name = "Stat";
        [SerializeField] private T baseValue; // TODO: Change this to be a property instead.

        private List<Modifier> modifiers = new List<Modifier> ();

        public Stat(string _name, T _baseValue = default(T)) {
            name = _name;
            baseValue = _baseValue;
        }

        public string GetName() {
            return name;
        }

        public T GetBaseValue() {
            return baseValue;
        }

        public List<T> GetAllValues () {
            List<T> all = new List<T> () { baseValue };
            all.AddRange (modifiers.Select (x => x.value));
            return all;
        }

        public List<Modifier> GetModifiers() {
            return modifiers;
        }

        public void AddModifier(object source, T value) {
            modifiers.Add (new Modifier (source, value));
        }

        public void RemoveModifier(object source) {
            Modifier modifier = modifiers.Find (x => x.source == source);
            modifiers.Remove (modifier);
        }

        public class Modifier {

            public object source;
            public T value;

            public Modifier(object _source, T _value) {
                source = _source; value = _value;
            }

        }
    }
}
