using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SwitchColor : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Vector3 playerStartPos;

    private MeshRenderer renderer = null;
    private FaceController faceController;

    private ColorEnum colorType = ColorEnum.Red;
    public ColorEnum ColorType { get { return colorType; } }

    private bool leftMove = false;
    private bool rightMove = false;

    private void Start()
    {
        faceController = FindObjectOfType<FaceController>();
        renderer = GetComponentInChildren<MeshRenderer>();
        playerStartPos = transform.position;

        StartListening();
        StartGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetNextColor();
        }

        Move();
    }

    private void ResetPlayer()
    {
        transform.position = playerStartPos;
        SetColor(ColorEnum.Yellow);
    }

    #region GameFlow
    public void StartGame()
    {
        if (faceController)
        {
            faceController.Event.StartListening((int)FaceEvent.MouthOpen, SetNextColor);

            faceController.Event.StartListening((int)FaceEvent.LeftEyeClose, LeftMove);
            faceController.Event.StartListening((int)FaceEvent.LeftEyeOpen, StopLeftMove);
            faceController.Event.StartListening((int)FaceEvent.RightEyeClose, RightMove);
            faceController.Event.StartListening((int)FaceEvent.RightEyeOpen, StopRightMove);
        }

        ResetPlayer();
    }
    public void StopGame()
    {
        ResetPlayer();
        if (faceController)
        {
            faceController.Event.StopListening((int)FaceEvent.MouthOpen, SetNextColor);

            faceController.Event.StopListening((int)FaceEvent.LeftEyeClose, LeftMove);
            faceController.Event.StopListening((int)FaceEvent.LeftEyeOpen, StopLeftMove);
            faceController.Event.StopListening((int)FaceEvent.RightEyeClose, RightMove);
            faceController.Event.StopListening((int)FaceEvent.RightEyeOpen, StopRightMove);
        }
    }
    #endregion

    #region Move
    private void LeftMove()
    {
        leftMove = true;
    }
    private void StopLeftMove()
    {
        leftMove = false;
    }
    private void RightMove()
    {
        rightMove = true;
    }
    private void StopRightMove()
    {
        rightMove = false;
    }

    private void Move()
    {
        if (leftMove) transform.Translate(Vector3.forward * speed * Time.deltaTime);
        else if (rightMove) transform.Translate(Vector3.back * speed * Time.deltaTime);

        Wall();
    }
    private void Wall()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0f) pos.x = 0f;
        if (pos.x > 1f) pos.x = 1f;
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
    #endregion

    #region Color
    private void SetColor(ColorEnum nextColor)
    {
        colorType = nextColor;
        renderer.material.color = GetColor.GetColorEnumToColor(colorType);
    }

    private void SetNextColor()
    {
        int index = (int)colorType + 1;
        if (index >= (int)ColorEnum.Count)
        {
            index = (int)ColorEnum.None + 1;
        }

        SetColor((ColorEnum)index);
    }
    #endregion

    #region Trigger()

    /// <summary>
    /// 플레이어와 다른 색깔인데 충돌했을 때 실행되는 함수
    /// </summary>
    /// <param name="player"></param>
    private void Different(FloorBlock floor)
    {
        EventManager.TriggerEvent(EventName.OnMiniGameOver);
    }

    /// <summary>
    /// 플레이어와 같은 색깔인데 충돌했을 때 실행되는 함수
    /// </summary>
    /// <param name="player"></param>
    private void Same(FloorBlock floor)
    {
    }

    /// <summary>
    /// 플레이어와 충돌 시
    /// </summary>
    /// <param name="player"></param>
    private void TriggerPlayer(FloorBlock floor)
    {
        if (GetColor.IsSameColor(colorType, floor))
        {
            Same(floor);
        }
        else
        {
            Different(floor);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        FloorBlock floor = collision.collider.GetComponent<FloorBlock>();
        TriggerPlayer(floor);
    }

    #endregion

    #region Event

    private void StartListening()
    {
        EventManager.StartListening(EventName.OnMiniGameOver, StopGame);
        EventManager.StartListening(EventName.OnMiniGameStart, StartGame);
    }
    

    private void StopListening()
    {
        EventManager.StopListening(EventName.OnMiniGameOver, StopGame);
        EventManager.StopListening(EventName.OnMiniGameStart, StartGame);
    }

    #endregion

    private void OnDestroy()
    {
        if (faceController)
        {
            faceController.Event.StopListening((int)FaceEvent.MouthOpen, SetNextColor);

            faceController.Event.StopListening((int)FaceEvent.LeftEyeClose, LeftMove);
            faceController.Event.StopListening((int)FaceEvent.LeftEyeOpen, StopLeftMove);
            faceController.Event.StopListening((int)FaceEvent.RightEyeClose, RightMove);
            faceController.Event.StopListening((int)FaceEvent.RightEyeOpen, StopRightMove);
        }

        StopListening();
    }

}