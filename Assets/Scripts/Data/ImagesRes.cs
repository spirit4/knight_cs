using Assets.Scripts.Core;
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

        public const string DECOR = "Decor";//???????????????????flovers

        //animations
        //public const string A_HERO_IDLEs: Object[];
        //public const string A_HERO_MOVEs: Object[];
        //public static A_HERO_DEATHs: Object[];
        //public const string A_ITEMs: Object;   // 0-helm, 1-shield, 2-sword
        //public const string A_MONSTER: Object;
        //public const string A_SMOKE string = "smoke";
        public const string A_BOOM = "Boom";
        public const string A_ATTACK_BOOM = "BoomSword";
        //public const string A_TRAP string = "trap";

        public static readonly Dictionary<string, float> numberImages = new Dictionary<string, float>();
        public static Dictionary<string, Sprite> tileSprites = new Dictionary<string, Sprite>(); //loader: createjs.LoadQueue;
        public static Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>(); //loader: createjs.LoadQueue;

        public static void init()
        {
            ImagesRes.numberImages[ImagesRes.GRASS] = 5.0f;
            ImagesRes.numberImages[ImagesRes.WATER] = 3.0f;

            try
            {
                Sprite[] sprites = Resources.LoadAll<Sprite>("images/tiles");
                //Debug.Log(sprites.Length);
                foreach (var s in sprites)
                {
                    tileSprites.Add(s.name, s);
                    //Debug.Log("[try] " + s.name);
                }
                Sprite gameEnd = Resources.Load<Sprite>("images/ui/game_end");
                tileSprites.Add("game_end", gameEnd);//easy hack


                sprites = Resources.LoadAll<Sprite>("images/ui/Achs");
                foreach (var s in sprites)
                {
                    tileSprites.Add(s.name, s);
                }

                GameObject[] clips = Resources.LoadAll<GameObject>("Prefabs");
                //Debug.Log(clips.Length);
                foreach (var prefab in clips)
                {
                    prefabs.Add(prefab.name, prefab);
                    //Debug.Log("[try] " + prefab.name);
                }
            }
            catch (Exception e)
            {
                Debug.Log("Loading failed with the following exception: ");
                Debug.Log(e);
            }
        }



        /** <summary>type0 or type_0 are okay</summary> */
        public static Sprite getImage(string name)
        {
            Sprite bd = null;
            int index;
            string digit = Regex.Match(name, @"\d+").Value;

            if (name.StartsWith(DECOR))
            {
                return tileSprites["flovers_" + digit]; //artist's spelling
            }

            if (ImagesRes.numberImages.ContainsKey(name) && ImagesRes.numberImages[name] > 0)
            {
                index = (int)Random.Range(0, ImagesRes.numberImages[name]);
                //Debug.Log("[getImage] " + name + '_' + index);
                bd = tileSprites[name + '_' + index];
            }
            else if (ImagesRes.tileSprites.ContainsKey(name))
            {
                bd = tileSprites[name];
                //Debug.Log("[getImage]!!!!!!!!!!!!!!!!!!!!!!!!: [" + name + "]");
            }
            else if (!ImagesRes.tileSprites.ContainsKey(name))
            {

                try
                {
                    index = int.Parse(digit);      //checking if there is actually an index in the string
                    bd = tileSprites[name.Substring(0, name.LastIndexOf(digit)) + '_' + digit];
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
