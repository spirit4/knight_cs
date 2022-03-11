using Assets.Scripts.Data;

namespace Assets.Scripts.Core
{
    public class Controller
    {
        public ManagerBg bg;
        public Model model;

        public static Controller instance;
        //    private _isRestarted: boolean = false;

        //    public ga: any;
        //    public api: any;

        public Controller(ManagerBg bg)
        {
            Controller.instance = this;

            //this.ga = window["gaTracker"];
            //Core.instance.api = null;//window["famobi"];

            this.bg = bg;
            Init();
        }

        private void Init()
        {
            this.model = new Model();

            JSONRes.Init();

            //        if (this.ga)
            //        {
            //        Core.instance.ga.send('pageview', "/InitMainMenu");
        }

        //        if (Core.instance.api)
        //        {
        //        GameBranding.adsPause();
        //        Core.instance.api.showAd(this.goAfterAds);
        //    }

        //private setLevel(level string): void
        //{
        //    if (level != "")
        //    {
        //        _isRestarted = false;
        //        Progress.currentLevel = +level;  //parse in Level
        //    }
        //    else
        //    {
        //        //console.log("[INCORRECT LEVEL]", level);
        //    }
        //}

    }
}
