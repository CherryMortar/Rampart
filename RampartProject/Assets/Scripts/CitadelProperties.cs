using UnityEngine;
using System.Collections;

public class CitadelProperties : RangeUnitProperties {
    public override bool CheckIsDead()
    {
        if (health < 0)
        {
            GameObject.Find("MainObject").GetComponent<MainScript>().GameState = RampartGameState.Loss;
            Debug.Log("You are loss.");
            return true;
        }
        return false;
    }
}
