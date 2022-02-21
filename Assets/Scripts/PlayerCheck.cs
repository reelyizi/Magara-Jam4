using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{
    [SerializeField] private float radius = 10f;
    public GameObject canvasFinish;

    private bool once;
    private void Update()
    {
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) < radius && !once)
        {
            canvasFinish.SetActive(true);
            once = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
