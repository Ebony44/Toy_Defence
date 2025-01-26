using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public float updateRate = 0.2f;
    private NavMeshAgent agent;

    public Transform GetTransform()
    {
        return transform;
    }

    public void TakeDamage(int damage)
    {

        // throw new NotImplementedException();
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(TrackTarget());
    }

    private IEnumerator  TrackTarget()
    {
        if(target == null)
        {
            Debug.LogError("Target is not set");
            yield break;
        }

        WaitForSeconds wait = new WaitForSeconds(updateRate);
        // please generate variable

        while (enabled)
        {
            agent.SetDestination(target.transform.position);
            yield return null;

        }
    }

    


    // Update is called once per frame
    void Update()
    {
        
    }
}
