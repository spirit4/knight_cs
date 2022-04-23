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
        //animations
        public const string A_BOOM = "Boom";
        public const string A_ATTACK_BOOM = "BoomSword";

        public static readonly Dictionary<string, float> NumberImages = new Dictionary<string, float>();
        public static Dictionary<string, Sprite> TileSprites = new Dictionary<string, Sprite>(); 

        public static void Init()
        {

            try
            {
                Sprite[] sprites = Resources.LoadAll<Sprite>("Images/GameScene/UI/ForLoad");

                foreach (var s in sprites)
                {
                    TileSprites.Add(s.name, s);
                }
                Sprite gameEnd = Resources.Load<Sprite>("Images/GameScene/game_end");
                TileSprites.Add("game_end", gameEnd);//TODO do it proper way


                sprites = Resources.LoadAll<Sprite>("Images/MainScene/Achievements");
                foreach (var s in sprites)
                {
                    TileSprites.Add(s.name, s);
                }
                sprites = Resources.LoadAll<Sprite>("Images/Both/AchIcons");//TODO fix all
                foreach (var s in sprites)
                {
                    TileSprites.Add(s.name, s);
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
