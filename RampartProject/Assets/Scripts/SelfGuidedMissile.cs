using UnityEngine;
using System.Collections;

public class SelfGuidedMissile : MonoBehaviour
{

    public Transform myTarget;
    public float myRange = 100f;
    public float mySpeed = 10f;
    private float myDist;
    public int myDmg = 0;
    void Update()
    {
        Debug.Log("run");
        transform.Translate(Vector3.forward * Time.deltaTime * mySpeed);
        myDist += Time.deltaTime * mySpeed;
        if (myDist >= myRange)
        {
            Explode();
            Debug.Log("self destroyed");
        }
        if (myTarget)
        {
            transform.LookAt(myTarget);
        }
        else
        {
            Debug.Log("self destroyed");
            Explode();
        }
    }
    private void Explode()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("inTrg");
        if (other.gameObject.CompareTag("Enemy"))
        {
            
            UnitProperties props = other.gameObject.GetComponent<UnitProperties>();
            props.health = props.health - myDmg;
            Debug.Log("set dmg");
            props.CheckIsDead();
            Explode();
        }
    }
}
