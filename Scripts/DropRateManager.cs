using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//控制掉率，挂载到怪物和可破坏物的prefab上
public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drops //定义掉落物基本参数
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;
    }

    public List<Drops> dropsList; //dropsList包含所有可能的掉落物，比如蓝宝石、红宝石

    bool quitting;

    void OnApplicationQuit() 
    {
        quitting = true;
    }

    //退出场景的时候也会调用OnDestroy，解决方法是通过OnApplicationQuit来控制一个quitting变量，然后通过检查这个变量来决定OnDestroy做什么
    void OnDestroy() 
    {
        if (!quitting)
        {
            Debug.Log(gameObject + " is Dropping.");
            RandomDrop();
        }
    }

    void RandomDrop()
    {
        //0-100随机数
        float randomNumber = UnityEngine.Random.Range(0f, 100f);
        //totalRate初始值为0，用来与randomNumber比较大小，决定生成的掉落物
        float totalRate = 0;
        //dropsCount为dropList中掉落物的个数
        int dropsCount = dropsList.Count;

        //比较randomNumber与totalRate大小，大于则累加编号i的概率并继续循环，小于则生成编号i掉落物并结束循环
        //这样做可以使掉落物的期望符合其概率
        for (int i = 0; i < dropsCount; i++)
        {
            if (randomNumber > totalRate)
            {
                if (i == dropsCount - 1)
                {
                    Instantiate(dropsList[i].itemPrefab, transform.position, Quaternion.identity);
                    break;
                }
                totalRate += dropsList[i].dropRate;
            }
            else
            {
                Instantiate(dropsList[i-1].itemPrefab, transform.position, Quaternion.identity);
                break;
            }
        }
    }
}
