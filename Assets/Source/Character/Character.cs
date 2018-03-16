using Lomztein.PlaceholderName.Characters.Extensions;
using Lomztein.PlaceholderName.Characters.PhysicalEquipment;
using Lomztein.PlaceholderName.Characters.SerializableStats;
using Lomztein.PlaceholderName.Interfaces;
using Lomztein.PlaceholderName.Items;
using Lomztein.PlaceholderName.Weaponary;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.PlaceholderName.Characters {

    /// <summary>
    /// Character represents any entity with basic "living" functionality, such as health, armor, and a controller.
    /// </summary>
    public class Character : MonoBehaviour, IDamageable, IKillable, IHasHealth {

        [Header ("Stats")]
        public HealthStat health = new HealthStat ("Health", new Health (new FloatStat ("Base Health", 100f)));
        public FloatStat armor = new FloatStat ("Armor", 0f);
        public FloatStat speed = new FloatStat ("Speed", 5f);
        public FloatStat damageMul = new FloatStat ("Damage", 1f);
        public FloatStat accuracyMul = new FloatStat ("Accuracy", 1f);
        public FloatStat firerateMul = new FloatStat ("Firerate", 1f);

        // TODO: Implement helper function to sync these with a potential collider.
        public Vector3 center = Vector3.up;
        public Vector3 size = new Vector3 (1, 2, 1);

        public CharacterEquipment.SlotDefinition [ ] equipmentSlotDefinitions;
        public CharacterEquipment equipment;

        public LayerMask targetLayer;
        public Inventory inventory;

        public delegate void CharacterUseToolEvent(Tool tool);

        /// <summary>
        /// Fired each frame that a tool is "held down", like an automatic weapon.
        /// </summary>
        public event CharacterUseToolEvent OnToolHeld;

        /// <summary>
        /// Fired when a tool starts to be held.
        /// </summary>
        public event CharacterUseToolEvent OnToolPressed;

        /// <summary>
        /// Fired when a tool being held down is released.
        /// </summary>
        public event CharacterUseToolEvent OnToolReleased;

        public delegate void OnKilledEvent(Character killer);
        public event OnKilledEvent OnKilled;

        public float GetHealth() {
            return health.GetAllValues ().Sum (x => x.health);
        }

        public float GetMaxHealth() {
            return health.GetAllValues ().Sum (x => x.maxHealth.GetAdditiveValue ());
        }

        public void TakeDamage(Damage damage) {
            float postArmor = damage.CalculateDamagePostArmor (armor.GetAdditiveValue ());
            if (health.TakeDamage (postArmor)) {
                Kill ();
            }
        }

        public void PressTool (Tool tool) {
            CallToolEvent (OnToolPressed, tool);
            tool.OnUsePressed ();
        }

        public void ReleaseTool (Tool tool) {
            CallToolEvent (OnToolReleased, tool);
            tool.OnUseReleased ();
        }

        public void HoldTool (Tool tool) {
            CallToolEvent (OnToolHeld, tool);
            tool.OnUseHeld ();
        }

        void Awake() {
            equipment = ScriptableObject.CreateInstance<CharacterEquipment> ();
            equipment.Initialize (this, equipmentSlotDefinitions);
        }

        public void Kill () {
            if (OnKilled != null)
                OnKilled (null);

            Destroy (gameObject);
        }

        private void OnDrawGizmosSelected() {
            Gizmos.DrawWireCube (transform.position + center, size); 
        }

        private void CallToolEvent (CharacterUseToolEvent evt, Tool tool) {
            if (evt != null)
                evt (tool);
        }
    }

}
