using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshScript : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent agent;
    private Animator animator;
    bool isWalking = true;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            agent.destination = target.transform.position;
        }
        else
        {
            agent.destination = transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "enemy")
        {
            isWalking = false;
            animator.SetTrigger("ATTACK");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "enemy")
        {
            isWalking = true;
            animator.SetTrigger("WALK");
        }
    }
}