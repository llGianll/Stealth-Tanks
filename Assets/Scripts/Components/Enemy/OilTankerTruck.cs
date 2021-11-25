using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilTankerTruck : EnemyUnit
{
    [SerializeField] GameObject _explosionRadiusGO;
    public override void Death()
    {
        base.Death();
        Explode();
    }

    void Explode()
    {
        _explosionRadiusGO.SetActive(true);
        //_explosionRadiusGO.GetComponent<ExplosionRadius>().Explode();
        //_enemyModel.SetActive(false);
        //_explosionRadiusGO.SetActive(false);
        //gameObject.SetActive(false);
    }
}
