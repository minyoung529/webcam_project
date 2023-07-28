using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using YoutubePlayer;

/// <summary>
/// ���� ��ũ �޾Ƽ� ���� �� Character Data UI Update
/// 
/// - Trigger() �� ���� ����
/// - ChangeVideoURL() �Լ��� ��ũ �ٲٱ�
/// </summary>
public class PlayVideoCall : MonoBehaviour
{
    [Header("Video Camera")]
    [SerializeField] private VideoPlayer video;

    private string urlString = "";

    // Change Video 
    public void ChangeVideo(string url)
    {
        urlString = url;
        video.PlayYoutubeVideoAsync(urlString);
        video.Pause();
    }

    // Play Video
    public void Trigger(string link = "") 
    {
        if(link != "") urlString = link;
        PlayVideo();
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

    private void PlayVideo()
    {
        if (urlString == "")
        {
            Debug.Log("Not URL");
            return;
        }
        video.PlayYoutubeVideoAsync(urlString);
    }

}
