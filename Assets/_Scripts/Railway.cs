using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Railway : MonoBehaviour
{
    [SerializeField] private Transform startingRails;
    [SerializeField] private Transform railParent;
    [SerializeField] private Rail prefab;


    private void Start()
    {
        SpawnRails();
    }

    private void SpawnRails()
    {
        Stack<Rail> spawnedRails = new Stack<Rail>();
        foreach (Transform rail in startingRails)
        {
            var newRail = Instantiate(prefab, MyGrid.GetWorldPosition(MyGrid.GetGridPosition(rail.position)), Quaternion.identity, railParent);
            if (spawnedRails.TryPeek(out Rail lastRail))
            {
                newRail.previous = lastRail;
                lastRail.next = newRail;
            }
            else
            {
                newRail.previous = newRail;
            }
            spawnedRails.Push(newRail);
        }
    }
}
