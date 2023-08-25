using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    //��ǳ��, ���� ä�� rect, ������ rect
    [SerializeField]
    private RectTransform bubbleRect, boxRect, textRect;

    //��ǳ�� ����
    [Space(10)]
    [SerializeField]
    private GameObject trail;
    //���� ����
    [SerializeField]
    private EmojiList emojis;

    //���� ������ (��� ��ǳ���� �ش�)
    [Space(10)]
    [SerializeField]
    private Image userImage;
    [SerializeField]
    private GameObject userImageObj;
    //���� �̸� text (��� ��ǳ���� �ش�)
    [Space(10)]
    [SerializeField]
    private TMP_Text userNameText;

    //���� ���´��� ���
    private string userName,sendTime;

    #region property
    public RectTransform BubbleRect => bubbleRect;
    public RectTransform BoxRect => boxRect;
    public RectTransform TextRect => textRect;

    public GameObject Trail => trail;
    public EmojiList Emojis => emojis;
    public Image UserImage => userImage;
    public GameObject UserImageObj => userImageObj;

    public TMP_Text UserNameText => userNameText;
    public string UserName { get { return userName; } set { userName = value; } }
    public string SendTime { get { return sendTime; } set { sendTime = value; } }
    #endregion
}
