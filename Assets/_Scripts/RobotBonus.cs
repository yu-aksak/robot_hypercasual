using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotBonus : MonoBehaviour
{
    float delayF;
    [SerializeField]
    Animator animator;
    
    [SerializeField]
    NavMeshAgent navMeshAgent;

    bool movement,breaked;

    LevelBonusManager lvlBManager;

    [SerializeField]
    Transform point;
    // Start is called before the first frame update
    void Start()
    {
        lvlBManager = GameObject.FindGameObjectWithTag("LevelBonusManager").GetComponent<LevelBonusManager>();
        breaked = false;
        movement = false;
        delayF = Random.Range(0.1f, 0.5f);
        StartCoroutine(AnimationDelay(delayF));
    }

    void MoveToTarget(Transform transform)
    {
        navMeshAgent.SetDestination(transform.position);
        point = transform;
        movement = true;
    }
    GameObject latestCoin;
    Transform FindClosestCoin()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Coin");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        latestCoin = closest;
        if(closest != null)
            return closest.transform;
        return null;
    }

    private void Update()
    {
        if (!breaked)
        {
            if (movement)
            {
                if (point != null)
                {
                    if (Vector3.Distance(point.position, navMeshAgent.transform.position) <= 1.3f)
                    {
                        if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude <= 4f)
                        {
                            movement = false;
                            Destroy(latestCoin);
                        }
                    }
                }
                else
                {
                    movement = false;
                }
            }
            else
            {
                if (FindClosestCoin() != null)
                {
                    MoveToTarget(FindClosestCoin());
                }
                else
                {
                    animator.SetBool("lose", true);
                    animator.SetBool("win", false);
                    breaked = true;
                }
            }
        }
    }

    public IEnumerator AnimationDelay(float time)
    {
        yield return new WaitForSeconds(time);
        animator.enabled = true;
        animator.SetBool("win", true);
    }
}
