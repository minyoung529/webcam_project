using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Video;
using YoutubePlayer;
public class TriggerVideo : MonoBehaviour
{
    [SerializeField] private string urlString = "";

    private VideoPlayer video;

    private void Awake()
    {
        video = GetComponent<VideoPlayer>();
        //GetUrl();
    }
    void Start()
    {

        video.PlayYoutubeVideoAsync(urlString);
    }
    private void OnEnable()
    {
     //   video.Play();
    }
    private void GetUrl()
    {
    ////아이디만 받아오기
    //string ExtractFileId(string url)
    //{
    //    url = url.Replace("https://drive.google.com/file/d/", "");
    //    url = url.Replace("/view?usp=sharing", "");

    //    return url;
    //}
    
        video.url = urlString;
        
        video.audioOutputMode = VideoAudioOutputMode.AudioSource;
        video.EnableAudioTrack(0, true);
        video.Prepare();

    }

    public IEnumerator DownLoadGet(string URL)
    {
        UnityWebRequest request = UnityWebRequest.Get(URL);

        yield return request.SendWebRequest();
        // 에러 발생 시
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            File.WriteAllBytes(urlString, request.downloadHandler.data); // 파일 다운로드
        }
    }
}
