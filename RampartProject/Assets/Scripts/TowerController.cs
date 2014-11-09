using UnityEngine;
using System.Collections;

public class TowerController : AIController {

	void Start () 
	{
	
	}

	new void FixedUpdate () 
	{
        base.FixedUpdate();
	}

    protected override void onTargetChanged()
    {
    }
}
