using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// 영상통화 흐름 스크립트 (받고 수락하고 끊고)
/// 
/// </summary>
public class RecieveVideoCall : MonoBehaviour
{
    [Header("Recieve Call")]
    [SerializeField] private GameObject recievePanel;
    [SerializeField] private UpdateCharacterDataProfile recieveCharacterProfile;

    [Header("Play Call")]
    [SerializeField] private GameObject callingPanel;
    [SerializeField] private PlayVideoCall videoCall;
    [SerializeField] private UpdateCharacterDataProfile callCharacterProfile;

    private CanvasGroup canvas;
    private VideoName video = VideoName.Count;

    private bool load = false;

    private void Awake()
    {
        canvas = GetComponent<CanvasGroup>();
        canvas.alpha = 0.0f;

        EventManager.StartListening(EventName.OnVideoLoadComplete, Init);
    }

    private void Init()
    {
        load = true;
    }

    [ContextMenu("Test")]
    private void Test()
    {
        Recieve(VideoName.NamSoJeong_Introduce);
    }

    /// <summary>
    /// 영상통화 신호 연결 상태로 만드는 함수
    /// </summary>
    public void Recieve(VideoName video)
    {
        if (!load)
        {
            Debug.LogWarning("Video Loding Not Complete");
            return;
        }

        this.video = video;
        Character sender = GetURLFile.GetVideoCallSender(video);

        recieveCharacterProfile.Trigger(sender);
        callCharacterProfile.Trigger(sender);

        canvas.alpha = 1;
        recievePanel.SetActive(true);
        callingPanel.SetActive(false);
    }

    /// <summary>
    /// 영상통화 수락하는 함수
    /// </summary>
    public void Calling()
    {
        if (video == VideoName.Count) return;

        callingPanel.SetActive(true);
        recievePanel.SetActive(false);

        videoCall.Trigger(GetURLFile.GetVideoURL(video));
    }

    /// <summary>
    /// 영상통화 끊는 함수
    /// </summary>
    public void BreakVideoCall()
    {
        canvas.alpha = 0;

       videoCall.StopVideo();
       video = VideoName.Count;
    }

    private void OnDestroy()
    {
        EventManager.StopListening(EventName.OnVideoLoadComplete, Init);
    }
}
