using Lomztein.PlaceholderName.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.PlaceholderName.Items {

    public class Item : ScriptableObject {

        public ItemPrefab prefab;

        private Dictionary<string, Metadata> metadata = new Dictionary<string, Metadata> ();

        public virtual Texture2D GetIcon() {
            if (prefab.icon != null)
                return prefab.icon;
            else
                return Iconography.GenerateIcon (prefab.gameObject);
        }

        // It would make more sense for this to return a mesh object, instead of an instantiated GameObject, but that'd require *work*, and be generally less versitile.
        public virtual GameObject GetModelInstance() {
            if (prefab.model != null)
                return Instantiate (prefab.model);
            else
                return Iconography.InstantiateModel (prefab.gameObject, Vector3.zero, Quaternion.identity);
        }

        public static implicit operator ItemPrefab(Item item) {
            return item.prefab;
        }

        public T GetAttribute<T>(string attributeName) {
            return (T)metadata [ attributeName ].entry;
        }

        public void SetAttribute(string attributeName, object obj) {
            if (!metadata.ContainsKey (attributeName))
                metadata.Add (attributeName, new Metadata (attributeName));
            metadata [ attributeName ].entry = obj;
        }

        public override string ToString() {
            return prefab.name;
        }

        public class Metadata {

            public string name;
            public object entry;

            public Metadata(string _name) {
                name = _name;
            }
        }

        public static bool Equals(Item slotOne, Item slotTwo) {
            return (slotOne && slotTwo && slotOne.prefab == slotTwo.prefab && Item.Equals (slotOne.metadata, slotTwo.metadata));
        }

        public static bool Equals(Dictionary<string, Metadata> attributesOne, Dictionary<string, Metadata> attributesTwo) {
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