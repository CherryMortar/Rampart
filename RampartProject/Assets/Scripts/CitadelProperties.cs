using UnityEngine;
using System.Collections;

public class CitadelProperties : RangeUnitProperties {
    public GameObject explosion;

    public override bool CheckIsDead()
    {
        if (health < 0)
        {
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);

            GameObject.Find("MainObject").GetComponent<MainScript>().GameState = RampartGameState.Loss;
            Debug.Log("You are loss.");
            return true;
        }
        return false;
    }
}
