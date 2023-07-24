using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

/// <summary>
/// ĸ�� ��ư (ĸ���ؼ� ������ �Ǵ� ������ ����)
/// </summary>
public class CaptureButton : MonoBehaviour
{
    [SerializeField] private Image screenFade;

    private string folderName = "WebcamGGM";
    private string fileName = "Webcam";
    private string extName = "png";
    private string RootPath
    {
        get
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            return Application.dataPath;
#elif UNITY_ANDROID
            return $"/storage/emulated/0/DCIM/{Application.productName}/";
#endif
        }
    }
    private string folderPath => $"{RootPath}/{folderName}";
    private string TotalPath => $"{folderPath}/{fileName}_{DateTime.Now.ToString("MMdd_HHmmss")}.{extName}";
    private string lastSavedPath;

    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();

        btn.onClick.AddListener(() =>
        {
            ScreenShot();
        });
    }


    public void ScreenShot()
    {
        CheckPermission(Permission.ExternalStorageRead);
    }

    
    // �ȵ���̵� : ����� ���� Ȯ���ϰ� ��û�ϱ�
    private void CheckPermission(string permission)
    {
        if (Permission.HasUserAuthorizedPermission(permission) == false)
        {
            PermissionCallbacks pCallbacks = new PermissionCallbacks();
            // ����
            pCallbacks.PermissionGranted += _ => StartCoroutine(TakePicture());
            // ����
            pCallbacks.PermissionDenied += str => Debug.Log($"����");
            Permission.RequestUserPermission(permission, pCallbacks);
        }
        else
        {
            StartCoroutine(TakePicture());
        }
    }

    private IEnumerator TakePicture()
    {
        yield return new WaitForEndOfFrame();
        ChangeTextureAndSave();
    }

    private void ChangeTextureAndSave()
    {
        string totalPath = TotalPath;
        Texture2D screenTex = ScreenCapture.CaptureScreenshotAsTexture();

        // ���� ��ũ�����κ��� ���� ������ �ȼ����� �ؽ��Ŀ� ����
        bool succeeded = true;
        try
        {
            // ������ ���ٸ� �����
            if (Directory.Exists(folderPath) == false)
                Directory.CreateDirectory(folderPath);

            File.WriteAllBytes(totalPath, screenTex.EncodeToPNG());
        }
        catch (Exception e)
        {
            succeeded = false;
            Debug.LogWarning($"Screen Shot Save Failed : {totalPath}\n{e}");
        }
        Destroy(screenTex);// ���� �ؽ��� ����

        if (succeeded)
        {
            // �����ߴٸ� �ֱ� ��ο� ����
            lastSavedPath = totalPath;
        }
        RefreshAndroidGallery(totalPath);
        CaptureEffect();
    }

    private void RefreshAndroidGallery(string imageFilePath)
    {
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#else
        AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass classUri = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject objIntent = new AndroidJavaObject("android.content.Intent", new object[2]
        { "android.intent.action.MEDIA_SCANNER_SCAN_FILE", classUri.CallStatic<AndroidJavaObject>("parse", "file://" + imageFilePath) });
        objActivity.Call("sendBroadcast", objIntent);
#endif
    }

    private void CaptureEffect()
    {
        screenFade.color = Color.white;
        screenFade.DOFade(0f, 0.7f).SetEase(Ease.Flash);
    }
}
