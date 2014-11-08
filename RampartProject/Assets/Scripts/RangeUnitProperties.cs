using UnityEngine;
using System.Collections;

public class RangeUnitProperties : UnitProperties {

    public GameObject missile;
    public Transform holePosition;

    public override void Attack()
    {
        if (currentTarget && lastAttackTime + reloadAttackTime < Time.time)
        {
            Debug.Log("in");
            FireProjectile();            
        }
    }

    private void FireProjectile()
    {
        GameObject rocket = (GameObject)Instantiate(missile,holePosition.position,holePosition.rotation);
        SelfGuidedMissile rocketScript = rocket.GetComponent<SelfGuidedMissile>();
        rocketScript.myTarget = currentTarget.transform;
        rocketScript.myDmg = attack;
        lastAttackTime = Time.time;
    }
}
