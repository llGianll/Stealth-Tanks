using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilTankerTruck : EnemyUnit
{
    [SerializeField] GameObject _explosionRadiusGO;
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
        Explode();
        _explosionRadiusGO.SetActive(true);
        _explosionRadiusGO.GetComponent<ExplosionRadius>().Explode();
        _enemyModel.SetActive(false);
        //_explosionRadiusGO.SetActive(false);
        //gameObject.SetActive(false);
    }

    void Explode()
    {

    }
}
