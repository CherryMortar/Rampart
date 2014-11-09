using UnityEngine;
using System.Collections;

public class CitadelProperties : RangeUnitProperties {
    public void CheckIsDead()
    {
        if (health < 0)
        {
            Debug.Log("You are loss.");
        }
    }
}
