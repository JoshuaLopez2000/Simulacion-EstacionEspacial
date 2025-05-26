using System.Numerics;
using UnityEngine;

public class TeleportOutside : MonoBehaviour
{
    public Transform targetTransform;
    public UnityEngine.Vector3 newPosition;
    public GameObject colliders;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        targetTransform.position = newPosition;
        colliders.SetActive(true);
    }
}
