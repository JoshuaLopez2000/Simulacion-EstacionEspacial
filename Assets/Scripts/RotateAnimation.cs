using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class RotateAnimation : MonoBehaviour
{
    public int horientationRotation = 1;
    public float rotationSpeed = 20f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around its local Y axis at 20 degrees per second
        transform.Rotate((UnityEngine.Vector3.left) * horientationRotation, rotationSpeed * Time.deltaTime);
    }
}
