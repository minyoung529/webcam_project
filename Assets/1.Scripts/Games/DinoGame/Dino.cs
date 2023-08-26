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

    private FaceController faceController;

    private bool isGameOver = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        faceController = FindObjectOfType<FaceController>();

        if (faceController)
        {
            faceController.Event.StartListening((int)FaceEvent.MouthOpen, Jump);
        }

        EventManager.StartListening(EventName.OnMiniGameStart, OnGameRestart);
    }

#if UNITY_EDITOR
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
#endif

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

    private void OnCollisionEnter(Collision collision)
    {
        if (!isJumping) return;

        // ¹Ù´Ú Ã¼Å©
        if (collision.gameObject.CompareTag("Bottom"))
        {
            JumpEnd();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isGameOver) return;

        if (other.gameObject.CompareTag("Obstacle"))
        {
            EventManager.TriggerEvent(EventName.OnMiniGameOver);
            animator.SetTrigger("Dead");
            isGameOver = true;
        }
    }

    private void OnGameRestart()
    {
        Debug.Log("RESTART");
        isGameOver = false;
        animator.SetTrigger("Restart");
    }

    private void OnDestroy()
    {
        if (faceController)
        {
            faceController.Event.StopListening((int)FaceEvent.MouthOpen, Jump);
        }

        EventManager.StopListening(EventName.OnMiniGameStart, OnGameRestart);
    }
}
