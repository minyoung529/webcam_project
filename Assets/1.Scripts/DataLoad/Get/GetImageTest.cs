using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetImageTest : MonoBehaviour
{
    private RawImage image;

    private void Awake()
    {
        image= GetComponent<RawImage>();
    }

    [ContextMenu("Test")]
    private void Test()
    {
        GetURLFile.SetTexture(image, "https://photo.newsen.com/news_photo/2021/09/07/202109071153412110_2.jpg");
    }

}
