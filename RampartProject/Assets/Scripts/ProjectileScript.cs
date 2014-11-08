using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour
{
    public Vector3 targetPosition;
    public GameObject targetObject;
    public float speed = 10;
    public int damage = 0;
    public float maxRange = Mathf.Infinity;
    public bool tracking = true;

    protected float passedRange = 0;

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
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {         
            UnitProperties props = other.gameObject.GetComponent<UnitProperties>();
            props.TakeDamage(damage);
            Explode();
        }
        else if (other.gameObject.CompareTag("Terrain"))
        {
            Explode();
        }
    }
}
