using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : MonoBehaviour
{
    private Rigidbody rigid;
    private BoxCollider collider;
    private Animator animator;

    [SerializeField]
    private float jumpForce = 10f;
    private bool isJumping = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Sliding();
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            SlidingEnd();
        }
    }

    #region JUMP

    private void Jump()
    {
        if (isJumping) return;
        isJumping = true;

        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;

        rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void JumpEnd()
    {
        isJumping = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isJumping) return;

        // ¹Ù´Ú Ã¼Å©
        if (collision.gameObject.CompareTag("Bottom"))
        {
            JumpEnd();
        }
    }
    #endregion

    #region SLIDING
    private void Sliding()
    {
        //if (isJumping) return;
        animator.SetBool("Sliding", true);
    }

    private void SlidingEnd()
    {
        animator.SetBool("Sliding", false);
    }
    #endregion
}
