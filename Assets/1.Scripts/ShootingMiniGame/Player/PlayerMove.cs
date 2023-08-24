using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private Transform modelingTransform;
    [SerializeField]
    private float moveSpeed = 50f;
    [SerializeField]
    private float xRange = 2.5f;
    [SerializeField]
    [Range(0f, 0.2f)]
    private float idleRange = 0.1f;

    private Rigidbody rigid;
    private SphereCollider sphereCollider;
    private Animator animator;
    int horizontal = 0, isMove = 0;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        animator = modelingTransform.GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        isMove = Animator.StringToHash("IsMove");
    }
    //-2.5 ~ 2.5
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed * Time.deltaTime;
        rigid.MovePosition(transform.position + movement);
        animator.SetFloat(horizontal, horizontalInput);
        if (horizontalInput > idleRange * -1 && horizontalInput < idleRange)
        {
            animator.SetBool(isMove, false);
        }
        else
        {
            animator.SetBool(isMove, true);
        }

    }
}
