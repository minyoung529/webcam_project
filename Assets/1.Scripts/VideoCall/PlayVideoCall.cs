using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using YoutubePlayer;
/// <summary>
/// 
/// 영상통화의 영상 실행 관련 함수
/// 
/// </summary>
public class PlayVideoCall : MonoBehaviour
{
    [Header("Video Camera")]
    [SerializeField] private VideoPlayer video;

    private string urlString = "";

    /// <summary>
    /// 영상 링크 바꾸는 함수 (실행은 따로 Trigger())
    /// </summary>
    public void ChangeVideo(string url)
    {
        urlString = url;
        video.PlayYoutubeVideoAsync(urlString);
        video.Pause();
    }

    /// <summary>
    /// 영상 실행
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
