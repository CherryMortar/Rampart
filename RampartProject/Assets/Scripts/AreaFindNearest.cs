using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaFindNearest : MonoBehaviour
{

    private const string ENEMY_TAG = "Enemy";
    private const string HERO_TAG = "Hero";
    private const string BUILDING_TAG = "Building";
    private GameObject currentTarget;
    private const int SIGHT_RANGE = 10;
    private GameObject nearest;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (this.CompareTag(ENEMY_TAG))
        {
            if(currentTarget!=null && currentTarget.CompareTag(HERO_TAG) 
                && (Vector3.Distance(this.transform.position,currentTarget.transform.position)<=SIGHT_RANGE))
            {
                //Debug.Log("Stayed on the same hero");
            }
            if( currentTarget ==null && FindNearestWithTag(HERO_TAG)==null)
            {
               // Debug.Log("Find first building");
                currentTarget = FindNearestBuilding();
            }
            else if (currentTarget == null && FindNearestWithTag(HERO_TAG) != null)
            {
                //Debug.Log("Find first hero");
                currentTarget = FindNearestWithTag(HERO_TAG);
            }
            else if (currentTarget != null && currentTarget.CompareTag(BUILDING_TAG) && FindNearestWithTag(HERO_TAG) != null)
            {
                //Debug.Log("Find first hero after first building");
                currentTarget = FindNearestWithTag(HERO_TAG);
            }
            else if(currentTarget != null && FindNearestWithTag(HERO_TAG)!=null && currentTarget.CompareTag(HERO_TAG)
                && (Vector3.Distance(this.transform.position,currentTarget.transform.position)>SIGHT_RANGE))
            {
                GameObject probableTarget = FindNearestWithTag(HERO_TAG);
                if (probableTarget != null 
                    && (Vector3.Distance(this.transform.position, probableTarget.transform.position) <= SIGHT_RANGE))
                {
                   // Debug.Log("Find second hero");
                    currentTarget = probableTarget;
                }
                else
                {
                 //   Debug.Log("Find second building");
                    currentTarget = FindNearestBuilding();
                }
            }
            else if (currentTarget != null && currentTarget.CompareTag(BUILDING_TAG) && FindNearestWithTag(HERO_TAG) == null)
            {
               // Debug.Log("Stayer on the same building");
            }
        }
        else if (this.CompareTag(HERO_TAG))
        {
            if (currentTarget != null && (Vector3.Distance(this.transform.position, currentTarget.transform.position)) <= SIGHT_RANGE)
            {
                //Stayed on the same target
                Debug.Log("stayed on the same");
            }
            else if (currentTarget == null && FindNearestWithTag(ENEMY_TAG) !=null)
            {
                //Set enemy
                GameObject probableTarget = FindNearestWithTag(ENEMY_TAG);
                if(probableTarget!=null 
                    && (Vector3.Distance(probableTarget.transform.position,this.transform.position)<=SIGHT_RANGE))
                {
                    Debug.Log("set to enemy");
                    currentTarget = probableTarget;
                }
            }
            else if (currentTarget != null && FindNearestWithTag(ENEMY_TAG) == null)
            {
                //Set to null
                Debug.Log("set to null");
                currentTarget = null;
            }
        }
    }
    
    private GameObject FindNearestBuilding()
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag(BUILDING_TAG);
        return LoopToNearest(buildings);
    }

    private GameObject FindNearestWithTag(string tag)
    {

        Collider[] nearColliders = Physics.OverlapSphere(this.transform.position, SIGHT_RANGE);
        if (nearColliders != null && nearColliders.Length != 0)
        {
            List<GameObject> probableTargets = new List<GameObject>();
            foreach (Collider collider in nearColliders)
            {
                if (collider.gameObject.CompareTag(tag))
                {
                    probableTargets.Add(collider.gameObject);
                }
            }
            if (probableTargets == null || probableTargets.Count == 0)
            {
                return null;
            }
            else if (probableTargets.Count == 1)
            {
                return probableTargets[0];
            }
            else
            {
                return LoopToNearest(probableTargets);
            }
        }
        return null;
    }

    public GameObject LoopToNearest(ICollection items)
    {
        if (nearest != null)
        {
            nearest = null;
        }

        float minDistance = Mathf.Infinity;
        foreach (GameObject item in items)
        {
            float currentDistance = Vector3.Distance(this.transform.position, item.transform.position);
            if (currentDistance < minDistance)
            {
                nearest = item;
                minDistance = currentDistance;
            }
        }
    
        return nearest;
    }
}
