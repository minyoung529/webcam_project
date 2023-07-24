using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TestDataLoadWithVideo : MonoBehaviour
{
    [SerializeField] private TriggerVideo video;

    private Sheet<VideoData> sheet = new();
    private Dictionary<string, VideoData> videoDatas;

    private void Start()
    {
        videoDatas= new Dictionary<string, VideoData>();
        sheet.Load(SheetName.VIDEO_TEMPLATE, VideoURLLog);
    }

    private void VideoURLLog(List<VideoData> datas)
    {
        StartCoroutine(GetURLLog());
    }

    private IEnumerator GetURLLog()
    {
        for (int i = 0; i < sheet.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);

            VideoType type = new VideoType();
            type.episode = sheet[i].episodeType;
            type.videosURL = sheet[i].GetVideoURL;

            videoDatas.Add(sheet[i].videoName, sheet[i]);
        }

        Test();
    }

    private void Test()
    {
        video.ChangeVideoURL(videoDatas["소정_인사"].GetVideoURL);
        video.Trigger();
    }

}
public class VideoType
{
    public EpisodeType episode;
    public string videosURL;
}
