using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// ������ȭ �帧 ��ũ��Ʈ (�ް� �����ϰ� ����)
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
    /// ������ȭ ��ȣ ���� ���·� ����� �Լ�
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
    /// ������ȭ �����ϴ� �Լ�
    /// </summary>
    public void Calling()
    {
        if (video == VideoName.Count) return;

        callingPanel.SetActive(true);
        recievePanel.SetActive(false);

        videoCall.Trigger(GetURLFile.GetVideoURL(video));
    }

    /// <summary>
    /// ������ȭ ���� �Լ�
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
