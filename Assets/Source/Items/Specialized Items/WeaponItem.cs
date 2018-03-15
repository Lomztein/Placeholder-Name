using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.Items {

    [CreateAssetMenu (fileName = "New Weapon", menuName = "Items/Weapon")]
    public class WeaponItem : EquipmentItem {

        public override Item CreateItem() {
            Item item = base.CreateItem ();
            item.SetAttribute ("AmmoSlot", ItemSlot.CreateSlot (null));
            return item;
        }
    }

}