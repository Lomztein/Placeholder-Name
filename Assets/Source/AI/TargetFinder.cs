using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TargetFinder {

    public static Func<Vector3, Collider, float> SqrDistanceFunction = ((pos, col) => (pos - col.transform.position).sqrMagnitude);

    public static Transform FindClosest (Vector3 position, float range, LayerMask layerMask) {
        return FindLowest (position, range, layerMask, SqrDistanceFunction);
    }

    // Could possibly use an IComparer instead of a function to sort. This would allow for any type of customization.
    public static Transform FindLowest (Vector3 position, float range, LayerMask layerMask, Func<Vector3, Collider, float> sortFunction) {

        Collider [ ] nearby = Physics.OverlapSphere (position, range, layerMask);

        float curValue = float.MaxValue;
        Collider curCol = null;

        foreach (Collider col in nearby) {
            float value = sortFunction (position, col);
            if (value < curValue) {
                curValue = value;
                curCol = col;
            }
        }

        if (curCol)
            return curCol.transform;
        return null;
    }

}
