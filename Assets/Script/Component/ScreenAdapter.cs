using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAdapter : MonoBehaviour
{
    private const float DEFAULT_RATIO = 9f / 16f;
    private int m_ScreenWidth;
    private Camera m_Camera;
    private const float DEFAULT_SIZE = 4.5f;

    private void Start()
    {
        m_Camera = Camera.main;

        // 设置摄像机和屏幕一样大
        m_Camera.orthographicSize = Screen.height / 2f;

        m_ScreenWidth = Screen.width;
    }

    private void LateUpdate()
    {
        if (m_ScreenWidth != Screen.width)
        {
            m_ScreenWidth = Screen.width;

            float width = Screen.width;
            float height = Screen.height;
            float ratio = width / height;
            if (ratio > DEFAULT_RATIO)
            {
                if (m_Camera.orthographicSize != DEFAULT_SIZE)
                    m_Camera.orthographicSize = DEFAULT_SIZE;
            }
            else
            {
                m_Camera.orthographicSize = DEFAULT_SIZE * (DEFAULT_RATIO) / (width / height);
            }
        }
    }
}