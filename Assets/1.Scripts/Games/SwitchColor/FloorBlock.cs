using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FloorBlock : MonoBehaviour
{
    #region Get

    public MeshRenderer Renderer { get { return renderer; } }
    public ColorEnum ColorType { get { return colorType; } }
    public bool CanMove => canMove;
    public float Speed => speed;

    #endregion

    #region Variable
    
    private float speed = 10f;

    private MeshRenderer renderer = null;

    private ColorEnum colorType = ColorEnum.Count;

    private Vector3 startPos = new Vector3(0f, -20f, -45f);

    private bool canMove = true;

    #endregion

    private void Awake()
    {
        renderer ??= GetComponentInChildren<MeshRenderer>();
    }

    private void Start()
    {
        StartListen();
    }

    private void StartListen()
    {
        EventManager.StartListening(EventName.OnMiniGameStart, GameStart);
        EventManager.StartListening(EventName.OnMiniGameOver, GameOver);
    }
    private void StopListen()
    {
        EventManager.StopListening(EventName.OnMiniGameStart, GameStart);
        EventManager.StopListening(EventName.OnMiniGameOver, GameOver);
    }


    private void GameStart()
    {
        speed = 10f;
        SetColor(ColorEnum.Yellow);
        canMove = true;
    }
    private void GameOver()
    {
        canMove = false;
    }

    private void Update()
    {
        if (CanMove) Move();
    }

    #region Move

    public void SetSpeed(float value)
    {
        speed = value;  
    }
    private void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (transform.position.z >= 55f)
        {
            transform.position = startPos;
            RandomNextColor();
        }
    }

    #endregion

    #region Color

    private void RandomNextColor()
    {
        SetColor(GetColor.GetRandomColorEnum());
    }

    private void SetColor(ColorEnum color)
    {
        colorType = color;
        renderer.material.SetColor("_EmissionColor", GetColor.GetColorEnumToColor(color));
    }


    #endregion

    private void OnDestroy()
    {
        StopListen();
    }

}
