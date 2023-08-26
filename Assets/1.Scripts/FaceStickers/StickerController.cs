using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerController : MonoBehaviour
{
    [SerializeField]
    private FaceSticker[] stickerPrefabs;

    private FaceSticker curSticker;

    private int index = 0;

    [SerializeField]
    private Transform root;

    [SerializeField]
    private FaceController faceController;

    void Start()
    {
        curSticker = CreateSticker(0);
        transform.SetParent(null);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) // <
        {
            ChangeSticker((index - 1 + stickerPrefabs.Length) % stickerPrefabs.Length);
        }

        else if (Input.GetKeyDown(KeyCode.D)) // >
        {
            ChangeSticker((index + 1 + stickerPrefabs.Length) % stickerPrefabs.Length);
        }
    }

    private void ChangeSticker(int index)
    {
        if (this.index == index)
            return;

        Destroy(curSticker.gameObject);
        curSticker = CreateSticker(index);
        this.index = index;
    }

    private FaceSticker CreateSticker(int index)
    {
        FaceSticker newSticker = Instantiate(stickerPrefabs[index], root);
        newSticker.Init(faceController);
        return newSticker;
    }
}
