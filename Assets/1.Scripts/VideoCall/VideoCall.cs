using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class VideoCall : MonoBehaviour
{
    private Sheet<VideoData> sheet = new();

    private void Awake()
    {
        EventManager<List<Character>>.StartListening(EventName.OnCharacterLoadComplete, Init);
    }

    private void Init(List<Character> list)
    {
        if (list.Count < 1) return;

        videoDatas = new Dictionary<VideoName, VideoData>();
        sheet.Load(SheetName.VIDEO_TEMPLATE, LoadVideoURL);
    }

    private void LoadVideoURL(List<VideoData> datas)
    {
        StartCoroutine(GetURLLog());
    }
    private IEnumerator GetURLLog()
    {
        for (int i = 0; i < sheet.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            videoDatas.Add((VideoName)i, sheet[i]);
        }

        EventManager.TriggerEvent(EventName.OnVideoLoadComplete);
    }

    private void OnDestroy()
    {
        EventManager<List<Character>>.StopListening(EventName.OnCharacterLoadComplete, Init);
    }
}

