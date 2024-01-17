using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MianCameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float smoothSpeed = 5f;
    public float minX, miny;
    public float maxX, maxy;
    void FixedUpdate()
    {
        if (playerTransform != null)
        {
            // Calculate the target position with the offset
            Vector3 targetPosition =new Vector3(Mathf.Clamp(playerTransform.position.x,minX,maxX ),Mathf.Clamp(playerTransform.position.y,miny,maxy ),-10);

            // Smoothly move the camera towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
