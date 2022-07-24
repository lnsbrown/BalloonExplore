using UnityEngine;

public class BalloonComponent : MonoBehaviour
{
    /// <summary>
    /// 从unity配置皮肤
    /// </summary>
    public Sprite[] skins;

    public SpriteRenderer balloonRender;

    // Start is called before the first frame update
    void Start()
    {
        balloonRender = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}