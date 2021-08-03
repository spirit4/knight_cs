using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public const string MAIN_MENU  = "ButtonPlay_MainMenu";
    public const string LEVELS = "ButtonBack_Levels";
    //public const GAME: string = "game";
    //public const CREDITS: string = "credits";
    //public const INTRO: string = "intro";
    //public const ACHS: string = "achs";

    private GameObject LevelsPanel;


    // Start is called before the first frame update
    void Start()
    {
        LevelsPanel = GameObject.Find("LevelsPanel");
        LevelsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoTo(Button button)
    {
       
        Debug.Log(button.gameObject);
        //switch (button.name)
        //{
        //    case UIManager.MAIN_MENU:
        //        LevelsPanel = GameObject.Find("Panel_MainMenu");
        //        LevelsPanel.SetActive(false);
        //        LevelsPanel = GameObject.Find("LevelsPanel");
        //        break;
            //case View.CREDITS:
            //    this._currentView = new Credits();
            //    break;
            //case UIManager.LEVELS:
            //    LevelsPanel = GameObject.Find("LevelsPanel");
            //    LevelsPanel.SetActive(false);
            //    LevelsPanel = GameObject.Find("Panel_MainMenu");
            //    break;
                //case View.INTRO:
                //    this._currentView = new Intro();
                //    break;
                //case View.ACHS:
                //    this._currentView = new Achievements();
                //    break;
                //default:
                //console.log("need addView", type);
        //}
        //LevelsPanel.SetActive(true);
    }
}
