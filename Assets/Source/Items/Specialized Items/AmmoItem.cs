using System.Collections;
using System.Collections.Generic;
using Lomztein.PlaceholderName.Weaponary;
using UnityEngine;

namespace Lomztein.PlaceholderName.Items {

    [CreateAssetMenu (fileName = "New Ammo", menuName = "Items/Ammo")]
    public class AmmoItem : ItemPrefab, IAmmo {

        [SerializeField] private Weapon.AmmoType ammoType;
        public Weapon.AmmoType AmmoType {
            get { return ammoType; }
            set { ammoType = value; }
        }
    }

}
