using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable {

    float GetHealth();

    float GetMaxHealth();

    void TakeDamage(Damage damage);

}
