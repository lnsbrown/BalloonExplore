using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using Script.Object;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    private PlayerUnit mainPlayer;

    // Start is called before the first frame update
    void Start()
    {
        var sceneManager = GameCore.GetInstance().GetManager<SceneManager>();
        mainPlayer = sceneManager.gameScene.mainPlayer;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("OnTriggerEnter2D" + col.gameObject.name);
    }
}