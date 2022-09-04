using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    private Vector2 followSpot;
    private Vector2 stuckDistanceCheck;

    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float perspectiveScale = 1f, scaleRatio = 1.5f;
    
    private Animator animator;
    private NavMeshAgent agent;

    private void Start()
    {
        followSpot = transform.position; 
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        { 
            followSpot = new Vector2(mousePosition.x, mousePosition.y);
        }
        agent.SetDestination(new Vector3(followSpot.x, followSpot.y, transform.position.z));
       
        //AdjustPerspective();
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        float distance = Vector2.Distance(transform.position, followSpot);
        if (Vector2.Distance(stuckDistanceCheck, transform.position) == 0) { animator.SetFloat("distance", 0f); return; }
        animator.SetFloat("distance", distance);

        if(distance > 0.01f)
        {
            Vector3 direction = transform.position - new Vector3(followSpot.x, followSpot.y, transform.position.y);
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            animator.SetFloat("angle", angle);
            stuckDistanceCheck = transform.position;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        followSpot = transform.position;
    }

    private void AdjustPerspective()
    {
        Vector3 scale = transform.localScale;
        scale.x = perspectiveScale * (scaleRatio - transform.position.y);
        scale.y = perspectiveScale * (scaleRatio - transform.position.y);
        transform.localScale = scale;
    }

    private void AdjustSortingLayer()
    {

    }

}
