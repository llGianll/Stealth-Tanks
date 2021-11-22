using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HugeTank : EnemyUnit
{
    public override void Death()
    {
        base.Death();
        gameObject.SetActive(false);
    }

}
