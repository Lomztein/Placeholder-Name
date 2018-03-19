using System.Collections;
using System.Collections.Generic;
using Lomztein.PlaceholderName.Characters;
using Lomztein.PlaceholderName.Characters.Extensions;
using UnityEngine;

namespace Lomztein.PlaceholderName.Weaponary {

    public class Projectile : MonoBehaviour, IProjectile {

        public float weight;
        public float speed;
        public float armorPenetration;
        public float damageMul;

        public float Damage { get { return weight * speed; } }

        public Character ParentCharacter { get; set; }

        public int amount;
        public float inaccuracy;

        public LayerMask hittableLayer;

        public Vector3 velocity;

        public IProjectile [ ] Create(Weapon fromWeapon, Transform muzzle) {
            IProjectile [ ] results = new IProjectile [ amount ];

            for (int i = 0; i < amount; i++) {
                Projectile newProjectile = Instantiate (gameObject, muzzle.position, muzzle.rotation).GetComponent<Projectile>();
                newProjectile.ParentCharacter = fromWeapon.parentCharacter;

                float rad = inaccuracy * Mathf.Deg2Rad;
                Vector3 angled = muzzle.forward + muzzle.rotation * (Vector3.right * Mathf.Sin (Random.Range (-rad, rad)) + Vector3.up * Mathf.Sin (Random.Range (-rad, rad)));
                newProjectile.velocity = angled * speed * Random.Range (0.9f, 1.1f);

                newProjectile.hittableLayer |= fromWeapon.parentCharacter.targetLayer;
                damageMul = fromWeapon.parentCharacter.damageMul.GetAdditiveValue ();

                results [ i ] = newProjectile;
            }

            return results;
        }

        public float GetArmorPenetration() {
            return armorPenetration;
        }

        public virtual void FixedUpdate() {
            Ray nextRay = new Ray (transform.position, velocity * Time.fixedDeltaTime);
            RaycastHit hit;

            if (Physics.Raycast (nextRay, out hit, speed * Time.fixedDeltaTime, hittableLayer)) {
                Hit (hit);
            }

            transform.position += (velocity * Time.fixedDeltaTime);
        }

        public virtual void Hit(RaycastHit hit) {
            IDamageable damageable = hit.collider.GetComponentInParent<IDamageable> ();
            if (damageable != null)
                new Damage (Damage, armorPenetration).DoDamage (damageable);

            Rigidbody body = hit.rigidbody;
            if (body) {
                body.AddForceAtPosition (transform.forward * Damage, hit.point, ForceMode.Impulse);
            }

            Destroy (gameObject);

            SendMessage ("OnHit", hit, SendMessageOptions.DontRequireReceiver);
        }
    }

}
