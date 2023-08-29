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

        Init();
    }

    private void Init()
    {
        SetColor(ColorEnum.Yellow);
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
        renderer.material.color = GetColor.GetColorEnumToColor(colorType);
    }


    #endregion

}
