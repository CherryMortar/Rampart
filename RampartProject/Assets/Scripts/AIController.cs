using UnityEngine;
using System.Collections;

public abstract class AIController : MonoBehaviour {

    public TargetFinder targetFinder;

    public float targetSearchInterval = 0.5f;
    protected float lastTargetSearch = 0;
    protected float lastAttackTime = 0;

    public bool aiActive;
    public MainScript mainScript;
    public UnitProperties unitProperties;

	// Use this for initialization
	void Start () 
	{
	}
	
	protected void FixedUpdate () 
	{
        if (aiActive)
        {
            if (Time.time - lastTargetSearch > targetSearchInterval)
            {
                if (targetFinder.FindTarget(unitProperties.sightRange, unitProperties.attackRange) && targetFinder.CurrentTarget != null)
                {
                    unitProperties.currentTarget = targetFinder.CurrentTarget;
                    
                }

                onTargetChanged();
                lastTargetSearch = Time.time;
            }

            if (unitProperties.currentTarget != null)
            {
                if (Vector3.Distance(gameObject.transform.position, unitProperties.currentTarget.transform.position) <= unitProperties.attackRange)
                {
                    if (Time.time - lastAttackTime > unitProperties.reloadAttackTime)
                    {
                        unitProperties.Attack(unitProperties.currentTarget);
                        lastAttackTime = Time.time;
                    }
                }
            }
        }
	}

    protected abstract void onTargetChanged();
}
