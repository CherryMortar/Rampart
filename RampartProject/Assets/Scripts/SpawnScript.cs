using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnScript : MonoBehaviour {

    public float spawnDelay;
    public Queue<GameObject> wave;
    private float lastSpawnTime;
	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate () {
        if (lastSpawnTime + spawnDelay < Time.time)
        {
            Spawn();
            lastSpawnTime = Time.time;
        }
	}

    private GameObject Spawn()
    {
        return (GameObject)Instantiate(wave.Dequeue(), this.transform.position, this.transform.rotation);
    }
}
