using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSize : MonoBehaviour
{
    //public float x;
    //public float y;

    //private CanvasScaler can;

    //void Start()
    //{
    //    can = GetComponent<CanvasScaler>();
    //    setinfo();
    //}

    //void setinfo()
    //{
    //    x = (float)Screen.currentResolution.width;
    //    y = (float)Screen.currentResolution.height;

    //    can.referenceResolution = new Vector2(x, y);
    //}

    /////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Reference Resolution like 1920x1080
    /// </summary>
    public Vector2 ReferenceResolution;

    /// <summary>
    /// Zoom factor to fit different aspect ratios
    /// </summary>
    public Vector3 ZoomFactor = Vector3.one;

    /// <summary>
    /// Design time position
    /// </summary>
    [HideInInspector]
    public Vector3 OriginPosition;

    /// <summary>
    /// Start
    /// </summary>
    void Start()
    {
        OriginPosition = transform.position;
    }

    /// <summary>
    /// Update per Frame
    /// </summary>
    void Update()
    {

        if (ReferenceResolution.y == 0 || ReferenceResolution.x == 0)
            return;

        var refRatio = ReferenceResolution.x / ReferenceResolution.y;
        var ratio = (float)Screen.width / (float)Screen.height;

        transform.position = OriginPosition + transform.forward * (1f - refRatio / ratio) * ZoomFactor.z
                                            + transform.right * (1f - refRatio / ratio) * ZoomFactor.x
                                            + transform.up * (1f - refRatio / ratio) * ZoomFactor.y;


    }


}

