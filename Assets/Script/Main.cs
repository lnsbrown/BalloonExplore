using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

// 游戏主场景入口
public class Main : MonoBehaviour
{
    private static readonly GameCore _gameCore = new GameCore();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Game Main");
        _gameCore.Init();
    }

    // Update is called once per frame
    void Update()
    {
        _gameCore.Update();
    }
}