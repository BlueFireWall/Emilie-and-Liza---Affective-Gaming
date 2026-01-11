using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Holds all Tetris groups and can spawn a random one
 */
public class GroupSpawner : MonoBehaviour
{
    public GameObject[] groups;

    // Spawn a new group at the starting position
    public GameObject SpawnNext()
    {
        int index = Random.Range(0, groups.Length);
        Vector3 spawnPosition = new Vector3(3, 14, 0);
        return Instantiate(groups[index], spawnPosition, Quaternion.identity);
    }
}
