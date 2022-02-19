using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothness = 1.25f;
    private Vector3 offset;

    public bool screenShake;
    public float shakeStrength;
    public float shakeDuration;
    private Vector3 shake = Vector3.zero;
    private float elapsedTime = 0f;

    [SerializeField] private Vector3 maxOffset, minOffset;
    [SerializeField] private float zoomModifierY,zoomModifierZ, rotateSpeed;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void Update()
    {
        HandleZoom();
        CameraFollow();
        if (screenShake)
            GenerateShake();
    }

    private void CameraFollow()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, target.position + offset, smoothness * Time.deltaTime);
    }

    private void GenerateShake()
    {
        if (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.position += Random.insideUnitSphere * shakeStrength;
        }
        else
        {
            screenShake = !screenShake;
            elapsedTime = 0f;
        }
    }
    private void HandleZoom()
    {
        //transform.LookAt(target.position);
        if (Input.GetAxis("Mouse ScrollWheel")>0)
        {
            //Zoom in
            transform.position += new Vector3(0, -zoomModifierY, zoomModifierZ);
            transform.Rotate(-rotateSpeed,0,0);

        }
        if (Input.GetAxis("Mouse ScrollWheel")<0)
        {
            //Zoom out
            transform.position+= new Vector3(0, zoomModifierY, -zoomModifierZ);
            transform.Rotate(rotateSpeed,0,0);

        }
    }
}
