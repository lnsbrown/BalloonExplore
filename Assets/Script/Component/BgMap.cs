using UnityEngine;

public class BgMap : MonoBehaviour
{
    // 移动速度
    private float moveSpeed = 0;

    // 观测移动中的地图
    private GameObject watchMapObject;
    private int watchMapObjectIndex;

    // 地图
    private GameObject[] mapObjects;

    // Start is called before the first frame update
    void Start()
    {
        this.Init();
    }

    void Init()
    {
        this.moveSpeed = 50;
        this.mapObjects = GameObject.FindGameObjectsWithTag("map");

        for (var index = 0; index < this.mapObjects.Length; index++)
        {
            var mapObject = this.mapObjects[index];
            if (index == 0)
            {
                // 观察第一个地图的移动情况
                this.watchMapObject = mapObject;
                this.watchMapObjectIndex = index;
            }

            var renderer = mapObject.GetComponentInChildren<Renderer>();

            float scaleX = Screen.width / 2f / renderer.bounds.size.x * 1f;
            float scaleY = Screen.height / 2f / renderer.bounds.size.y * 1f;
            var transformLocalScale = mapObject.transform.localScale;
            transformLocalScale.x = scaleX;
            transformLocalScale.y = scaleY;
            mapObject.transform.localScale = transformLocalScale;

            var transformLocalPosition = mapObject.transform.localPosition;
            transformLocalPosition.x = 0;
            transformLocalPosition.y = Screen.height * index;
            mapObject.transform.localPosition = transformLocalPosition;
        }
    }

    // Update is called once per frame
    public void Update()
    {
        RollDown();
    }

    private void RollDown()
    {
        // 计算移动举例
        var moveDistance = Time.deltaTime * this.moveSpeed;
        for (var index = 0; index < this.mapObjects.Length; index++)
        {
            var mapObject = this.mapObjects[index];
            // 移动所有地图
            MoveMap(moveDistance, mapObject);
        }

        // 是否移动到边界
        if (IsMapOverDown())
        {
            // 交换地图位置
            SwapMap();
        }
    }

    // 地图是否超出下边界
    private bool IsMapOverDown()
    {
        // y坐标小于等于屏幕，则认为超出边界
        return this.watchMapObject.transform.localPosition.y <= -Screen.height;
    }

    private void SwapMap()
    {
        int allLen = this.mapObjects.Length;

        // 移动地图
        var transformLocalPosition = this.watchMapObject.transform.localPosition;
        transformLocalPosition.y = Screen.height * (allLen - 1);
        this.watchMapObject.transform.localPosition = transformLocalPosition;

        // 下一个观察的地图对象
        int nextIndex = this.watchMapObjectIndex + 1;
        if (nextIndex >= allLen)
        {
            nextIndex = 0;
        }

        // 转移到下一个观察目标
        this.watchMapObject = this.mapObjects[nextIndex];
        this.watchMapObjectIndex = nextIndex;
    }

    private void MoveMap(float moveDistance, GameObject mapObject)
    {
        var localPosition = mapObject.transform.localPosition;
        localPosition.y -= moveDistance;
        mapObject.transform.localPosition = localPosition;
    }
}