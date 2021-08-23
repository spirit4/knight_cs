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
        //        //tiles
        //        static NONE string = "none";
        public const string HERO = "hero";
        public const string GRASS = "grass";
        public const string STAR  = "star";   //helm, shield, sword
        public const string EXIT = "exit";
        //public const string MILL string = "mill";
        //public const string MILL_VANE string = "millVane";
        public const string WATER = "water";
        public const string PINE = "pine";
        public const string STONE = "stone";
        public const string STUMP = "stump";
        public const string MONSTER = "wolwpig";
        public const string MONSTER_ANIMATION = "Wolf";
        public const string BRIDGE = "brige";
        //public const string TOWER string = "tower";
        //public const string ARROW string = "arrow";
        //public const string SPIKES string = "spikes";
        //public const string TRAP string = "trap";
        //public const string BOULDER string = "boulder";
        //public const string BOULDER_MARK string = "boulderMark";
        //public const string TARGET_MARK string = "targetMark";

        public const string DECOR = "Decor";//???????????????????flovers

        //    //gui
        //public const string MAIN_BG string = "MainBack";   //in preloader
        //public const string GAME_BG string = "GameBack";
        //public const string LEVELS_BG string = "LevelBack";

        //public const string PRELOADER_BAR_BG string = "preloaderBarBg";    //in preloader
        //public const string PRELOADER_BAR string = "preloaderBar";    //in preloader
        //public const string SPEAR_UI string = "spearUI";        //in preloader (2)

        //public const string INTRO_ICON string = "intro";

        //public const string UI_MILL string = "UImill";
        //public const string UI_STICK string = "UIstick";
        //public const string UI_COURSE string = "UIcourse";
        //public const string UI_STAR_ON string = "UIstarOn";
        //public const string UI_STAR_OFF string = "UIstarOff";
        //public const string UI_MILL_VANE string = "UImillVane";
        //public const string UI_LEVEL_BOARD string = "UILevelBoard";

        //    //public static UI_FAMOBI_MORE string = "UIFamobiMoreGames";

        //public const string MAIN_TITLE string = "MainLogo";   //in preloader
        //public const string CREDITS_TITLE string = "Credits";
        //public const string LEVEL_SELECT_TITLE string = "SelectLevel";
        //public const string ACHIEVEMENS_TITLE string = "Achievemens";
        //public const string VICTORY_MID string = "victoryMid";
        //public const string FINISH_MID string = "finishMid";
        //public const string VICTORY_TOP string = "victoryTop";
        //public const string VICTORY_BOTTOM string = "victoryBottom";

        //public const string ICON_ACH string = "IconAch";
        //public const string TEXT_ACH string = "textAch";

        //public const string HELP string = "help";

        //public const string BUTTON_CREDITS string = "button_credits_main_main_state_0";
        //public const string BUTTON_CREDITS_OVER string = "button_credits_main_main_state_01";
        //    //public static BUTTON_MORE_GAME string = "botton_joystick_game_state_0";
        //    //public static BUTTON_MORE_OVER_GAME string = "botton_joystick_game_state_01";
        //public const string BUTTON_MORE_MAIN string = "botton_joystick_game_state_02";
        //public const string BUTTON_MORE_OVER_MAIN string = "botton_joystick_game_state_012";
        //public const string BUTTON_PLAY_MAIN string = "button_play_main_state_0";
        //public const string BUTTON_PLAY_OVER_MAIN string = "button_play_main_state_01";
        //public const string BUTTON_PLAY_LC string = "button_play_main_state_02";
        //public const string BUTTON_PLAY_OVER_LC string = "button_play_main_state_012";
        //public const string BUTTON_ACH string = "button_achievments_main_state_0";
        //public const string BUTTON_ACH_OVER string = "button_achievments_main_state_01";
        //public const string BUTTON_SOUND_ON string = "button_sound_on_main_state_0";
        //public const string BUTTON_SOUND_ON_OVER string = "button_sound_on_main_state_01";
        //public const string BUTTON_SOUND_OFF string = "button_sound_off_main_state_0";
        //public const string BUTTON_SOUND_OFF_OVER string = "button_sound_off_main_state_01";
        //public const string BUTTON_BACK string = "button_back_levels_state_0";
        //public const string BUTTON_BACK_OVER string = "button_back_levels_state_01";
        //public const string BUTTON_LS string = "desk_level_state_0";
        //public const string BUTTON_LS_OVER string = "desk_level_state_01";
        //public const string BUTTON_MENU string = "button_levels_level_complete_state_0";
        //public const string BUTTON_MENU_OVER string = "button_levels_level_complete_state_01";
        //public const string BUTTON_RESET string = "button_replay_level_complete_state_0";
        //public const string BUTTON_RESET_OVER string = "button_replay_level_complete_state_01";

        //    //animations
        //public const string A_HERO_IDLEs: Object[];
        //public const string A_HERO_MOVEs: Object[];
        //    //public static A_HERO_DEATHs: Object[];
        //public const string A_ITEMs: Object;   // 0-helm, 1-shield, 2-sword
        //public const string A_MONSTER: Object;
        //public const string A_SMOKE string = "smoke";
        //public const string A_BOOM string = "boom";
        //public const string A_ATTACK_BOOM string = "boom_sword";
        //public const string A_TRAP string = "trap";

        public static readonly Dictionary<string, float> numberImages = new Dictionary<string, float>();
        public static Dictionary<string, Sprite> tileSprites = new Dictionary<string, Sprite>(); //loader: createjs.LoadQueue;
        public static Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>(); //loader: createjs.LoadQueue;

        public static void init()
        {
            ImagesRes.numberImages[ImagesRes.GRASS] = 5.0f;
            ImagesRes.numberImages[ImagesRes.WATER] = 3.0f;

            //    var manifest = [
            //            { src: "images/ui/help1.png", id: ImagesRes.HELP + "0" },
            //            { src: "images/ui/help2.png", id: ImagesRes.HELP + "1" },
            //            { src: "images/ui/help3.png", id: ImagesRes.HELP + "2" },
            //            { src: "images/ui/help4.png", id: ImagesRes.HELP + "3" },
            //            { src: "images/ui/help5.png", id: ImagesRes.HELP + "4" },

            //            { src: "images/ui/select_level.png", id: ImagesRes.LEVEL_SELECT_TITLE },
            //            { src: "images/ui/Achievemens.png", id: ImagesRes.ACHIEVEMENS_TITLE },
            //            { src: "images/ui/levelcomplete_center.png", id: ImagesRes.VICTORY_MID },
            //            { src: "images/ui/game_end.png", id: ImagesRes.FINISH_MID },
            //            { src: "images/ui/levelcomplete_head.png", id: ImagesRes.VICTORY_TOP },
            //            { src: "images/ui/levelcomplete_foot.png", id: ImagesRes.VICTORY_BOTTOM },
            //            { src: "images/ui/credits.png", id: ImagesRes.CREDITS_TITLE },

            //            { src: "images/ui/level_back.png", id: ImagesRes.LEVELS_BG },
            //            { src: "images/ui/back_game.png", id: ImagesRes.GAME_BG },//main in preloader
            //            { src: "images/ui/spear_0.png", id: ImagesRes.SPEAR_UI + "0" },
            //            { src: "images/ui/spear_1.png", id: ImagesRes.SPEAR_UI + "1" },
            //            { src: "images/ui/intro1.png", id: ImagesRes.INTRO_ICON + "0" },
            //            { src: "images/ui/intro2.png", id: ImagesRes.INTRO_ICON + "1" },
            //            { src: "images/ui/intro3.png", id: ImagesRes.INTRO_ICON + "2" },
            //            { src: "music/menu.ogg", type: createjs.LoadQueue.SOUND, id: SoundManager.MUSIC_MENU },
            //            { src: "music/game.ogg", type: createjs.LoadQueue.SOUND, id: SoundManager.MUSIC_GAME },
            //            { src: "images/ui/mill_paddle_shadow.png", id: ImagesRes.UI_MILL },
            //            { src: "images/ui/stick.png", id: ImagesRes.UI_STICK },
            //            { src: "images/ui/direction_sign.png", id: ImagesRes.UI_COURSE },
            //            { src: "images/ui/helmet_on.png", id: ImagesRes.UI_STAR_ON + "0" },
            //            { src: "images/ui/shield_on.png", id: ImagesRes.UI_STAR_ON + "1" },
            //            { src: "images/ui/sword_on.png", id: ImagesRes.UI_STAR_ON + "2" },
            //            { src: "images/ui/helmet_off.png", id: ImagesRes.UI_STAR_OFF + "0" },
            //            { src: "images/ui/shield_off.png", id: ImagesRes.UI_STAR_OFF + "1" },
            //            { src: "images/ui/sword_off.png", id: ImagesRes.UI_STAR_OFF + "2" },
            //            { src: "images/ui/mill_paddle.png", id: ImagesRes.UI_MILL_VANE },
            //            { src: "images/ui/level_board.png", id: ImagesRes.UI_LEVEL_BOARD },
            //            //{ src: "images/branding/More_Games600x253_SimpleWhite.png", id: ImagesRes.UI_FAMOBI_MORE },

            //            { src: "images/ui/button_credits_main_main_state_0.png", id: ImagesRes.BUTTON_CREDITS },
            //            { src: "images/ui/button_credits_main_state_1.png", id: ImagesRes.BUTTON_CREDITS_OVER },
            //            //{ src: "images/ui/botton_joystick_game_state_0.png", id: ImagesRes.BUTTON_MORE_GAME },
            //            //{ src: "images/ui/botton_joystick_game_state_1.png", id: ImagesRes.BUTTON_MORE_OVER_GAME },
            //            { src: "images/ui/button_joystick_main_state_0.png", id: ImagesRes.BUTTON_MORE_MAIN },
            //            { src: "images/ui/button_joystick_main_state_1.png", id: ImagesRes.BUTTON_MORE_OVER_MAIN },
            //            { src: "images/ui/button_play_level_complete_state_0.png", id: ImagesRes.BUTTON_PLAY_LC },
            //            { src: "images/ui/button_play_level_complete_state_1.png", id: ImagesRes.BUTTON_PLAY_OVER_LC },
            //            { src: "images/ui/button_play_main_state_0.png", id: ImagesRes.BUTTON_PLAY_MAIN },
            //            { src: "images/ui/button_play_main_state_1.png", id: ImagesRes.BUTTON_PLAY_OVER_MAIN },
            //            { src: "images/ui/button_achievments_main_state_0.png", id: ImagesRes.BUTTON_ACH },
            //            { src: "images/ui/button_achievments_main_state_1.png", id: ImagesRes.BUTTON_ACH_OVER },
            //            { src: "images/ui/button_sound_on_main_state_0.png", id: ImagesRes.BUTTON_SOUND_ON },
            //            { src: "images/ui/button_sound_on_main_state_1.png", id: ImagesRes.BUTTON_SOUND_ON_OVER },
            //            { src: "images/ui/button_sound_off_main_state_0.png", id: ImagesRes.BUTTON_SOUND_OFF },
            //            { src: "images/ui/button_sound_off_main_state_1.png", id: ImagesRes.BUTTON_SOUND_OFF_OVER },
            //            { src: "images/ui/button_back_levels_state_0.png", id: ImagesRes.BUTTON_BACK },
            //            { src: "images/ui/button_back_levels_state_1.png", id: ImagesRes.BUTTON_BACK_OVER },
            //            { src: "images/ui/desk_level_state_0.png", id: ImagesRes.BUTTON_LS },
            //            { src: "images/ui/desk_level_state_1.png", id: ImagesRes.BUTTON_LS_OVER },
            //            { src: "images/ui/button_levels_level_complete_state_0.png", id: ImagesRes.BUTTON_MENU },
            //            { src: "images/ui/button_levels_level_complete_state_1.png", id: ImagesRes.BUTTON_MENU_OVER },
            //            { src: "images/ui/button_replay_level_complete_state_0.png", id: ImagesRes.BUTTON_RESET },
            //            { src: "images/ui/button_replay_level_complete_state_1.png", id: ImagesRes.BUTTON_RESET_OVER },

            //            { src: "images/ui/a1.png", id: ImagesRes.ICON_ACH + "0" },
            //            { src: "images/ui/a2.png", id: ImagesRes.ICON_ACH + "1" },
            //            { src: "images/ui/a3.png", id: ImagesRes.ICON_ACH + "2" },
            //            { src: "images/ui/a4.png", id: ImagesRes.ICON_ACH + "3" },
            //            { src: "images/ui/a5.png", id: ImagesRes.ICON_ACH + "4" },
            //            { src: "images/ui/a6.png", id: ImagesRes.ICON_ACH + "5" },
            //            { src: "images/ui/a7.png", id: ImagesRes.ICON_ACH + "6" },
            //            { src: "images/ui/a8.png", id: ImagesRes.ICON_ACH + "7" },
            //            { src: "images/ui/a9.png", id: ImagesRes.ICON_ACH + "8" },
            //            { src: "images/ui/a10.png", id: ImagesRes.ICON_ACH + "9" },
            //            { src: "images/ui/a11.png", id: ImagesRes.ICON_ACH + "10" },
            //            { src: "images/ui/a12.png", id: ImagesRes.ICON_ACH + "11" },
            //            { src: "images/ui/at1.png", id: ImagesRes.TEXT_ACH + "0" },
            //            { src: "images/ui/at2.png", id: ImagesRes.TEXT_ACH + "1" },
            //            { src: "images/ui/at3.png", id: ImagesRes.TEXT_ACH + "2" },
            //            { src: "images/ui/at4.png", id: ImagesRes.TEXT_ACH + "3" },
            //            { src: "images/ui/at5.png", id: ImagesRes.TEXT_ACH + "4" },
            //            { src: "images/ui/at6.png", id: ImagesRes.TEXT_ACH + "5" },
            //            { src: "images/ui/at7.png", id: ImagesRes.TEXT_ACH + "6" },
            //            { src: "images/ui/at8.png", id: ImagesRes.TEXT_ACH + "7" },
            //            { src: "images/ui/at9.png", id: ImagesRes.TEXT_ACH + "8" },
            //            { src: "images/ui/at10.png", id: ImagesRes.TEXT_ACH + "9" },
            //            { src: "images/ui/at11.png", id: ImagesRes.TEXT_ACH + "10" },
            //            { src: "images/ui/at12.png", id: ImagesRes.TEXT_ACH + "11" },

            //            { src: "images/tiles/hero.png", id: ImagesRes.HERO },
            //            { src: "images/tiles/monster.png", id: ImagesRes.MONSTER },
            //            { src: "images/tiles/grass_0.png", id: ImagesRes.GRASS + 0 },
            //            { src: "images/tiles/grass_1.png", id: ImagesRes.GRASS + 1 },
            //            { src: "images/tiles/grass_2.png", id: ImagesRes.GRASS + 2 },
            //            { src: "images/tiles/grass_3.png", id: ImagesRes.GRASS + 3 },
            //            { src: "images/tiles/grass_4.png", id: ImagesRes.GRASS + 4 },

            //            { src: "images/tiles/level_helmet.png", id: ImagesRes.STAR + "0" },
            //            { src: "images/tiles/level_shield.png", id: ImagesRes.STAR + "1" },
            //            { src: "images/tiles/level_sword.png", id: ImagesRes.STAR + "2" },
            //            { src: "images/tiles/mill.png", id: ImagesRes.MILL },
            //            { src: "images/tiles/mill_paddle_game.png", id: ImagesRes.MILL_VANE },
            //            { src: "images/tiles/tower.png", id: ImagesRes.TOWER },
            //            { src: "images/tiles/arrow.png", id: ImagesRes.ARROW },
            //            { src: "images/tiles/castle.png", id: ImagesRes.EXIT },
            //            { src: "images/tiles/boulder.png", id: ImagesRes.BOULDER },
            //            { src: "images/tiles/trap.png", id: ImagesRes.TRAP },
            //            { src: "images/tiles/boulder_mark.png", id: ImagesRes.BOULDER_MARK },
            //            { src: "images/tiles/spikes0.png", id: ImagesRes.SPIKES + "0"},
            //            { src: "images/tiles/spikes1.png", id: ImagesRes.SPIKES + "1"},
            //            { src: "images/tiles/pine_0.png", id: ImagesRes.PINE + "0" },
            //            { src: "images/tiles/pine_1.png", id: ImagesRes.PINE + "1" },
            //            { src: "images/tiles/pine_2.png", id: ImagesRes.PINE + "2" },
            //            { src: "images/tiles/pine_3.png", id: ImagesRes.PINE + "3" },
            //            { src: "images/tiles/stone1.png", id: ImagesRes.STONE + "0" },
            //            { src: "images/tiles/stone2.png", id: ImagesRes.STONE + "1" },
            //            { src: "images/tiles/stump.png", id: ImagesRes.STUMP },
            //            { src: "images/tiles/water_0.png", id: ImagesRes.WATER + 0 },
            //            { src: "images/tiles/water_1.png", id: ImagesRes.WATER + 1 },
            //            { src: "images/tiles/water_2.png", id: ImagesRes.WATER + 2 },
            //            { src: "images/tiles/bridge_0.png", id: ImagesRes.BRIDGE + "0" },
            //            { src: "images/tiles/bridge_1.png", id: ImagesRes.BRIDGE + "1" },

            //            { src: "images/tiles/target0.png", id: ImagesRes.TARGET_MARK + "0" },
            //            { src: "images/tiles/target1.png", id: ImagesRes.TARGET_MARK + "1" },

            //            { src: "images/tiles/flovers_0.png", id: ImagesRes.DECOR + "0" },
            //            { src: "images/tiles/flovers_1.png", id: ImagesRes.DECOR + "1" },
            //            { src: "images/tiles/flovers_2.png", id: ImagesRes.DECOR + "2" },
            //            { src: "images/tiles/flovers_3.png", id: ImagesRes.DECOR + "3" },
            //            { src: "images/tiles/flovers_4.png", id: ImagesRes.DECOR + "4" },

            //            { src: "images/animations/knight_full_15fps_0.png", id: JSONRes.ATLAS_0 },
            //            { src: "images/animations/knight_full_15fps_1.png", id: JSONRes.ATLAS_1 },
            //            { src: "images/tiles/hero.png", id: ImagesRes.HERO }   //copy
            //        ];

            //    ImagesRes.loader = new createjs.LoadQueue(false, "", true);
            //    ImagesRes.loader.installPlugin(createjs.Sound);
            //    createjs.Sound.alternateExtensions = ["m4a"];
            //    ImagesRes.loader.loadManifest(manifest);

            try
            {
                //Debug.Log("Loading with Proper Method...");

                // This is the short hand version and requires that you include the "using System.Linq;" at the top of the file.
                Sprite[] sprites = Resources.LoadAll<Sprite>("images/tiles");
                //Debug.Log(sprites.Length);
                foreach (var s in sprites)
                {
                    tileSprites.Add(s.name, s);
                    //Debug.Log("[try] " + s.name);
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
                Debug.Log("Proper Method failed with the following exception: ");
                Debug.Log(e);
            }
            //Game.sprite = Resources.Load<Sprite>("images/tiles/grass_0");
        }

        //public static initAnimations(): void
        //{
        //    ImagesRes.A_HERO_IDLEs = [
        //            { animation: "knight_stay", atlas: JSONRes.atlas0 },
        //            { animation: "knight_stay_helmet", atlas: JSONRes.atlas0 },
        //            { animation: "knight_stay_shield", atlas: JSONRes.atlas0 },
        //            { animation: "knight_stay_sword", atlas: JSONRes.atlas0 },
        //            { animation: "knight_stay_helmet_shield", atlas: JSONRes.atlas0 },
        //            { animation: "knight_stay_helmet_sword", atlas: JSONRes.atlas0 },
        //            { animation: "knight_stay_sword_shield", atlas: JSONRes.atlas0 },
        //            { animation: "knight_stay_helmet_sword_shield", atlas: JSONRes.atlas0 }
        //        ];

        //    ImagesRes.A_HERO_MOVEs = [
        //            { animation: "knight_walk", atlas: JSONRes.atlas0 },
        //            { animation: "knight_walk_helmet", atlas: JSONRes.atlas0 },
        //            { animation: "knight_walk_shield", atlas: JSONRes.atlas0 },
        //            { animation: "knight_walk_sword", atlas: JSONRes.atlas0 },
        //            { animation: "knight_walk_helmet_shield", atlas: JSONRes.atlas0 },
        //            { animation: "knight_walk_helmet_sword", atlas: JSONRes.atlas0 },
        //            { animation: "knight_walk_sword_shield", atlas: JSONRes.atlas0 },
        //            { animation: "knight_walk_helmet_sword_shield", atlas: JSONRes.atlas0 }
        //        ];

        //    //ImagesRes.A_HERO_DEATHs = [
        //    //    { animation: "knight_death", atlas: JSONRes.atlas0 },
        //    //    { animation: "knight_death_helmet", atlas: JSONRes.atlas1 },
        //    //    { animation: "knight_death_shield", atlas: JSONRes.atlas0 },
        //    //    { animation: "knight_death_sword", atlas: JSONRes.atlas0 },
        //    //    { animation: "knight_death_helmet_shield", atlas: JSONRes.atlas0 },
        //    //    { animation: "knight_death_helmet_sword", atlas: JSONRes.atlas0 },
        //    //    { animation: "knight_death_sword_shield", atlas: JSONRes.atlas0 },
        //    //    { animation: "knight_death_helmet_sword_shield", atlas: JSONRes.atlas0 },
        //    //];

        //    ImagesRes.A_MONSTER = { animation: "wolf", atlas: JSONRes.atlas1 };

        //    ImagesRes.A_ITEMs =  { star0: "helmet", star1: "shield", star2: "sword" };
        //}

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
