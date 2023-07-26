using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using UnityEngine.Video;
using YoutubePlayer;

/// <summary>
/// 영상 링크 받아서 실행 및 Character Data UI Update
/// 
/// - Trigger() 시 실행
/// - ChangeVideoURL() 함수로 링크 바꾸기
/// </summary>
public class PlayVideoCall : MonoBehaviour
{
    [Header("Character UI")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image profileImage;

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
    public void Trigger(Character sender, string link = "") 
    {
        if(link != "") urlString = link;
        PlayVideo(sender);
    }
    public void Trigger(CharacterName sender, string link = "")
    {
        if(link != "") urlString = link;
        PlayVideo(Character.GetCharacter(sender));
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

    #region Play Video

    private void PlayVideo(Character sender)
    {
        if (sender == null || urlString == "")
        {
            EventManager.TriggerEvent(EventName.OnBreakVideoCall);
            return;
        }
        video.PlayYoutubeVideoAsync(urlString);

        // Sender UI Update
        SetName(sender);
        Addressables.LoadAssetAsync<Sprite>(sender.profileLink.Trim()).Completed += SetSprite;
    }

    #endregion

    #region Sender UI
    private void SetSprite(AsyncOperationHandle<Sprite> handle)
    {
        profileImage.sprite = handle.Result;
    }
    private void SetName(Character character)
    {
        nameText.SetText(character.name);
    }
    #endregion
}
