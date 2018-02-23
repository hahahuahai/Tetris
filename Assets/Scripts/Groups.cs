using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Groups : MonoBehaviour
{

    float lastFall = 0;

    // Use this for initialization
    void Start()
    {
        if (!isValidGridPos())
        {
            Debug.Log("GAME OVER!");

            GameObject.Find("Canvas").GetComponent<GUIManager>().GameOver();

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //控制左移
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);

            if (isValidGridPos())
            {
                updateGrid();
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        //控制右移
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);

            if (isValidGridPos())
            {
                updateGrid();

            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        //控制旋转
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);

            if (isValidGridPos())
            {
                updateGrid();
            }
            else
            {
                transform.Rotate(0, 0, 90);
            }
        }
        //控制快速掉落
        else if (Input.GetKeyDown(KeyCode.DownArrow) ||
            Time.time - lastFall >= 1f)
        {
            transform.position += new Vector3(0, -1, 0);

            if (isValidGridPos())
            {
                updateGrid();
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);

                //已经到位，所以可以测试是否可以删除已经“满”的行
               Grid.deleteFullRows();

                FindObjectOfType<Spawner>().spawnNext();
                enabled = false;
            }


            lastFall = Time.time;
        }
    }

    bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);

            //1.判断是否在边界之内（左右下）
            if (!Grid.insideBorder(v))
            {
                return false;
            }
            //2.现在的grid对应的格子里面是null        
            if (Grid.grid[(int)v.x, (int)v.y] != null &&
                Grid.grid[(int)v.x, (int)v.y].parent != transform)
            {
                return false;
            }

        }
        return true;
    }

    void updateGrid()
    {
        //上一次的数据清理，移去原来占据的格子信息
        for(int y = 0; y < Grid.h;y++)
            for(int x = 0;x < Grid.w;x++)
            {
                if (Grid.grid[x, y] != null)
                    if (Grid.grid[x, y].parent == transform)
                        Grid.grid[x, y] = null;
            }

        //加入本次的更新的位置信息
        foreach(Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);
            Grid.grid[(int)v.x, (int)v.y] = child;
        }
    }

}
