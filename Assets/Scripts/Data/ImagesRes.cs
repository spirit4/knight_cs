using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Data
{
    public static class ImagesRes
    {
        //tiles
        public const string HERO = "hero";
        public const string GRASS = "grass";
        public const string STAR = "star";   //helm, shield, sword
        public const string EXIT = "exit";
        public const string MILL = "mill";
        public const string MILL_VANE  = "mill_paddle_game";
        public const string WATER = "water";
        public const string PINE = "pine";
        public const string STONE = "stone";
        public const string STUMP = "stump";
        public const string MONSTER = "wolwpig";
        public const string MONSTER_ANIMATION = "Wolf";
        public const string BRIDGE = "brige";
        public const string TOWER  = "tower";
        public const string ARROW  = "arrow";
        public const string SPIKES  = "spikes";
        public const string TRAP = "trap";
        public const string BOULDER  = "boulder";
        public const string BOULDER_MARK  = "boulderMark";
        public const string TARGET_MARK = "target_";

        public const string ICON_ACH = "a";
        public const string DECOR = "Decor";// ====="flovers" spelling from artist

        //animations
        public const string A_BOOM = "Boom";
        public const string A_ATTACK_BOOM = "BoomSword";

        public static readonly Dictionary<string, float> NumberImages = new Dictionary<string, float>();
        public static Dictionary<string, Sprite> TileSprites = new Dictionary<string, Sprite>(); 
        public static Dictionary<string, GameObject> Prefabs = new Dictionary<string, GameObject>(); 

        public static void Init()
        {
            ImagesRes.NumberImages[ImagesRes.GRASS] = 5.0f;
            ImagesRes.NumberImages[ImagesRes.WATER] = 3.0f;

            try
            {
                Sprite[] sprites = Resources.LoadAll<Sprite>("Images/Tiles");

                foreach (var s in sprites)
                {
                    TileSprites.Add(s.name, s);
                }
                Sprite gameEnd = Resources.Load<Sprite>("Images/UI/game_end");
                TileSprites.Add("game_end", gameEnd);//TODO do it proper way


                sprites = Resources.LoadAll<Sprite>("Images/UI/Achievements");
                foreach (var s in sprites)
                {
                    TileSprites.Add(s.name, s);
                }

                GameObject[] clips = Resources.LoadAll<GameObject>("Prefabs");
                
                foreach (var prefab in clips)
                {
                    Prefabs.Add(prefab.name, prefab);
                }
            }
            catch (Exception e)
            {
                Debug.Log("Loading failed with the following exception: ");
                Debug.Log(e);
            }
        }

        /** <summary>type0 or type_0 are okay</summary> */
        public static Sprite GetImage(string name)
        {
            Sprite bd = null;
            int index;
            string digit = Regex.Match(name, @"\d+").Value;

            if (name.StartsWith(DECOR))
            {
                return TileSprites["flovers_" + digit]; //artist's spelling
            }

            if (ImagesRes.NumberImages.ContainsKey(name) && ImagesRes.NumberImages[name] > 0)
            {
                index = (int)Random.Range(0, ImagesRes.NumberImages[name]);
                bd = TileSprites[name + '_' + index];
            }
            else if (ImagesRes.TileSprites.ContainsKey(name))
            {
                bd = TileSprites[name];
            }
            else if (!ImagesRes.TileSprites.ContainsKey(name))
            {

                try
                {
                    index = int.Parse(digit);      //checking if there is actually an index in the string
                    bd = TileSprites[name.Substring(0, name.LastIndexOf(digit)) + '_' + digit];
                }
                catch (Exception e)
                {
                    Debug.Log("[getImage] THERE is NO item yet: [" + name + "]");
                    Debug.Log(e);
                }
            }
            return bd;
        }
    }


}
