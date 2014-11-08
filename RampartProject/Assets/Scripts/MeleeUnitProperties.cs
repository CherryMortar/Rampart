using UnityEngine;
using System.Collections;

public class MeleeUnitProperties : UnitProperties
{
    
    public override void Attack()
    {
        UnitProperties enemyProp = currentTarget.GetComponent<UnitProperties>();
        if (Vector3.Distance(this.transform.position, currentTarget.transform.position) <=attackRange
            && lastAttackTime + reloadAttackTime < Time.time)
        {
            //Animation
            enemyProp.health = enemyProp.health  - attack;
            //Debug.Log(enemyProp.health);
            enemyProp.CheckIsDead();
            lastAttackTime = Time.time;
        }
    }
}
