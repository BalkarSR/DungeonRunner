using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int hp { get; set; }

    //dmg method
    void Damage();
}
