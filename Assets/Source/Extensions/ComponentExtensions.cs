using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.Extensions {

    public static class ComponentExtensions {

        public static T GetComponentFromRoot<T>(this Component component) {
            return component.transform.root.GetComponentInChildren<T> ();
        }

    }

}
