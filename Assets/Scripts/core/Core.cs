namespace Assets.Scripts.core
{
    class Core
    {
        //    //-------------------------------
        //    //public editor: Editor;
        //    public debug: Debuger;
        //    //-------------------------------

        public ManagerBg bg;
        public Model model;

        public static Core instance;

        //    private _preloader: Preloader;

        //    private _currentView: View;
        //    private _blind: Blind;

        //    private _isRestarted: boolean = false;

        //    public ga: any;
        //    public api: any;

        public Core(ManagerBg bg)
        {
            Core.instance = this;
            //this.snapToPixel = true;

            //this.ga = window["gaTracker"];
            //Core.instance.api = null;//window["famobi"];

            this.bg = bg;
            //this.name = "core";

            //this._preloader = new Preloader();
            //this.addChild(this._preloader);
            //this._preloader.on(GameEvent.COMPLETE, this.init, this);

            //console.log("[core init]");
            init();
        }

        private void init()//e: GameEvent) : void
        {
            //console.log("[preloader loaded]");

            //this._preloader.removeAllEventListeners();
            this.model = new Model();

            //ImagesRes.init();
            //        this._preloader.initProgress();
            //        this._preloader.on(GameEvent.COMPLETE, this.initCompleted, this);
        }

        //    private initCompleted() : void
        //    {
        //        JSONRes.init();
        //        ImagesRes.initAnimations();
        //        this.bg.init();

        //        //-------------------------------
        //        //this.editor = new Editor(this.model.grid);
        //        //this.editor.on(GameEvent.EDITOR_ON_OFF, this.onOffHandler, this);
        //        //this.addChild(this.editor);
        //        //-------------------------------

        //        if (this.ga)
        //        {
        //        Core.instance.ga.send('pageview', "/InitMainMenu");
        //    }

        //    //window.addEventListener(GUIEvent.KEYDOWN, this.editor.keyHandler.bind(this.editor));

        //    var sm: SoundManager = new SoundManager();//singleton
        //        this.model.loadProgress();

        //        var ac: AchController = new AchController();//singleton
        //        ac.init(this);

        //     //console.log("[adding first screen]");

        //        //this.startGame();
        //        this.addView(View.MAIN_MENU);
        //    SoundManager.instance.setLocation(SoundManager.MUSIC_MENU);

        //        //remove preloader
        //        this._preloader.removeAllEventListeners();
        //        this.removeChild(this._preloader);
        //        this._preloader = null;
        //    }

        //    //private onOffHandler(e: GameEvent): void
        //    //{
        //    //    this.editor.visible = !this.editor.visible;

        //    //    if (this._currentView)
        //    //    {
        //    //        this.removeView();
        //    //    }
        //    //    else
        //    //    {
        //    //        this.startGame();
        //    //    }
        //    //}

        //    private restartHandler(e: GameEvent) : void
        //    {
        //        e.stopPropagation();

        //     //console.log("[restart game]");

        //        this._isRestarted = true;

        //        this.addBlind();
        //    }

        //    private completeHandler(e: GameEvent) : void
        //    {
        //        Progress.currentLevel++;
        //     //console.log("[complete game] new lvl:", Progress.currentLevel);

        //        if (!this._isRestarted)
        //        {
        //        AchController.instance.addParam(AchController.LEVEL_WITHOUT_DEATH);
        //    }

        //        this._isRestarted = false;


        //        if (Core.instance.api)
        //        {
        //        GameBranding.adsPause();
        //        Core.instance.api.showAd(this.goAfterAds);
        //    }
        //        else
        //        {
        //        this.addBlind();
        //    }
        //    }

        //    private goAfterAds() : void
        //    {
        //        GameBranding.adsResume();
        //        Core.instance.addBlind();
        //    }

        //private setLevel(level: string): void
        //{
        //    if (level != "")
        //    {
        //        this._isRestarted = false;
        //        Progress.currentLevel = +level;  //parse in Level
        //    }
        //    else
        //    {
        //        //console.log("[INCORRECT LEVEL]", level);
        //    }
        //}

        //private startGame(e: GUIEvent = null): void
        //{
        //    var game: Game = new Game();
        //    this.addChildAt(game, this.getNumChildren() - 1); // under blind
        //    game.createHint();
        //    game.on(GameEvent.RESTART, this.restartHandler, this);
        //    game.on(GameEvent.LEVEL_COMPLETE, this.completeHandler, this);

        //    this._currentView = game;
        //    game.on(GUIEvent.GOTO_WINDOW, this.gotoHandler, this);
        //}

        //private addBlind(): void
        //{
        //    if (!this._blind)
        //    {
        //        this._blind = new Blind();
        //        this.addChild(this._blind);

        //        this._blind.on(GUIEvent.BLIND_CLOSED, this.removeView, this);  //clap
        //        this._blind.on(GUIEvent.BLIND_CLOSED, this.startGame, this);  //clap
        //        this._blind.on(GameEvent.COMPLETE, this.removeBlind, this);
        //    }
        //    else
        //    {
        //        //console.log("WTF BLIND EXIST");
        //    }
        //}

        //private removeBlind(e: GameEvent): void
        //{
        //    this._blind.removeAllEventListeners();
        //    this.removeChild(this._blind);
        //    this._blind.destroy();
        //    this._blind = null;
        //}

        //private addView(type: string): void
        //{
        //    switch (type)
        //    {
        //        case View.MAIN_MENU:
        //            this._currentView = new MainMenu();
        //            break;
        //        case View.CREDITS:
        //            this._currentView = new Credits();
        //            break;
        //        case View.LEVELS:
        //            this._currentView = new Levels();
        //            this._currentView.on(GameEvent.GOTO_LEVEL, this.gotoLevelHandler, this);
        //            break;
        //        case View.INTRO:
        //            this._currentView = new Intro();
        //            break;
        //        case View.ACHS:
        //            this._currentView = new Achievements();
        //            break;
        //        default:
        //            //console.log("need addView", type);
        //    }
        //    this.addChild(this._currentView);

        //    this._currentView.on(GUIEvent.GOTO_WINDOW, this.gotoHandler, this);
        //}

        //private removeView(e:GUIEvent = null): void
        //{
        //    if (this._currentView)
        //    {
        //        this.removeChild(this._currentView);
        //        this._currentView.removeAllEventListeners();
        //        this._currentView.destroy();
        //        this._currentView = null;
        //    }
        //}

        //private gotoHandler(e: GUIEvent): void
        //{
        //    if (this._currentView instanceof Game)
        //        {
        //        SoundManager.instance.setLocation(SoundManager.MUSIC_MENU);
        //    }

        //    this.removeView();

        //    if (e.window == View.GAME)
        //    {
        //        SoundManager.instance.setLocation(SoundManager.MUSIC_GAME);
        //        this.addBlind();
        //    }
        //    else
        //    {
        //        this.addView(e.window);
        //    }
        //}

        //private gotoLevelHandler(e: GameEvent): void
        //{
        //    this.removeView();

        //    if (e.objectType != "0")
        //    {
        //        SoundManager.instance.setLocation(SoundManager.MUSIC_GAME);
        //        this.setLevel(e.objectType);
        //        this.addBlind();
        //    }
        //    else
        //    {
        //        Progress.currentLevel = 0;
        //        this.addView(View.INTRO);
        //    }
        //}

        //public update(): void
        //{
        //    if (this._currentView instanceof Game)
        //        {
        //        (< Game > this._currentView).update();//add straight link 
        //    }
        //}
    }
}
