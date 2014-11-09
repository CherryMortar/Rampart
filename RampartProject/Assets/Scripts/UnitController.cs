using UnityEngine;
using Pathfinding;
using System.Collections;

public class UnitController : AIController {

    protected const float WAYPOINT_MIN_DISTANCE = 0.2f;

    protected Path path;
    protected int currentWaypoint;

    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<Seeker>();
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();

        if (aiActive)
        {
            if (path != null)
                MoveOnPath();
        }
    }

    protected override void onTargetChanged()
    {
        if(targetFinder.CurrentTarget != null)
            gameObject.GetComponent<Seeker>().StartPath(gameObject.transform.position, targetFinder.CurrentTarget.transform.position,delegate(Path p)
                {
                    this.path = p;
                    currentWaypoint = 0;
                });
    }

    void MoveOnPath()
    {
        if (Vector3.Distance(gameObject.transform.position, unitProperties.CurrentTarget.transform.position) <= unitProperties.attackRange + unitProperties.CurrentTarget.GetComponent<UnitProperties>().unitRadius)
            return;

        if (animator != null && animator.GetInteger("state") != 2)
        {
            animator.SetInteger("state", 2);
        }

        Vector3 moveDir = (path.vectorPath[currentWaypoint] - gameObject.transform.position).normalized;
        gameObject.transform.position += moveDir * unitProperties.moveSpeed * Time.fixedDeltaTime;
        gameObject.transform.LookAt(path.vectorPath[currentWaypoint]);

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
