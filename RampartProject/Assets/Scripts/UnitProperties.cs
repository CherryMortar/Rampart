using UnityEngine;
using System.Collections;

public abstract class UnitProperties : MonoBehaviour
{
	public int health;
	public int attack;
	public float attackRange;
	public float sightRange;
	public float reloadAttackTime;
	public float moveSpeed;

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
		Debug.Log("taking it" + damage);
		this.health -= damage;
		CheckIsDead();
	}

	public void CheckIsDead()
	{
		if (health < 0)
		{
			//Play Dead Animation
			Destroy(gameObject);
			currentTarget = null;
		}
	}
}

