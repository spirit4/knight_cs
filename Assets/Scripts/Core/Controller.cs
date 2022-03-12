using Assets.Scripts.Data;

namespace Assets.Scripts.Core
{
    public class Controller
    {
        public ManagerBg bg;
        public Model model;

        public static Controller instance;

        public Controller(ManagerBg bg)
        {
            Controller.instance = this;

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
