using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float altura;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y + altura, transform.position.z);
        //transform.DOMove(new Vector3(target.position.x, target.position.y + altura, transform.position.z), 0.5f);

    }
}
