using UnityEngine;
using Pathfinding;
using System.Collections;

public abstract class UnitController : AIController {

    protected const float WAYPOINT_MIN_DISTANCE = 0.2f;

    protected Path path;
    protected int currentWaypoint;

    // Use this for initialization
    void Start()
    {
    }

    void FixedUpdate()
    {
        if (aiActive)
        {
            if (path != null)
                MoveOnPath();
        }
    }

    protected override void onTargetChanged()
    {
        mainScript.pathfindingManager.FindPath(gameObject.transform.position, targetFinder.CurrentTarget.transform.position,
            delegate(Path p)
            {
                this.path = p;
                currentWaypoint = 0;
            });
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
