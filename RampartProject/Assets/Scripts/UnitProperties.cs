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

	public GameObject currentTarget;

	Animator animator;

    protected float lastAttackTime;

    public abstract void Attack(GameObject target);

    public void TakeDamage(int damage)
    {
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

