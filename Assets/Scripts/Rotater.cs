using System;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    public float rotationSpeed; 
    
    
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
