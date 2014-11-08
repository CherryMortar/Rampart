using UnityEngine;
using System.Collections;

public class RangeUnitProperties : UnitProperties {

    public GameObject projectile;
    public Vector3 firePosition;

    public override void Attack(GameObject target)
    {
        FireProjectile(target);            
    }

    private void FireProjectile(GameObject target)
    {
        GameObject projectileInstance = (GameObject)Instantiate(projectile, transform.position + firePosition, Quaternion.identity);
        ProjectileScript projectileScript = projectileInstance.GetComponent<ProjectileScript>();

        projectileScript.targetObject = target;
        projectileScript.damage = this.attack;
    }
}
