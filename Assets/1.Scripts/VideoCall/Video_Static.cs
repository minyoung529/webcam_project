using System.Collections.Generic;

public partial class VideoCall
{
    private static Dictionary<VideoName, VideoData> videoDatas;

    public static Character GetVideoCallSender(VideoName video)
    {
        string[] send = video.ToString().Split('_');
        return Character.GetCharacter(send[0]);
    }

    public static string GetVideoURL(VideoName video)
    {
        return videoDatas[video].videoURL;
    }
}

public enum VideoName
{
    NamSoJeong_Introduce,
    LeeMinYoung_Introduce,

    Count
}