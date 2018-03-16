using Lomztein.PlaceholderName.Characters;
using Lomztein.PlaceholderName.Characters.Extensions;
using Lomztein.PlaceholderName.Characters.PhysicalEquipment;
using Lomztein.PlaceholderName.Items;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.PlaceholderName.Weaponary {

    public class Weapon : Tool, IContainer {

        public ItemSlot ammoSlot;

        public GameObject projectilePrefab;
        public IProjectile projectile;
        
        public enum AmmoType { Pistol, Rifle, Shotgun }
        public AmmoType ammoType;

        public Transform [ ] muzzle;
        public float sequenceTime;

        public int magazineCapacity;

        private bool chambered = true;
        public bool HasAmmo { get { return ammoSlot.item != null && ammoSlot.count != 0; } }

        public float rechamberRate;
        public float reloadTime;

        public List<ItemSlot> ItemSlots {
            get {
                return new List<ItemSlot> () { ammoSlot };
            }

            set {
                ammoSlot = value.Single ();
            }
        }

        public OnItemsChangedEvent OnItemsChanged { get; set; }

        public IEnumerator GetEnumerator() {
            return ItemSlots.GetEnumerator ();
        }

        public override void OnEquip(Character character, CharacterEquipment.Slot slot) {
            base.OnEquip (character, slot);

            ammoSlot = parentSlot.item.GetAttribute<ItemSlot> ("AmmoSlot");

            ammoSlot.maxItems = magazineCapacity;
            ammoSlot.lockedType = typeof (IAmmo);

            ammoSlot.maxItems = magazineCapacity;
            ammoSlot.OnItemChanged += ChangeAmmoType;

            Reload ();
            Rechamber ();
        }

        public override void OnUseHeld() {
            StartCoroutine (Fire ());
        }

        private void ChangeAmmoType(ItemSlot slot, Item oldItem, Item newItem) {
            projectilePrefab = ammoSlot.item.prefab.gameObject;
            projectile = projectilePrefab.GetComponent<IProjectile> ();
        }

        public override void OnUnequip(Character character, CharacterEquipment.Slot slot) {
            base.OnUnequip (character, slot);
        }

        private void UseAmmo () {
            ammoSlot.ChangeCount (-1);
        }

        private void Rechamber () {
            chambered = true;
        }

        public void Reload() {
            int space = ammoSlot.maxItems - ammoSlot.count;
            ItemSlot withAmmo = parentCharacter.inventory.FindItemByPredicate (x => x.item.prefab.gameObject == projectilePrefab);

            if (withAmmo == null)
                withAmmo = parentCharacter.inventory.FindItemByPredicate (x => {
                    IAmmo ammoPrefab = x.item.prefab as IAmmo;
                    if (ammoPrefab != null) {
                        return ammoPrefab.AmmoType == ammoType;
                    }
                    return false;
                }
            );

            if (withAmmo)
                withAmmo.MoveItem (ammoSlot);

            Rechamber ();
        }

        public float GetFirerate () {
            return rechamberRate * parentCharacter.firerateMul.GetAdditiveValue ();
        }

        public float GetSequenceRate () {
            return sequenceTime / parentCharacter.firerateMul.GetAdditiveValue ();
        }

        public float GetReloadRate () {
            return reloadTime / parentCharacter.firerateMul.GetAdditiveValue ();
        }

        private IEnumerator Fire () {
            if (chambered && HasAmmo) {

                chambered = false;
                Invoke ("Rechamber", GetFirerate ());

                for (int i = 0; i < muzzle.Length; i++) {

                    if (HasAmmo) {
                        UseAmmo ();

                        Transform muz = muzzle [ i ];
                        projectile.Create (this, muz);
                        yield return new WaitForSeconds (GetSequenceRate ());
                    }

                }

                if (ammoSlot.count == 0) {
                    if (!IsInvoking ("Reload"))
                        Invoke ("Reload", GetReloadRate ());
                    yield break;
                }

            }
        }
    }

}
