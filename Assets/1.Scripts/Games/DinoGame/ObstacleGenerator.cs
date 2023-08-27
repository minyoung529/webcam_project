using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    private float spawnInterval = 2f;
    private float counter = 0f;

    private void Update()
    {
        counter += Time.deltaTime;

        if(counter >= spawnInterval)
        {
            counter -= spawnInterval;

        }
    }

    private void Spawn()
    {

    }
}
