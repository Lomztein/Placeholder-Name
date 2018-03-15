using Lomztein.PlaceholderName.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.Characters.PhysicalEquipment {

    public interface IEquipment {

        void OnEquip(Character character, CharacterEquipment.Slot itemSlot);

        void OnUnequip(Character character, CharacterEquipment.Slot itemSlot);

    }

}

