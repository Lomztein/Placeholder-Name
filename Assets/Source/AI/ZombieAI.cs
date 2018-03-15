using Lomztein.PlaceholderName.Characters;
using Lomztein.PlaceholderName.Weaponary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.AI {

    public class ZombieAI : MonoBehaviour {

        private Humanoid character;
        private Transform target;

        public float range;
        public float damage;

        // Use this for initialization
        void Start() {
            character = GetComponent<Humanoid> ();
            target = GameObject.FindGameObjectWithTag ("Player").transform;
        }

        // Update is called once per frame
        void Update() {
            Vector3 direction = (target.position - transform.position).normalized;
            character.Move (direction, Time.deltaTime);

            if (Vector3.Distance (transform.position, target.position) < range ) {
                target.GetComponent<IDamageable> ().TakeDamage (new Damage (damage * Time.deltaTime, 0f));
            }
        }
    }

}
