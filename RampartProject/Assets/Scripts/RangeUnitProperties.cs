using UnityEngine;
using System.Collections;

public class RangeUnitProperties : UnitProperties {

    public GameObject missile;
    public Transform holePosition;

    public override void Attack()
    {
        if (currentTarget && Time.frameCount % 10 == 0)
        {
            GameObject rocket = (GameObject)Instantiate(missile);
            rocket.transform.position = holePosition.position;
            rocket.transform.rotation = holePosition.rotation;
            // SelfGuidedMissile rocketScript = rocket.GetComponent<SelfGuidedMissile>();
            //rocketScript.myTarget = target.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
