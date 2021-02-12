using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotBehavior : MonoBehaviour
{
    float delayF;
    [SerializeField]
    Animator animator;

    [SerializeField]
    NavMeshAgent navMeshAgent;

    [SerializeField]
    GameObject particleSystem;

    Transform point;

    bool movement;
    // Start is called before the first frame update
    void Start()
    {
        movement = false;
        delayF = Random.Range(0.1f, 0.8f);
        StartCoroutine(AnimationDelay(delayF, 0)); 
    }

    public void WinAnim(Transform transform)
    {
        delayF = Random.Range(0.1f, 0.8f);
        StartCoroutine(AnimationDelay(delayF, 2));
        MoveToTarget(transform);
    }

    public void MoveToTarget(Transform transform)
    {
        navMeshAgent.SetDestination(transform.position);
        point = transform;
        movement = true;
    }

    

    private void Update()
    {
        if (movement)
        {
            if (Vector3.Distance(point.position, navMeshAgent.transform.position) <= 1f)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude <= 0.5f)
                {
                    point.GetComponent<RocketAccepter>().RobotReached();
                    Destroy(gameObject);
                }
            }
        }
    }
    public void LoseAnim()
    {
        delayF = Random.Range(0.1f, 0.8f);
        StartCoroutine(AnimationDelay(delayF, 1));
    }

    public IEnumerator AnimationDelay(float time, int animID)
    {
        yield return new WaitForSeconds(time);
        switch (animID) {
            case 0:
                {
                    animator.enabled = true;
                    animator.SetBool("lose",false);
                    
                }
                break;
            case 1:
                {
                    animator.SetBool("lose", true);
                    AnimationDelay(4, 0);
                }
                break;
            case 2:
                {
                    animator.SetBool("win", true);
                    particleSystem.SetActive(false);
                }
                break;
        }
    }
}
