namespace Assets.Scripts.Data
{
    public static class Config
    {
        /** WIDTH: number = 9*/
        public const int WIDTH = 9;
        /** HEIGHT: number = 9*/
        public const int HEIGHT = WIDTH;

        /**STAGE_W: number = 640*/
        public const float STAGE_W = 5.4f;
        /**STAGE_H_MIN: number = 750*/
        public const float STAGE_H_MIN = STAGE_W * 750 / 640;
        /**STAGE_H_MAX: number = 1138*/
        public const float STAGE_H_MAX = STAGE_W * 1138 / 640;

        /**SIZE_W: number = 60*/
        public const float SIZE_W = 0.614f;// STAGE_W * 60 / 640;
        /**SIZE_H: number = 60*/
        public const float SIZE_H = SIZE_W;

        ///**MARGIN_TOP: number = 120*/
        //public const int MARGIN_TOP = 150;
        ///**PADDING: number = 194*/
        //public const int PADDING = 194;

        /**GAME_NAME: string = "Knight"*/
        public const string GAME_NAME = "Knight_cs";
        /**GAME_VERSION: string = "Knight"*/
        public const string GAME_VERSION = "1.0.1";
    }
}
