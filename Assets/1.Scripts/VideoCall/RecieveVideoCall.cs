using OpenCVForUnity.VideoModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class RecieveVideoCall : MonoBehaviour
{
    [Header("Recieve Call")]
    [SerializeField] private GameObject recievePanel;

    [Header("Play Call")]
    [SerializeField] private GameObject callingPanel;
    [SerializeField] private PlayVideoCall videoCall;

    private Character sender;
    private CanvasGroup canvas;

    private Sheet<VideoData> sheet = new();
    private Dictionary<string, VideoData> videoDatas;

    private void Awake()
    {
        canvas = GetComponent<CanvasGroup>();
        canvas.alpha = 0.0f;

        EventManager<List<Character>>.StartListening(EventName.OnCharacterLoadComplete, Init);
    }
    private void Start()
    {
        videoDatas = new Dictionary<string, VideoData>();
        sheet.Load(SheetName.VIDEO_TEMPLATE, VideoURLLog);
    }

    [ContextMenu("Call")]
    private void Test()
    {
        Character send = Character.GetCharacter(CharacterName.NamSoJeong);
        EventManager<Character>.TriggerEvent(EventName.OnCallingVideoCall, send);
    }

    // Change Calling Sender
    public void ChangeCallingCharacter(Character character)
    {
        sender = character;
    }
    public void ChangeCallingCharacter(CharacterName name)
    {
        sender = Character.GetCharacter(name);
    }

    // Yes Call
    public void Calling()
    {
        if (sender == null) return;

        callingPanel.SetActive(true);
        recievePanel.SetActive(false);

        string videoName = sender.name + "_" + "ภฮป็";
        videoCall.Trigger(sender, videoDatas[videoName].GetVideoURL);
        EventManager.StartListening(EventName.OnBreakVideoCall, BreakVideoCall);
    }
    
    // Break Call
    public void BreakVideoCall()
    {
        Debug.Log("Break Call");
        canvas.alpha = 0;

        videoCall.StopVideo();
        sender = null;

        EventManager.StopListening(EventName.OnBreakVideoCall, BreakVideoCall);
    }

    #region VideoCall
    private void Init(List<Character> list)
    {
        if (list.Count < 1) return;

        sender = list[0];
        EventManager<Character>.StartListening(EventName.OnCallingVideoCall, CallingVideoCall);
    }
    private void CallingVideoCall(Character character)
    {
        sender = character;

        canvas.alpha = 1;
        recievePanel.SetActive(true);
        callingPanel.SetActive(false);

        Debug.Log(sender + " : Calling VideoCall");
    }
    #endregion

    #region Load Video Sheet
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
            type.videosURL = sheet[i].GetVideoURL;

            videoDatas.Add(sheet[i].videoName, sheet[i]);
        }
    }
    #endregion

    private void OnDestroy()
    {
        EventManager<List<Character>>.StopListening(EventName.OnCharacterLoadComplete, Init);
        EventManager<Character>.StopListening(EventName.OnCallingVideoCall, CallingVideoCall);
    }
}
public class VideoType
{
    public string videosURL;
}