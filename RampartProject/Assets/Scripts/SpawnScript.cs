using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnScript : MonoBehaviour {

	public float spawnDelay;
	public Queue<GameObject> wave;
	private float lastSpawnTime = 0f;
	private GameObject lastSpawnedObject;
	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate () {
		if (lastSpawnTime + spawnDelay < Time.time && wave.Count>0) 
		{
			Spawn();
			lastSpawnTime = Time.time;
		}
	}

	private GameObject Spawn()
	{
		if(lastSpawnedObject!=null 
			&& !lastSpawnedObject.transform.position.Equals(this.transform.position))
		{
				lastSpawnedObject = (GameObject)Instantiate(wave.Dequeue(), this.transform.position, this.transform.rotation);
		}
		else if (lastSpawnedObject == null)
		{
			lastSpawnedObject = (GameObject)Instantiate(wave.Dequeue(), this.transform.position, this.transform.rotation);
		}
		return lastSpawnedObject;
	}
}
