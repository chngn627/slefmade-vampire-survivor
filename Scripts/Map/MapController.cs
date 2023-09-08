using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    Vector3 noTerrainPosition;
    PlayerMovement pm;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOpDist; //必须大于一个chunk的长宽
    float opDist;
    float optimizerCooldown;
    public float optimizerCooldownSet;

    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker()
    {
        if (!currentChunk)
        {
            return;
        }

        if(pm.moveDirection.x > 0 && pm.moveDirection.y == 0) //右
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right").position;
                SpawnChunk();
            }
        }
        else if(pm.moveDirection.x < 0 && pm.moveDirection.y == 0) //左
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left").position;
                SpawnChunk();
            }
        }
        else if(pm.moveDirection.x == 0 && pm.moveDirection.y > 0) //上
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Up").position;
                SpawnChunk();
            }
        }
        else if(pm.moveDirection.x == 0 && pm.moveDirection.y < 0) //下
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Down").position;
                SpawnChunk();
            }
        }
        else if(pm.moveDirection.x > 0 && pm.moveDirection.y > 0) //右上
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("RightUp").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("RightUp").position;
                SpawnChunk();
            }
        }
        else if(pm.moveDirection.x < 0 && pm.moveDirection.y > 0) //左上
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("LeftUp").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("LeftUp").position;
                SpawnChunk();
            }
        }
        else if(pm.moveDirection.x > 0 && pm.moveDirection.y < 0) //右下
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("RightDown").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("RightDown").position;
                SpawnChunk();
            }
        }
        else if(pm.moveDirection.x < 0 && pm.moveDirection.y < 0) //左下
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("LeftDown").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("LeftDown").position;
                SpawnChunk();
            }
        }

    }

    void SpawnChunk()
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;
        if (optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCooldownSet;
        }
        else
        {
            return;
        }

        /**
        //直接删除距离过远的地图块
        //实现方法1
        List<GameObject> tmp = new List<GameObject>(spawnedChunks);
        foreach (GameObject chunk in tmp)
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if(opDist > maxOpDist)
            {
                spawnedChunks.Remove(chunk);
                Destroy(chunk, 0);
            }
        }
        **/

        /**
        //直接删除距离过远的地图块
        //实现方法2
        for (int index = spawnedChunks.Count - 1; index >= 0; index--)
        {
            opDist = Vector3.Distance(player.transform.position, spawnedChunks[index].transform.position);
            if(opDist > maxOpDist)
            {
                GameObject toBeDestroyed = spawnedChunks[index];
                spawnedChunks.Remove(toBeDestroyed);
                Destroy(toBeDestroyed, 0);
            }
        }
        **/


        //把距离过远的地图块关闭，可以保留完整地图，但是玩久了可能导致资源占用大
        foreach (GameObject chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if(opDist > maxOpDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }

    }
}
