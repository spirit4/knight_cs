using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Scripts.Data
{
    public class AddressablesLoader
    {
        /// <summary>
        /// Synchronous loading
        /// </summary>
        public static T GetObject<T>(string key)
        {
            var handle = Addressables.LoadAssetAsync<T>(key);
            return  handle.WaitForCompletion();
        }

        public static void ReleaseObject(GameObject obj)
        {
            if (!Addressables.ReleaseInstance(obj))
                GameObject.Destroy(obj);
        }

    }
}