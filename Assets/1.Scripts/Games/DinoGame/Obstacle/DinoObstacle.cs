using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DinoObstacle : MonoBehaviour
{
    [SerializeField]
    protected float speedMultiplier;

    private float distance;
    private const float MAX_DISTANCE = 25f;

    public Action<DinoObstacle> ReleaseAction { get; set; }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            Activate();

            if (distance > MAX_DISTANCE)
            {
                // ∏±∏Æ¡Ó
                ReleaseAction?.Invoke(this);
            }
        }
    }

    protected virtual void Activate()
    {
        float speed = DinoGameController.Speed * speedMultiplier * Time.deltaTime;
        distance += speed;
        transform.Translate(Vector3.left * speed);
    }

    #region POOL
    public void OnCreated()
    {

    }

    public void OnGet()
    {
        distance = 0f;
        gameObject.SetActive(true);
    }

    public void OnRelease()
    {
        distance = 0f;
        gameObject.SetActive(false);
    }

    public void OnDestroyed()
    {

    }
    #endregion
}
