using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.Items {

    [CreateAssetMenu (fileName = "New Item", menuName = "Items/Base Item Prefab")]
    public class ItemPrefab : ScriptableObject {

        public const int defaultStackSize = 512;

        new public string name = "Unnamed Item";
        [TextArea (3, 10)]
        public string description = "This item lacks a description.";
        public int maxStackSize = defaultStackSize;

        public GameObject gameObject;
        public Texture2D icon;
        public GameObject model;

        public virtual Item CreateItem() {
            Item item = CreateInstance<Item> ();
            item.prefab = this;
            return item;
        }

    }

}