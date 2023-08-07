using UnityEngine.UI;

/// <summary>
/// ÷������ ���� static �Լ���
/// </summary>
public partial class GetURLFile
{

    #region Video
   
    /// <summary>
    /// �ش� ������ ���� ���
    /// </summary>
    /// <returns>���� ���</returns>
    public static Character GetVideoCallSender(VideoName video)
    {
        string[] send = video.ToString().Split('_');
        return Character.GetCharacter(send[0]);
    }

    /// <summary>
    /// Ʋ����� Image�� ���� URL ����
    /// </summary>
    public static void SetVideoURL(PlayVideoCall video, string url)
    {
        video.ChangeVideo(url);
    }

    /// <summary>
    /// �ش� ������ ��ũ �������� �Լ�
    /// </summary>
    /// <returns>������ ��ũ</returns>
    public static string GetVideoURL(VideoName video)
    {
        return videoDatas[video].videoURL;
    }

    #endregion

    #region Image

    /// <summary>
    /// URL�� Texture�� �̹��� ����
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