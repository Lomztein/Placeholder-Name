using Lomztein.PlaceholderName.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.PlaceholderName.UI {

    public class HealthBar : MonoBehaviour {

        public IHasHealth parent;
        public Slider healthBar;

        public void Update () {

            healthBar.value = parent.GetHealth () / parent.GetMaxHealth ();

        }

    }

}