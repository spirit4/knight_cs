using Assets.Scripts.Data;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Core
{
    public class Controller// : Singleton<Controller>
    {
        public ManagerBg bg;
        public Model model;

        public static Controller Instance;

        public Controller(ManagerBg bg)
        {
            Controller.Instance = this;

            this.bg = bg;
            Init();
        }

        private void Init()
        {
            this.model = new Model();

            JSONRes.Init();
       }
    }
}
