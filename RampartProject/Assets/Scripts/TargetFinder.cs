using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetFinder : MonoBehaviour
{
    private const string ENEMY_TAG = "Enemy";
    private const string HERO_TAG = "Hero";
    private const string BUILDING_TAG = "Building";
    private const string CITADEL_TAG = "Citadel";
    private GameObject currentTarget = null;

    public bool chaseTarget = true;

    public GameObject CurrentTarget
    {
        get { return currentTarget; }
    }

    public bool FindTarget(float sightRange, float attackRange)
    {
        GameObject prevTarget = currentTarget;

        if (currentTarget != null)
        {
            if (GetDistance(this.gameObject, currentTarget) > (chaseTarget ? sightRange : attackRange))
                currentTarget = null;
        }

        if (currentTarget == null)
        {
            if (this.CompareTag(ENEMY_TAG))
            {
                GameObject closestHero = FindNearestWithTag(HERO_TAG);
                GameObject closestCitadel = FindNearestWithTag(CITADEL_TAG);

                if (closestHero == null || GetDistance(this.gameObject, closestHero) > sightRange)
                {
                    currentTarget = closestCitadel;
                }
                else
                {
                    currentTarget = closestHero;
                }
            }
            else if (this.CompareTag(HERO_TAG))
            {
                GameObject closestEnemy = FindNearestWithTag(ENEMY_TAG);

                if (closestEnemy == null || GetDistance(this.gameObject, closestEnemy) > sightRange)
                {
                    currentTarget = null;
                }
                else
                {
                    currentTarget = closestEnemy;
                }
            }
            else if (this.CompareTag(BUILDING_TAG))
            {
                GameObject closestEnemy = FindNearestWithTag(ENEMY_TAG);

                if (closestEnemy == null || GetDistance(this.gameObject, closestEnemy) > sightRange)
                {
                    currentTarget = null;
                }
                else
                {
                    currentTarget = closestEnemy;
                }
            }
        }

        return prevTarget != currentTarget;
    }

    protected GameObject FindNearestWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        return FindNearest(objects);
    }

    protected GameObject FindNearest(ICollection items)
    {
        GameObject nearest = null;

        float minDistance = Mathf.Infinity;
        foreach (GameObject item in items)
        {
            float currentDistance = GetDistance(this.gameObject, item);
            if (currentDistance < minDistance)
            {
                nearest = item;
                minDistance = currentDistance;
            }
        }
    
        return nearest;
    }

    protected static float GetDistance(GameObject a, GameObject b)
    {
        return Vector3.Distance(a.transform.position, b.transform.position);
    }
}
