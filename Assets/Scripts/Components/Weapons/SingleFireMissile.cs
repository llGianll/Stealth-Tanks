using System.Linq;
using UnityEngine;

public class SingleFireMissile : Weapon
{
    [Header("Subclass variables")]
    [SerializeField] float _ySpawnOffset = 10f;
    protected override void UseWeapon()
    {
        if (CanFire())
        {
            GameObject projectile = PooledObjectManager.Instance.GetPooledObject(_projectileID);
            projectile.transform.position = _targetMode.Target.First().gameObject.transform.position + new Vector3(0, _ySpawnOffset, 0);
            projectile.SetActive(true);
            projectile.GetComponent<Projectile>().Move(_targetMode.Target.First());
        }
    }
}
