using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public static int w = 10;
    public static int h = 20;

    //数据结构
    public static Transform[,] grid = new Transform[w, h];

    public static Vector2 roundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    //保证每个被检查的位置不小于左边框，并不大于右边框，不小于最小的Y
    public static bool insideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 &&
            (int)pos.x < w &&
            (int)pos.y >= 0
            );
    }

    public static bool isRowFull(int y)
    {
        //检查某一行的每一个单元是否为空，如果有一个是空的，那么该行还没有满
        for (int x = 0; x < w; ++x)
            if (grid[x, y] == null)
                return false;
        return true;
    }

    public static void deleteRow(int y)
    {
        //删除某一行的所有数据
        for(int x = 0; x< w;++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public static void decreaseRow(int y)
    {
        //1、复制该行的数据到下一行
        //2、清空该行数据
        //3、视觉上的，改变原来的方块的位置 (Y + (-1))

        for(int x = 0; x<w;++x)
        {
            if(grid[x,y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public static void decreaseRowAbove(int y)
    {
        for (int i = y; i < h; i++)
            decreaseRow(i);
    }

    public static void deleteFullRows()
    {
        for(int y = 0;y<h;)
        {
            if (isRowFull(y))
            {
                deleteRow(y);

                decreaseRowAbove(y + 1);
            }
            else
                y++;
        }
    }
}
