using UnityEngine;
using System.Collections;

public class MeleeUnitProperties : UnitProperties
{
    public override void Attack(GameObject target)
    {
        UnitProperties enemyProperties = currentTarget.GetComponent<UnitProperties>();

        enemyProperties.TakeDamage(this.attack);
    }

    public override bool CheckIsDead()
    {
        if (base.CheckIsDead())
        {
            return true;
        }
        return false;
    }
}
