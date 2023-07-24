using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class VideoData
{
    private static Dictionary<int, object> videoDatas = new();

    public static void Set(VideoDataType videoDataType, object obj)
    {
        videoDatas[GetHash(videoDataType)] = obj;
    }

    public static EpisodeType SetEpisode(string videoName)
    {
        string[] video = videoName.Split('$');
        
        return EpisodeType.Introduce;
    }

    public static int GetHash<T>(T value)
    {
        return value.ToString().GetHashCode();
    }
}
public enum VideoDataType
{
    name,    // ���� �̸�
    url,     // ���� ��ũ
    sender,  // ���� ���� ���

    Count
}