using UnityEngine;
using System.Collections;

public class MissileMovement : MonoBehaviour
{

	public float mySpeed = 10f;
	public float myRange = 10f;
	public GameObject myEnemy;
	public int myDmg;
	private float myDist;
	void Start()
	{
		transform.LookAt(myEnemy.transform);
	}

	void Update () {
		transform.Translate(Vector3.forward * Time.deltaTime * mySpeed);
		myDist += Time.deltaTime * mySpeed;
		if (myDist >= myRange)
			Destroy(gameObject);
	}
}
