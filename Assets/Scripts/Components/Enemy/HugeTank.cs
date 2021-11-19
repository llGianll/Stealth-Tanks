using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HugeTank : EnemyUnit
{
    public override void DecreaseHealth()
    {
        if (!_isRevealed)
            return;

        CurrentHealth -= _damagePerHit;

        if (CurrentHealth <= 0)
            Death();
    }

    public override void Death()
    {
        gameObject.SetActive(false);
    }

}
