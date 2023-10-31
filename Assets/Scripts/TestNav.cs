using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestNav : MonoBehaviour
{
    public Transform target;

    NavMeshAgent _agent;

    Animator _animator;

    private const string MOTIONTYPE  = "MotionType";

    private const float STOP_DISTANCE = 0.1f;

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        _agent = gameObject.AddComponent<NavMeshAgent>();
    }


    private void Start()
    {

        _agent.stoppingDistance = STOP_DISTANCE;
        _agent.SetDestination(target.position);

        _animator.SetInteger(MOTIONTYPE, 1);
    }


    private void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) <= STOP_DISTANCE)
        {
            _animator.SetInteger(MOTIONTYPE, 0);
        }
    }
}
