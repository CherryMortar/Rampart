﻿using UnityEngine;
using System.Collections;

public abstract class AIController : MonoBehaviour {

    public TargetFinder targetFinder;

    public float targetSearchInterval = 1f;
    protected float lastTargetSearch = 0;
    protected float lastAttackTime = 0;

    public bool aiActive = true;
    protected MainScript mainScript;
    public UnitProperties unitProperties;

    public Animator animator;

	// Use this for initialization
	void Start () 
	{
	}
	
	protected void FixedUpdate () 
	{
        if (mainScript == null)
        {
            mainScript = GameObject.Find("MainObject").GetComponent<MainScript>();
        }

        if (aiActive && mainScript.GameState == RampartGameState.WavePhase)
        {
            if (Time.time - lastTargetSearch > targetSearchInterval)
            {
                if (targetFinder.FindTarget(unitProperties.sightRange, unitProperties.attackRange) && targetFinder.CurrentTarget != null)
                {
                    unitProperties.CurrentTarget = targetFinder.CurrentTarget;
                    
                }

                onTargetChanged();
                lastTargetSearch = Time.time;
            }

            if (unitProperties.CurrentTarget != null)
            {
                //Debug.Log(unitProperties.CurrentTarget);
                if (Vector3.Distance(gameObject.transform.position, unitProperties.CurrentTarget.transform.position) <= unitProperties.attackRange + unitProperties.CurrentTarget.GetComponent<UnitProperties>().unitRadius)
                {
                    if (animator != null && animator.GetInteger("state") != 2)
                    {
                        animator.SetInteger("state", 2);
                    }

                    unitProperties.FaceTarget();
                    if (Time.time - lastAttackTime > unitProperties.reloadAttackTime)
                    {
                        unitProperties.Attack(unitProperties.CurrentTarget);
                        lastAttackTime = Time.time;
                    }
                }
            }
        }
	}

    protected abstract void onTargetChanged();
}
