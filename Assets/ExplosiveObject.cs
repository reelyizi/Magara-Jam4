using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObject : MonoBehaviour
{

    public Vector3 radius;
    public float force;
    private bool canDown;

    private void Update()
    {
        if(Input.GetMouseButtonDown(2))
        {
            Explode();
        }
        if(canDown)
            StartCoroutine(DownPlace());
    }
    public void Explode()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        Vector3 explotionPos = transform.position;
        Collider[] colliders = Physics.OverlapBox(explotionPos, radius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.isKinematic = false;
                rb.AddExplosionForce(force, explotionPos, 55, 0.05f, ForceMode.Impulse);
            }
        }
        canDown = true;
    }

    IEnumerator DownPlace()
    {
        yield return new WaitForSeconds(3f);
        transform.position = Vector3.MoveTowards(transform.position, transform.position - transform.up * 5, 12 * Time.deltaTime);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        yield return null;
    }
}
