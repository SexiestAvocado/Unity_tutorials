using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Transform target;
    private float speed = 15.0f;
    private bool homing;
    private float bulletStrength = 15.0f;
    private float aliveTimer = 5.0f;

    // Update is called once per frame
    void Update()
    {
        if (homing && target != null)
        {
            Vector3 moveDirection = (target.transform.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(target);
        }

        StartCoroutine(BulletCountdownRoutine());
    }
    public void Fire(Transform newTarget)
    {
        target = newTarget;
        homing = true;
        Destroy(gameObject, aliveTimer);
    }
    void OnCollisionEnter(Collision col)
    {
        if (target != null)
        {
            if (col.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRigidbody = col.gameObject.GetComponent<Rigidbody>();
                Vector3 away = -col.contacts[0].normal;
                targetRigidbody.AddForce(away * bulletStrength, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }
    IEnumerator BulletCountdownRoutine()//in this case sets a timer outside update method
    {
        yield return new WaitForSeconds(3);//after time it will do things
        Destroy(gameObject);
    }
}
