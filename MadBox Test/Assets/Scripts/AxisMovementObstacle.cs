using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisMovementObstacle : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    [SerializeField] float distance = 5f;
    [SerializeField] float actualPosition = 0f;
    [SerializeField] float speed = 1f;

    Vector3 startPoint;
    Vector3 positiveEnd;
    bool positiveMovement = true;
    Vector3 normalizedDirection;
    [SerializeField] Vector3 actualTarget;

    void Start()
    {
        normalizedDirection = direction.normalized;
        startPoint = -normalizedDirection * actualPosition + transform.position;
        positiveEnd = startPoint + normalizedDirection * distance;
        actualTarget = positiveEnd;
    }

    // Controls an obstacle that moves between two points in line
    void Update()
    {
        if (Vector3.Distance(transform.position, actualTarget) < 0.2f)
        {
            normalizedDirection = -normalizedDirection;
            actualTarget = startPoint + normalizedDirection*distance;
        }
        else transform.position += normalizedDirection * speed * Time.deltaTime;
    }
}
