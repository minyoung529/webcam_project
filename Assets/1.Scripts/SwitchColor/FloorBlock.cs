using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FloorBlock : MonoBehaviour
{
    #region Get

    [SerializeField] private float speed = 10f;
    public SpriteRenderer Renderer { get { return renderer; } }
    public ColorEnum ColorType { get { return colorType; } }

    #endregion

    #region Variable

    private SpriteRenderer renderer = null;

    private ColorEnum colorType = ColorEnum.Count;

    #endregion

    private void Awake()
    {
        renderer ??= GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        colorType = GetColor.GetRandomColorEnum();
    }

    private void Update()
    {
        Move();
    }

    #region Move

    private void Move()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    #endregion

    #region Trigger()

    /// <summary>
    /// �÷��̾�� �ٸ� �����ε� �浹���� �� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="player"></param>
    protected virtual void Different(ColorPlayer player)
    {

    }
    /// <summary>
    /// �÷��̾�� ���� �����ε� �浹���� �� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="player"></param>
    protected virtual void Same(ColorPlayer player)
    {

    }

    /// <summary>
    /// �÷��̾�� �浹 ��
    /// </summary>
    /// <param name="player"></param>
    private void TriggerPlayer(ColorPlayer player)
    {
        if (GetColor.IsSameColor(colorType, player))
        {
            Same(player);
        }
        else
        {
            Different(player);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            ColorPlayer player = collision.collider.GetComponent<ColorPlayer>();
            TriggerPlayer(player);
        }
    }

    #endregion
}
