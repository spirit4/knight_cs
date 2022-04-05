using Assets.Scripts.Data;
using UnityEngine.UI;

namespace Assets.Scripts.UnityEditor
{
    public class PanelCredits : BasePanel 
    {
       protected override void Start()
        {
            base.Start();

            Text version = this.transform.Find("TextVersion").GetComponent<Text>();
            version.text = Config.GAME_VERSION;
        }

    }
}
