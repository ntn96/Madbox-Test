using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    [SerializeField] Vector3 rotation = Vector3.forward;
    [SerializeField] float speed = 2f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation*speed);
    }
}
