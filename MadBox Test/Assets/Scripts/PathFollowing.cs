using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowing : MonoBehaviour
{
    // Path precalculated for player and camera
    [SerializeField] List<Vector3> playerRoute = new List<Vector3>();
    [SerializeField] List<Vector3> cameraRoute = new List<Vector3>();
    [SerializeField] bool debug = true;
    public static PathFollowing instance = null;
    // Update is called once per frame

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Code that calculate the direction that player or camera have to move next
    public Vector3 RuteDirection(bool camera, Vector3 position, ref Vector3 actualTarget, out int actualIndex)
    {
        List<Vector3> actualRoute;
        if (camera) actualRoute = cameraRoute;
        else actualRoute = playerRoute;
        actualIndex = actualRoute.IndexOf(actualTarget);
        if (Vector3.Distance(position, actualTarget) < 0.2f)
        {
            if (actualIndex != actualRoute.Count - 1)
            {
                actualTarget = actualRoute[actualIndex + 1];
            }
            else return Vector3.zero;
        }
        return Vector3.Normalize(actualTarget - position);

    }

    // Allow to see the path of player and camera with gizmos
    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.red;
            for (int i=0; i<playerRoute.Count-1; i++)
            {
                Gizmos.DrawLine(playerRoute[i], playerRoute[i + 1]);
                Gizmos.DrawSphere(playerRoute[i], 0.5f);
            }
            if(playerRoute.Count > 0) Gizmos.DrawSphere(playerRoute[playerRoute.Count-1], 0.5f);

            Gizmos.color = Color.blue;
            for (int i = 0; i < cameraRoute.Count - 1; i++)
            {
                Gizmos.DrawLine(cameraRoute[i], cameraRoute[i + 1]);
                Gizmos.DrawSphere(cameraRoute[i], 0.5f);
            }
            if (cameraRoute.Count > 0) Gizmos.DrawSphere(cameraRoute[cameraRoute.Count - 1], 0.5f);
        }
    }
}
