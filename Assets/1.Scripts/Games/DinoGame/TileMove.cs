using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMove : MonoBehaviour
{
    [SerializeField]
    private float speedMultiplier = 1f;

    [SerializeField]
    private Transform spawnTransform;

    [SerializeField]
    private Transform resetPosition;

    private void Update()
    {
        transform.Translate(Vector3.left * DinoGameController.Speed * speedMultiplier * Time.deltaTime);

        if (transform.position.x < resetPosition.transform.position.x)
        {
            transform.position = spawnTransform.position;
        }
    }
}