using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Lomztein.PlaceholderName.Extensions {

    public static class BoundsExtension {

        public static float CalcVolume (this Bounds bounds) {
            return bounds.size.x * bounds.size.y * bounds.size.z;
        }

    }

}
