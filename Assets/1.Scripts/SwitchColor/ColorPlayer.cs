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

    private SpriteRenderer renderer = null;
    private ColorEnum colorType = ColorEnum.Red;

    #endregion

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
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
        SetColor(ColorEnum.Red);
    }

    #region Move

    private void Move()
    {
        if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.left * speed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.D))  transform.Translate(Vector3.right * speed * Time.deltaTime);

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
        renderer.color = GetColor.GetColorEnumToColor(colorType);
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
}
