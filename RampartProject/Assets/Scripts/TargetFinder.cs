using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetFinder : MonoBehaviour
{
    private const string ENEMY_TAG = "Enemy";
    private const string HERO_TAG = "Hero";
    private const string BUILDING_TAG = "Building";
    private GameObject currentTarget;
    public float sightRange = 10;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget != null)
        {
            if (GetDistance(this.gameObject, currentTarget) > sightRange)
                currentTarget = null;
        }

        if (currentTarget == null)
        {
            if (this.CompareTag(ENEMY_TAG))
            {
                GameObject closestHero = FindNearestWithTag(ENEMY_TAG);
                GameObject closestBuilding = FindNearestWithTag(BUILDING_TAG);

                if (closestHero == null || GetDistance(this.gameObject, closestHero) > sightRange)
                {
                    currentTarget = closestBuilding;
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
        }
    }

    private GameObject FindNearestWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        return FindNearest(objects);
    }

    public GameObject FindNearest(ICollection items)
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
