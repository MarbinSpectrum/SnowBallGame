using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam;

    private void Update()
    {
        if (vcam == null)
            return;

        if (Screen.width >= Screen.height)
        {
            float rate = (1280.0f / Screen.width);
            vcam.m_Lens.OrthographicSize = 7 * rate;
        }
        else
        {
            float rate = (720.0f / Screen.height);
            vcam.m_Lens.OrthographicSize = 7 * rate;
        }
    }

}
