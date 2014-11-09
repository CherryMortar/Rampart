using UnityEngine;
using System.Collections;

public class RangeUnitProperties : UnitProperties {

    public GameObject projectile;
    public Vector3 firePosition;
    public GameObject weapon;

    private Quaternion weaponInitialRotation = Quaternion.identity;
    private bool weaponInitialRotaionSet = false;

    public float bonusRotation = 0;

    public override void Attack(GameObject target)
    {
        FireProjectile(target);            
    }

    public override void FaceTarget()
    {
        base.FaceTarget();

        if (!weaponInitialRotaionSet)
        {
            weaponInitialRotation = weapon.transform.rotation;
            weaponInitialRotaionSet = true;
        }

        if (weapon != null)
        {
            Vector3 relativePos = transform.position - new Vector3(currentTarget.transform.position.x, transform.position.y, currentTarget.transform.position.z);
            weapon.transform.rotation = weaponInitialRotation * Quaternion.AngleAxis(bonusRotation, new Vector3(0, 1, 0)) * Quaternion.LookRotation(relativePos, new Vector3(0, 1, 0));
        }
    }

    private void FireProjectile(GameObject target)
    {
        GameObject projectileInstance = (GameObject)Instantiate(projectile, transform.position + firePosition, Quaternion.identity);
        ProjectileScript projectileScript = projectileInstance.GetComponent<ProjectileScript>();

        projectileScript.TargetObject = target;
        projectileScript.Damage = this.attack;
    }
}
