using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Transform transformCamera;
    [SerializeField] private Vector2 parallax;
    private Vector3 lastCameraPosition;

    // Update is called once per frame

    private void Start()
    {
        lastCameraPosition = transformCamera.position;
    }
    void LateUpdate()
    {
        Vector3 deltaMovement = transformCamera.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallax.x, deltaMovement.y * parallax.y);
        lastCameraPosition = transformCamera.position;
    }
}
