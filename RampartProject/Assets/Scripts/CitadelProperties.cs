using UnityEngine;
using System.Collections;

public class CitadelProperties : RangeUnitProperties {
    public override void CheckIsDead()
    {
        if (health < 0)
        {
            GameObject.Find("MainObject").GetComponent<MainScript>().GameState = RampartGameState.Loss;
            Debug.Log("You are loss.");
        }
    }
}
