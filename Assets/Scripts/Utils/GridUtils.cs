using Assets.Scripts.Core;
using Assets.Scripts.Data;
using Assets.Scripts.Units;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class GridUtils
    {

        public static int GetIndex(float localX, float localY)
        {
            int cellX = (int)((localX + Config.SIZE_W / 2) / Config.SIZE_W);
            int cellY = (int)((localY - Config.SIZE_H / 2) / Config.SIZE_H);
            return -cellY * Config.WIDTH + cellX;
        }

        public static Vector2 GetUnityPoint(int x, int y)
        {
            float cellX = Config.SIZE_W * x / Config.PIXEL_SIZE;
            float cellY = -Config.SIZE_H * y / Config.PIXEL_SIZE;
            return new Vector2(cellX, cellY);
        }

        public static Vector2 GetPoint(int i)
        {
            float cellY = (int)(i / Config.WIDTH) * Config.SIZE_W;
            float cellX = (i - (int)(i / Config.WIDTH) * Config.WIDTH) * Config.SIZE_H;
            return new Vector2(cellX, -cellY);
        }

        public static int FindAround(Tile[] grid, int index, string type)
        {
            if (index + 1 < Config.WIDTH * Config.HEIGHT && grid[index + 1].IsContainType(type))
            {
                return index + 1;
            }
            else if (index - 1 >= 0 && grid[index - 1].IsContainType(type))
            {
                return index - 1;
            }
            else if (index + Config.WIDTH < Config.WIDTH * Config.HEIGHT && grid[index + Config.WIDTH].IsContainType(type))
            {
                return index + Config.WIDTH;
            }
            else if (index - Config.WIDTH >= 0 && grid[index - Config.WIDTH].IsContainType(type))
            {
                return index - Config.WIDTH;
            }

            return -1;
        }

        public static Tile FindDirection(Tile[] grid, int index)
        {
            if (index + 1 < Config.WIDTH * Config.HEIGHT && grid[index + 1].IsContainDirection())
            {
                return grid[index + 1];
            }
            else if (index - 1 >= 0 && grid[index - 1].IsContainDirection())
            {
                return grid[index - 1];
            }
            else if (index + Config.WIDTH < Config.WIDTH * Config.HEIGHT && grid[index + Config.WIDTH].IsContainDirection())
            {
                return grid[index + Config.WIDTH];
            }
            else if (index - Config.WIDTH >= 0 && grid[index - Config.WIDTH].IsContainDirection())
            {
                return grid[index - Config.WIDTH];
            }

            return null;
        }
    }

}
