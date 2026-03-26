using UnityEngine;
using Gemmob;
using Gemmob.Api.Analytics;
using System.Collections;

public class GameResourceLoader : SingletonBindAlive<GameResourceLoader> {
    [SerializeField] private GameResources gameResources;
    [SerializeField] private Bootstrap bootstrap;

    public GameResources GameResources { get => gameResources; }


    //private void Start() {
    //    LoadAllData();
    //}

    public void LoadAllData() {
        LoadPlayerStatManager();
        SaveLoad.Load();
        LoadResourceManager();
        StartCoroutine(ActionAfterLoadData());
    }
    private void LoadPlayerStatManager() {
        bootstrap.AssignStart();
        PlayerStatManager.Instance.AssignData();
    }

    private void LoadResourceManager() {
        GameResources.Instance.Assign();
        PlayerStatManager.Instance.LoadData();
    }

    public static IEnumerator ActionAfterLoadData() {
        float duration = 10;
        float time = 0;
        PrefSaver.FirstOpenGameAfterConvert = false;
#if IAP_ENABLE
        while (!GameIAP.Instance.IsInitialized()) {
            time += 1;
            if (time > duration)
                yield break;
            yield return Yielder.Wait(1);
        }
#else
        yield return null;
#endif
        GameResources.Instance.ShipPackData.ActionOnLoad();
    }
}
