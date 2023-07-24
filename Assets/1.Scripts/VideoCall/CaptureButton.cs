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
/// 캡쳐 버튼 (캡쳐해서 갤러리 또는 폴더에 저장)
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

    
    // 안드로이드 : 저장소 권한 확인하고 요청하기
    private void CheckPermission(string permission)
    {
        if (Permission.HasUserAuthorizedPermission(permission) == false)
        {
            PermissionCallbacks pCallbacks = new PermissionCallbacks();
            // 승인
            pCallbacks.PermissionGranted += _ => StartCoroutine(TakePicture());
            // 거절
            pCallbacks.PermissionDenied += str => Debug.Log($"거절");
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

        // 현재 스크린으로부터 지정 영역의 픽셀들을 텍스쳐에 저장
        bool succeeded = true;
        try
        {
            // 폴더가 없다면 만들기
            if (Directory.Exists(folderPath) == false)
                Directory.CreateDirectory(folderPath);

            File.WriteAllBytes(totalPath, screenTex.EncodeToPNG());
        }
        catch (Exception e)
        {
            succeeded = false;
            Debug.LogWarning($"Screen Shot Save Failed : {totalPath}\n{e}");
        }
        Destroy(screenTex);// 만든 텍스쳐 삭제

        if (succeeded)
        {
            // 성공했다면 최근 경로에 저장
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
