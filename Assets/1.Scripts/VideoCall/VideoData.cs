using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoData
{
    public string videoName;
    private string videoURL;
    public string sender;

    public string GetVideoURL { get { return videoURL; } }
}

public enum EpisodeType
{
    Introduce,

    Count,
}
