using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public partial class GetURLFile : MonoBehaviour
{
    private static Dictionary<VideoName, VideoData> videoDatas;
    private Sheet<VideoData> sheet = new();

    private void Awake()
    {
        EventManager<List<Character>>.StartListening(EventName.OnCharacterLoadComplete, Init);
        
        // Image
        EventManager<ImageData>.StartListening(EventName.OnSetTextureURL, LoadTexture);
    }

    private void Init(List<Character> list)
    {
        if (list.Count < 1) return;

        videoDatas = new Dictionary<VideoName, VideoData>();
        sheet.Load(SheetName.VIDEO_TEMPLATE, LoadVideoURL);
    }

    #region Video

    /// <summary>
    /// 스프레드시트에서 영상 URL 가져와 저장해두는 함수
    /// </summary>
    private void LoadVideoURL(List<VideoData> datas)
    {
        StartCoroutine(GetVideoURLLog());
    }
    private IEnumerator GetVideoURLLog()
    {
        for (int i = 0; i < sheet.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            videoDatas.Add((VideoName)i, sheet[i]);
        }

        EventManager.TriggerEvent(EventName.OnVideoLoadComplete);
    }

    #endregion

    #region Image

    /// <summary>
    /// Texture를 웹에서 가져오는 함수
    /// </summary>
    private void LoadTexture(ImageData data)
    {
        StartCoroutine(GetTexture(data.image, data.url));
    }
    private IEnumerator GetTexture(RawImage image, string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            image.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
    }

    #endregion

    private void OnDestroy()
    {
        EventManager<List<Character>>.StopListening(EventName.OnCharacterLoadComplete, Init);

        // Image
        EventManager<ImageData>.StopListening(EventName.OnSetTextureURL, LoadTexture);
    }
}

