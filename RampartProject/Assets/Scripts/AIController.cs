using UnityEngine;
using Pathfinding;
using System.Collections;

public class AIController : MonoBehaviour {

    protected const float WAYPOINT_MIN_DISTANCE = 0.2f;

    public TargetFinder targetFinder;

    public float targetSearchInterval = 0.5f;
    protected float lastTargetSearch = 0;

    protected Path path;
    protected int currentWaypoint;

    public bool aiActive;
    public MainScript mainScript;
    public UnitProperties unitProperties;

	// Use this for initialization
	void Start () 
	{
	}
	
	void FixedUpdate () 
	{
        if (aiActive)
        {
            if (Time.time - lastTargetSearch > targetSearchInterval)
            {
                if (targetFinder.FindTarget(unitProperties.sightRange) && targetFinder.CurrentTarget != null)
                {
                    unitProperties.currentTarget = targetFinder.CurrentTarget;
                    mainScript.pathfindingManager.FindPath(gameObject.transform.position, targetFinder.CurrentTarget.transform.position, 
                    delegate(Path p)
                    {
                        this.path = p;
                        currentWaypoint = 0;
                    });
                }

                lastTargetSearch = Time.time;
            }


            if (path != null)
                MoveOnPath();
        }
	}

    void MoveOnPath()
    {
        Vector3 moveDir = (path.vectorPath[currentWaypoint] - gameObject.transform.position).normalized;
        gameObject.transform.position += moveDir * unitProperties.moveSpeed * Time.fixedDeltaTime;

        if (Vector3.Distance(gameObject.transform.position, path.vectorPath[currentWaypoint]) < WAYPOINT_MIN_DISTANCE)
        {
            currentWaypoint++;

            if (currentWaypoint == path.vectorPath.Count)
            {
                path = null;
            }
        }

        //Debug.Log("Movement " + path.vectorPath[currentWaypoint] + " " + path.vectorPath[currentWaypoint] + " " + Vector3.Distance(gameObject.transform.position, path.vectorPath[currentWaypoint]) + " " + moveDir.ToString() + " " + unitProperties.moveSpeed + " " + Time.fixedDeltaTime);
    }
}
