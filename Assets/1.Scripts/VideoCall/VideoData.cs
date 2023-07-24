using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class VideoData
{
    public string videoName;
    private string videoURL;
    public string sender;
    public EpisodeType episodeType { get { return SetEpisode(videoName); }}

    public string GetVideoURL { get { return videoURL; } }
}

public enum EpisodeType
{
    Introduce,

    Count,
}
