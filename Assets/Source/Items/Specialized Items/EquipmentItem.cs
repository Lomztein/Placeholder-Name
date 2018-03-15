using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lomztein.PlaceholderName.Characters;

namespace Lomztein.PlaceholderName.Items {

    [CreateAssetMenu (fileName = "New Equipment", menuName = "Items/Equipment")]
    public class EquipmentItem : ItemPrefab, IEquipable {

        public Characters.Equipment.Type Type { get { return type; } set { type = value; } }
        public Characters.Equipment.Type type;

    }

}
