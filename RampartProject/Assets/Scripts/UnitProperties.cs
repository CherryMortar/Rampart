using UnityEngine;
using System.Collections;

public class UnitProperties : MonoBehaviour
{
	public int health;
	public int attack;
	public int range;
	private bool dead;
	public int sightRange;
	public float reloadAttackTime;
	Animator animator;

    public virtual void Attack()
    {

    }
}

