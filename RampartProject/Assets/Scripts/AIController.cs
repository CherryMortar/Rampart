using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

    public TargetFinder targetFinder;

    public float targetSearchInterval = 0.5f;
    protected float lastTargetSearch = 0;

	// Use this for initialization
	void Start () 
	{
	}
	
	void Update () 
	{
        if (Time.time - lastTargetSearch > targetSearchInterval)
        {
            targetFinder.FindTarget();
            lastTargetSearch = Time.time;
        }
	}
}
