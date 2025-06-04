using UnityEngine;

public class RobotCamera : MonoBehaviour
{
    static private Transform target;
    [SerializeField] private Transform initialTarget;

    private void Awake()
    {
        if (target == null)
        {
            if (initialTarget != null)
            {
                target = initialTarget;
            }
            else
            {
                FindRobot();
            }
        }
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

    public static void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
