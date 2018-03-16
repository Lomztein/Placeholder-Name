using Lomztein.PlaceholderName.Characters;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Lomztein.PlaceholderName.Items {

    public class Loot : MonoBehaviour {

        public Entry [ ] lootEntries;
        public float minScatterSpeed = 1f;
        public float maxScatterSpeed = 2f;

        private Character character;

        private void Start () {
            character = GetComponent<Character> ();
            character.OnKilled += Character_OnKilled;
        }

        private void Character_OnKilled(Character killer) {
            Scatter ();
        }

        public void Scatter () {

            foreach (Entry entry in lootEntries) {
                int amount = entry.RollDie ();
                if (amount > 0) {
                    PhysicalItem item = PhysicalItem.Create (entry.itemPrefab.CreateItem (), amount, transform.position + Vector3.up, Random.rotation);
                    item.rigidbody.velocity = new Vector3 (Random.Range (maxScatterSpeed, maxScatterSpeed),
                        Random.Range (minScatterSpeed, maxScatterSpeed),
                        Random.Range (maxScatterSpeed, maxScatterSpeed));
                }

            }

            Destroy (this);
            character.OnKilled -= Character_OnKilled;

        }

        [System.Serializable]
        public class Entry {

            public ItemPrefab itemPrefab;
            public float chance;

            public int minAmount;
            public int maxAmount;

            public int RollDie () {
                if (Random.Range (0f, 100f) < chance) {
                    return Random.Range (minAmount, maxAmount + 1);
                }
                return 0;
            } 

        }

    }

}
