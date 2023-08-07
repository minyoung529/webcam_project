using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using YoutubePlayer;
/// <summary>
/// 
/// ������ȭ�� ���� ���� ���� �Լ�
/// 
/// </summary>
public class PlayVideoCall : MonoBehaviour
{
    [Header("Video Camera")]
    [SerializeField] private VideoPlayer video;

    private string urlString = "";

    /// <summary>
    /// ���� ��ũ �ٲٴ� �Լ� (������ ���� Trigger())
    /// </summary>
    public void ChangeVideo(string url)
    {
        urlString = url;
        video.PlayYoutubeVideoAsync(urlString);
        video.Pause();
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void Trigger(string link = "") 
    {
        if(link != "") urlString = link;
        PlayVideo();
    }
    private void PlayVideo()
    {
        if (urlString == ""){
            Debug.Log("Not URL");
            return;
        }
        video.PlayYoutubeVideoAsync(urlString);
    }


    // Video PlaySetting
    [ContextMenu("Pause")]
    public void PauseVideo()
    {
        video.Pause();
    }
    [ContextMenu("Stop")]
    public void StopVideo()
    {
        video.Stop();
    }
}
