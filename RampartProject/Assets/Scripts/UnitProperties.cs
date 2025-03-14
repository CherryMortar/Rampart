﻿using UnityEngine;
using System.Collections;

public abstract class UnitProperties : MonoBehaviour
{
	public int health;
	public int attack;
	public float attackRange;
	public float sightRange;
	public float reloadAttackTime;
	public float moveSpeed;
    public float unitRadius;
    public int price;

	protected GameObject currentTarget;

	public GameObject CurrentTarget { get{ return currentTarget;} set{currentTarget = value;} }

	Animator animator;

	protected float lastAttackTime;

	public abstract void Attack(GameObject target);

    public virtual void FaceTarget()
    {
    }

	public void TakeDamage(int damage)
	{
		this.health -= damage;
		CheckIsDead();
	}

	public virtual bool CheckIsDead()
	{
		if (health < 0)
		{
			//Play Dead Animation
			Destroy(gameObject);
			currentTarget = null;
            return true;
		}
        return false;
	}
}

