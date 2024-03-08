using UnityEngine;
using System.Collections.Generic;
public class SwarmController : MonoBehaviour
{
    public GameObject target;
    public float followSpeed = 1f;
    public float minDistance = 1f;
    public float separationDistance = 2f;
    public int swarmSize = 10;
    public GameObject swarmPrefab;
    public List<GameObject> swarm;
    void Start()
    {
        // Create the swarm
        for (int i = 0; i < swarmSize; i++)
        {
            GameObject member = Instantiate(swarmPrefab, transform.position + Vector3.right *(i * 0.1f), Quaternion.identity) as GameObject;
            swarm.Add(member);
        }
    }
    void Update()
    {
        // Make the swarm follow the target
        foreach (GameObject member in swarm)
        {
            Vector3 targetDir = (target.transform.position - member.transform.position).normalized;
            member.transform.position += targetDir * followSpeed * Time.deltaTime;
        }
        // Maintain minimum distance between members
        for (int i = 0; i < swarm.Count; i++)
        {
            for (int j = i + 1; j < swarm.Count; j++)
            {
                float distance = Vector3.Distance(swarm[i].transform.position, swarm[j].transform.position);
                if (distance < minDistance)
                {
                    Vector3 dir = (swarm[i].transform.position - swarm[j].transform.position).normalized;
                    dir.y = 0;
                    swarm[i].transform.position += dir * (minDistance - distance) / 2f;
                    swarm[j].transform.position -= dir * (minDistance - distance) / 2f;
                }
            }
        }
        return;
        // Maintain separation between members
        for (int i = 0; i < swarm.Count; i++)
        {
            for (int j = i + 1; j < swarm.Count; j++)
            {
                float distance = Vector3.Distance(swarm[i].transform.position, swarm[j].transform.position);
                if (distance < separationDistance)
                {
                    Vector3 dir = (swarm[i].transform.position - swarm[j].transform.position).normalized;
                    dir.y = 0;
                    swarm[i].transform.position += dir * (separationDistance - distance) / 2f;
                    swarm[j].transform.position -= dir * (separationDistance - distance) / 2f;
                }
            }
        }
    }
}