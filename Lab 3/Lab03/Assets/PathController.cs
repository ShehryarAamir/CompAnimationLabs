using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    [SerializeField]
    public PathManager pathManager;

    List<WayPoint> thePath;
    WayPoint target;

    public float moveSpeed;
    public float rotateSpeed;

    public Animator animator;
    bool IsFlying;

    // Start is called before the first frame update
    void Start()
    {
        thePath = pathManager.GetPath();
        if (thePath != null && thePath.Count > 0)
        {
            //set the starting target to the first waypoint
            target = thePath[0];
        }

        IsFlying = false;
        animator.SetBool("IsFlying", IsFlying);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            print("This is flying");
            //toggle if any key is pressed
            IsFlying = !IsFlying;
            animator.SetBool("IsFlying", IsFlying);
        }
        if (IsFlying)
        {
            rotateTowardsTarget();
            MoveForward();
        }
    }

    void rotateTowardsTarget()
    {
        float stepSize = rotateSpeed * Time.deltaTime;

        Vector3 targetDir = target.pos - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stepSize, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    void MoveForward()
    {
        float stepSize = Time.deltaTime * moveSpeed;
        float distanceToTarget = Vector3.Distance(transform.forward, target.pos);

        if (distanceToTarget < stepSize)
        {
            //we will overshoot the  target
            //so we should do something about it
            return;
        }

        //take a step forward
        Vector3 moveDir = Vector3.forward;
        transform.Translate(moveDir * stepSize);
    }

    private void OnTriggerEnter(Collider other)
    {
        //switch to the next target
        target = pathManager.GetNextTarget();
    }

}
