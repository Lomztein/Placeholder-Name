using Lomztein.PlaceholderName.Characters.Extensions;
using Lomztein.PlaceholderName.Characters.SerializableStats;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.PlaceholderName.Characters {

    /// <summary>
    /// Character represents any entity with basic "living" functionality, such as health, armor, and a controller.
    /// </summary>
    public class Character : MonoBehaviour, IDamageable, IKillable {

        [Header ("Stats")]
        public HealthStat health = new HealthStat ("Health", new Health (new FloatStat ("Base Health", 100f)));
        public FloatStat armor = new FloatStat ("Armor", 0f);

        // TODO: Implement helper function to sync these with a potential collider.
        public Vector3 center = Vector3.up;
        public Vector3 size = new Vector3 (1, 2, 1);

        public Equipment.SlotDefinition [ ] equipmentSlotDefinitions;
        public Equipment equipment;

        public float GetHealth() {
            return health.GetModifiers ().Sum (x => x.value.health);
        }

        public float GetMaxHealth() {
            return health.GetModifiers ().Sum (x => x.value.maxHealth.GetAdditiveValue ());
        }

        public void TakeDamage(Damage damage) {
            float postArmor = damage.CalculateDamagePostArmor (armor.GetAdditiveValue ());
            if (health.TakeDamage (postArmor)) {
                Kill ();
            }
        }

        void Awake() {
            equipment = ScriptableObject.CreateInstance<Equipment> ();
            equipment.Initialize (equipmentSlotDefinitions);
        }

        public void Kill () {
            Destroy (gameObject);
        }

        private void OnDrawGizmosSelected() {
            Gizmos.DrawWireCube (transform.position + center, size); 
        }

    }

}
