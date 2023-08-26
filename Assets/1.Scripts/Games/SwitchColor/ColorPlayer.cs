using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPlayer : MonoBehaviour
{
    #region Get

    [SerializeField] private float speed = 5f;
    public ColorEnum ColorType { get { return colorType; } }

    #endregion

    #region Variable

    private MeshRenderer renderer = null;
    private ColorEnum colorType = ColorEnum.Red;

    #endregion

    private void Awake()
    {
        renderer = GetComponentInChildren<MeshRenderer>();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetNextColor();
        }

        Move();
    }

    private void Init()
    {
        SetColor(ColorEnum.Yellow);
    }

    #region Move

    private void Move()
    {
        if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.forward * speed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.D))  transform.Translate(Vector3.back * speed * Time.deltaTime);

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
        EventManager<Action>.TriggerEvent(EventName.OnSwitchColorFail, null);
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
}
