using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour
{
    public Vector3 targetPosition;
    public GameObject targetObject;
    public float speed = 10;
    protected int damage = 0;
    public float maxRange = Mathf.Infinity;
    public bool tracking = true;
    public bool AoE = false;
    public float AoERadius = 2;

    protected float passedRange = 0;

    public int Damage { get { return damage; } set { damage = value; } }

    void Update()
    {
        if (targetObject == null)
            tracking = false;

        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (tracking)
        {
            transform.LookAt(targetObject.transform);
            targetPosition = targetObject.transform.position;
        }
        else
        {
            transform.LookAt(targetPosition);
        }

        passedRange += Time.deltaTime * speed;
        if (passedRange > maxRange)
        {
            Explode();
        }
    }

    private void Explode()
    {
        if (AoE)
        {
            Collider [] hitObjects = Physics.OverlapSphere(transform.position, AoERadius);

            foreach(Collider collider in hitObjects)
            {
                if(collider.gameObject.CompareTag("Enemy"))
                {
                    collider.gameObject.GetComponent<UnitProperties>().TakeDamage(damage);
                }
            }
        }

        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {         
            UnitProperties props = other.gameObject.GetComponent<UnitProperties>();
            if(!AoE)
                props.TakeDamage(damage);

            Explode();
        }
        else if (other.gameObject.CompareTag("Terrain"))
        {
            Explode();
        }
    }
}
