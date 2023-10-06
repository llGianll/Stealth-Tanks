using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SingleFireMissile : Weapon
{
    [Header("Scriptable Object References")]
    [SerializeField] EnergyManager _energyManager;

    [Header("Subclass variables")]
    [SerializeField] float _ySpawnOffset = 10f;
    protected override void UseWeapon()
    {
        if (MouseTarget.Instance.HitCollider.GetComponent<GridTileProcessor>() != null)
        {
            if (_energyManager.DecreaseEnergy(_energyCost))
            {
                GameObject projectile = PooledObjectManager.Instance.GetPooledObject(_projectileID);
                //projectile.transform.position = _targetMode.TargetTile.gameObject.transform.position + new Vector3(0, _ySpawnOffset, 0);
                projectile.transform.position = _targetMode.Target.First().gameObject.transform.position + new Vector3(0, _ySpawnOffset, 0);
                projectile.SetActive(true);
                //projectile.GetComponent<Projectile>().Move(_targetMode.TargetTile);
                projectile.GetComponent<Projectile>().Move(_targetMode.Target.First());
            }
        }
    }
}
