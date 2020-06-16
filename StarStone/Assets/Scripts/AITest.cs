using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITest : MonoBehaviour
{
    private NavMeshAgent m_enemyNavMesh;

    public Transform Destination;

    // Start is called before the first frame update
    void Start()
    {
        m_enemyNavMesh = GetComponent<NavMeshAgent>();

        SetDestination();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetDestination()
    {
        Vector3 targetVector = Destination.transform.position;
        m_enemyNavMesh.SetDestination(targetVector);
    }
}
