using UnityEngine;

public class RobotCamera : MonoBehaviour
{
    static private Transform target;

    private void Awake()
    {
        FindRobot();
    }

    private void Update()
    {
        FollowRobot();
    }

    void FollowRobot()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position;
            targetPosition.z = transform.position.z;
            transform.position = targetPosition;
        }
    }

    static public void FindRobot()
    {
        GameObject coreObject = GameObject.FindGameObjectWithTag("Player");
        if (coreObject != null)
        {
            target = coreObject.transform;
        }
    }
}
