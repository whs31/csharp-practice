using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    [Range(0.01f, 1)]
    private float cameraSpeed;

    private GameObject target;
    private Vector3 targetPosition;

    private float interpolateVelocity;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Manager").GetComponent<PlayerStats>().player;
        targetPosition = transform.position;
    }

    private void Update()
    {
        if(target != null)
        {
            Vector3 noZ = transform.position;
            noZ.z = target.transform.position.z;
            Vector3 targetDirection = target.transform.position - noZ;

            interpolateVelocity = targetDirection.magnitude * 10f;
            targetPosition = transform.position + (targetDirection.normalized * interpolateVelocity * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, targetPosition + offset, cameraSpeed);
        }
    }
}