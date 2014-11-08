using UnityEngine;
using System.Collections;

public class UnitProperties : MonoBehaviour
{
	public int health;
	public int attack;
	public int attackRange;
	//private bool dead;
	public int sightRange;
	public float reloadAttackTime;
	public float lastAttackTime;
	public GameObject currentTarget;
	Animator animator;

	public virtual void Attack()
	{
		//Play Attack Animation
		Debug.Log("Attack in parrent");
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

