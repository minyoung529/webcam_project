using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceSticker : MonoBehaviour
{
    protected FaceController faceController;

    public void Init(FaceController faceController)
    {
        this.faceController = faceController;
        ChildInit();
    }

    public virtual void ChildInit() { }
    public virtual void ChildDestroyed() { }

    private void OnDestroy()
    {
        ChildDestroyed();
    }
}
