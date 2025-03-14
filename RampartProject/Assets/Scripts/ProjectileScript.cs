﻿using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour
{
    protected Vector3 targetPosition;
    protected GameObject targetObject;
    public float speed = 10;
    protected int damage = 0;
    public float maxRange = Mathf.Infinity;
    public bool tracking = true;
    public bool AoE = false;
    public float AoERadius = 2;
    public GameObject impactEffect;

    protected float passedRange = 0;

    public int Damage { get { return damage; } set { damage = value; } }
    public GameObject TargetObject { get { return targetObject; } set { targetObject = value; targetPosition = targetObject.transform.position; } }

    void Update()
    {
        if (targetObject == null)
            tracking = false;

        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (tracking)
        {
            targetPosition = targetObject.transform.position + new Vector3(0, 1, 0);
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

        if (impactEffect != null)
        {
            impactEffect.GetComponent<ParticleSystem>().playbackSpeed = 11;
            Instantiate(impactEffect, gameObject.transform.position, Quaternion.identity);
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
