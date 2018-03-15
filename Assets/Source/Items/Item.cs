using Lomztein.PlaceholderName.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.PlaceholderName.Items {

    public class Item : ScriptableObject {

        public ItemPrefab prefab;

        private Dictionary<string, Attribute> attributes = new Dictionary<string, Attribute> ();

        public virtual Texture2D GetIcon() {
            if (prefab.icon != null)
                return prefab.icon;
            else
                return Iconography.GenerateIcon (prefab.gameObject);
        }

        public virtual GameObject GetModel() {
            if (prefab.model != null)
                return prefab.model;
            else
                return Iconography.GenerateModel (prefab.gameObject);
        }

        public static implicit operator ItemPrefab(Item item) {
            return item.prefab;
        }

        public T GetAttribute<T>(string attributeName) {
            return (T)attributes [ attributeName ].entry;
        }

        public void SetAttribute(string attributeName, object obj) {
            if (!attributes.ContainsKey (attributeName))
                attributes.Add (attributeName, new Attribute (attributeName));
            attributes [ attributeName ].entry = obj;
        }

        public override string ToString() {
            return prefab.name;
        }

        public class Attribute {

            public string name;
            public object entry;

            public Attribute(string _name) {
                name = _name;
            }
        }

        public static bool Equals(Item slotOne, Item slotTwo) {
            return (slotOne && slotTwo && slotOne.prefab == slotTwo.prefab && Item.Equals (slotOne.attributes, slotTwo.attributes));
        }

        public static bool Equals(Dictionary<string, Attribute> attributesOne, Dictionary<string, Attribute> attributesTwo) {
            if (attributesOne.Count == 0 && attributesTwo.Count == 0)
                return true;

            if (attributesOne.Count != attributesTwo.Count)
                return false;

            for (int i = 0; i < attributesOne.Count; i++) {
                var thisPair = attributesOne.Values.ElementAt (i);
                var otherPair = attributesTwo.Values.ElementAt (i);
                if (thisPair.name != otherPair.name)
                    return false;

                if (!Equals (thisPair.entry, otherPair.entry))
                    return false;
            }

            return true;
        }
    }
}