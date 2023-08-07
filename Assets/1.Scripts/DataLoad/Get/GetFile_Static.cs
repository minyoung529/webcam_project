using UnityEngine.UI;

/// <summary>
/// 첨부파일 관련 static 함수들
/// </summary>
public partial class GetURLFile
{

    #region Video
   
    /// <summary>
    /// 해당 영상을 보낸 사람
    /// </summary>
    /// <returns>보낸 사람</returns>
    public static Character GetVideoCallSender(VideoName video)
    {
        string[] send = video.ToString().Split('_');
        return Character.GetCharacter(send[0]);
    }

    /// <summary>
    /// 틀고싶은 Image에 영상 URL 세팅
    /// </summary>
    public static void SetVideoURL(PlayVideoCall video, string url)
    {
        video.ChangeVideo(url);
    }

    /// <summary>
    /// 해당 영상의 링크 가져오는 함수
    /// </summary>
    /// <returns>영상의 링크</returns>
    public static string GetVideoURL(VideoName video)
    {
        return videoDatas[video].videoURL;
    }

    #endregion

    #region Image

    /// <summary>
    /// URL의 Texture로 이미지 세팅
    /// </summary>
    public static void SetTexture(RawImage image, string url)
    {
        ImageData data = new();
        data.image = image;
        data.url = url;

        EventManager<ImageData>.TriggerEvent(EventName.OnSetTextureURL, data);
    }
    #endregion

}

public class VideoData
{
    public string videoName;
    public string videoURL;
    public string sender;
}

public class ImageData
{
    public RawImage image;
    public string url;
}