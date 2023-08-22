using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    //말풍선, 유저 채팅 rect, 프로필 rect
    [SerializeField]
    private RectTransform bubbleRect, boxRect, textRect;

    //말풍선 꼬리
    [Space(10)]
    [SerializeField]
    private GameObject trail;
    //유저 반응
    [SerializeField]
    private EmojiList emojis;

    //유저 프로필 (상대 말풍선만 해당)
    [Space(10)]
    [SerializeField]
    private Image userImage;
    [SerializeField]
    private GameObject userImageObj;
    //유저 이름 text (상대 말풍선만 해당)
    [Space(10)]
    [SerializeField]
    private TMP_Text userNameText;

    //누가 보냈는지 기억
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
