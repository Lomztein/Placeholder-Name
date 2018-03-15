using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Lomztein.PlaceholderName.Items {

    public delegate void OnItemsChangedEvent(Item oldItem, Item newItem);

    public interface IContainer : IEnumerable {

        List<ItemSlot> ItemSlots { get; set; }

        // This can be prone to bugs since it isn't an event, but I can't see any other practical way to implement similar functionality
        // without rewriting larger parts of the inventory. Even then, I'm not so sure what to do. Might redo it in the future.
        /// <summary>
        /// Prone to bugs, be careful with this.
        /// </summary>
        OnItemsChangedEvent OnItemsChanged { get; set; }

    }

}