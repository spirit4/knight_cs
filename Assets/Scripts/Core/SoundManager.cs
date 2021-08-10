using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Core
{
    public class SoundManager
    {
        //        //music
        //        static MUSIC_MENU string = 'musicMenu';
        //public const string MUSIC_GAME string = 'musicGame';

        public static SoundManager instance;

        //    //false = true;
        //    private _isSFX: boolean = true;
        //    private _isMusic: boolean = true;

        //    private _currentButton: Button;
        //    private _currentLocation string = ""; //menu or game

        //    private _musicInstances: { [index string]: createjs.AbstractSoundInstance; } = {};
        //private _musicPositions: {[index string] number; } = { };


        public SoundManager()
        {
            SoundManager.instance = this;
        }

        //public setLocation(type string): void
        //{
        //    if (this._isMusic)
        //    {
        //        if (type != this._currentLocation)
        //        {
        //            //console.log("type" + type, this.currentLocation);
        //            this.stopMusicTrack();

        //            this._musicPositions[SoundManager.MUSIC_GAME] = 0;
        //            this._musicPositions[SoundManager.MUSIC_MENU] = 0;

        //            this._currentLocation = type;
        //            this.playMusicTrack();
        //        }
        //    }
        //    else
        //    {
        //        this._currentLocation = type;
        //    }
        //}

        //private playMusicTrack(): void
        //{
        //    if (!this._musicPositions[this._currentLocation])
        //    {
        //        this._musicPositions[this._currentLocation] = 0;
        //    }

        //    this._musicInstances[this._currentLocation] = createjs.Sound.play(this._currentLocation, "none", 0, this._musicPositions[this._currentLocation], -1);
        //}

        //private stopMusicTrack(): void
        //{
        //    if (this._currentLocation == "")
        //    {
        //        return;
        //    }

        //    this._musicPositions[this._currentLocation] = this._musicInstances[this._currentLocation].getPosition();
        //    this._musicInstances[this._currentLocation].stop();
        //}

        //public muteOnOff(): void
        //{
        //    if (this._isMusic == this._isSFX)
        //    {
        //        this.isMusic = !this._isMusic;
        //        this.isSFX = this._isMusic;

        //        if (this._currentButton)
        //        {
        //            this._currentButton.setState();
        //        }
        //    }
        //    else
        //    {
        //        alert("use different button for sfx");
        //    }
        //}

        //public set isSFX(value: boolean)
        //    {
        //    this._isSFX = value;
        //}

        //public get isSFX(): boolean
        //{
        //    return this._isSFX;
        //}

        //public set isMusic(value: boolean)
        //    {
        //    this._isMusic = value;

        //    if (this._isMusic)
        //    {
        //        this.playMusicTrack();
        //    }
        //    else
        //    {
        //        this.stopMusicTrack();
        //    }
        //}

        //public get isMusic(): boolean
        //{
        //    return this._isMusic;
        //}

        //public set savingState(state: boolean)
        //    {
        //    this._isMusic = state;
        //    this._isSFX = state;
        //}

        //public set currentButton(value: Button)
        //    {
        //    this._currentButton = value;
        //}
    } 
}
