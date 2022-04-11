namespace Assets.Scripts.Data
{
    public static class Config
    {
        public const int WIDTH = 9;
        public const int HEIGHT = WIDTH;

        public const float STAGE_W = 5.4f;
        public const float CAMERA_SIZE = 5.69f;
        public const float CAMERA_SIZE_MIN = 4.1f;
        public const float STAGE_H_MIN = STAGE_W * 750 / 640;
        public const float STAGE_H_MAX = STAGE_W * 1138 / 640;

        public const float SIZE_W = 0.6f;
        public const float SIZE_H = SIZE_W;

        public const float PIXEL_SIZE = 60; //html5 version

        public const string GAME_NAME = "Knight_cs";
        public const string GAME_VERSION = "v1.1.5";
    }
}
