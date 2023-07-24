using Microsoft.Unity.VisualStudio.Editor;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.Video;
using YoutubePlayer;

/// <summary>
/// ���� ��ũ �޾Ƽ� ����
/// 
/// - Trigger() �� ����
/// - ChangeVideoURL() �Լ��� ��ũ �ٲٱ�
/// </summary>
public class TriggerVideo : MonoBehaviour
{
    [SerializeField] private string urlString = "";
    [SerializeField] private bool playOnAwake = false;

    private VideoPlayer video;

    private void Awake()
    {
        video = GetComponent<VideoPlayer>();
    }

    private void Start()
    {
        if(playOnAwake) Trigger();
    }

    public void ChangeVideoURL(string url)
    {
        urlString = url;
    }

    [ContextMenu("Trigger")]
    public void Trigger()
    {
        if (urlString == "") return;
        video.PlayYoutubeVideoAsync(urlString);
    }

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
