using SimpleJSON;
using UnityEngine;


[CreateAssetMenu(fileName = "ShopData", menuName = "Resource/HardData/Shop/ShopData")]
public class ShopData : ScriptableObject, ISaveLoadable {
    [Header("ValuableOffer")]
    [SerializeField] private PackItem[] packs;

    [Space, Header("Chest")]
    [SerializeField] private ChestItem normalChest;
    [SerializeField] private ChestItem eliteChest;
    [SerializeField] private int numberOpenChests = 10;
    [SerializeField] private ItemStack priceChests;
    [SerializeField] private ItemStack oldPriceChests;

    [Space, Header("Gem")]
    [SerializeField] private GemPackItem[] gems;

    [Space, Header("Chip")]
    [SerializeField] private ChipPackInfo chips;

    [Space, Header("Daily Free Pack")]
    [SerializeField] private DailyFreePackInfo dailyFree;

    [Space, Header("Reroll")]
    [SerializeField] private RerollPackInfo rerolls;

    [Space, Header("RewardAds")]
    [SerializeField] private ShopRewardAdsData rewardAds;

    public PackItem[] Packs { get => packs; }
    public GemPackItem[] Gems { get => gems; }
    public ChipPackInfo Chips { get => chips; }
    public ChestItem EliteChest { get => eliteChest; }
    public int NumberOpenChests { get => numberOpenChests; }
    public ItemStack PriceChests { get => priceChests; }
    public ItemStack OldPriceChests { get => oldPriceChests; }
    public DailyFreePackInfo DailyFree { get => dailyFree; }
    public RerollPackInfo Rerolls { get => rerolls; }
    public ShopRewardAdsData RewardAds { get => rewardAds; }

    public ChestItem[] GetAllChest() {
        return new ChestItem[] { normalChest, eliteChest };
    }

    private void OnEnable() {
        Gemmob.EventDispatcher.Instance.AddListener(EventKey.OnWatchRewardAdSuccess, ShopRewardAdsUpgradeProgress);
    }
    private void OnDisable() {
        Gemmob.EventDispatcher.Instance.RemoveListener(EventKey.OnWatchRewardAdSuccess, ShopRewardAdsUpgradeProgress);
    }

    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null) {
            normalChest.LoadFromJson(null);
            eliteChest.LoadFromJson(null);
            dailyFree.LoadFromJson(null);
            chips.LoadFromJson(null);
            rerolls.LoadFromJson(null);
            rewardAds.LoadFJson(null);
            for (int i = 0; i < gems.Length; i++) {
                gems[i].IsBought = false;
            }
            return;
        }
        normalChest.LoadFromJson(saveData.NormalChest);
        eliteChest.LoadFromJson(saveData.EliteChest);
        dailyFree.LoadFromJson(saveData.DailyFree);
        chips.LoadFromJson(saveData.ChipPack);
        rerolls.LoadFromJson(saveData.RerollPack);
        rewardAds.LoadFJson(saveData.RewardAds);
        if (saveData.IsBoughtGems == null) {
            saveData.IsBoughtGems = new bool[gems.Length];
            for (int i = 0; i < saveData.IsBoughtGems.Length; i++) {
                saveData.IsBoughtGems[i] = gems[i].IsBought;
            }
        }
        else {
            for (int i = 0; i < gems.Length; i++) {
                gems[i].IsBought = saveData.IsBoughtGems[i];
            }
        }
    }

    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.NormalChest = normalChest.SaveToJson();
        saveData.EliteChest = eliteChest.SaveToJson();
        saveData.DailyFree = dailyFree.SaveToJson();
        saveData.ChipPack = chips.SaveToJson();
        saveData.RerollPack = rerolls.SaveToJson();
        saveData.RewardAds = rewardAds.Save2Json();
        if (saveData.IsBoughtGems == null)
            saveData.IsBoughtGems = new bool[gems.Length];
        for (int i = 0; i < gems.Length; i++) {
            saveData.IsBoughtGems[i] = gems[i].IsBought;
        }
        return JsonUtility.ToJson(saveData);
    }

    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            normalChest.LoadFJson(null);
            eliteChest.LoadFJson(null);
            dailyFree.LoadFJson(null);
            chips.LoadFJson(null);
            rerolls.LoadFJson(null);
            rewardAds.LoadFJson(null);
            for (int i = 0; i < gems.Length; i++) {
                gems[i].IsBought = false;
            }
        }
        else {
            normalChest.LoadFJson(json[JsonKey.NormalChest]);
            eliteChest.LoadFJson(json[JsonKey.EliteChest]);
            dailyFree.LoadFJson(json[JsonKey.DailyFree]);
            chips.LoadFJson(json[JsonKey.ChipPack]);
            rerolls.LoadFJson(json[JsonKey.RerollPack]);
            rewardAds.LoadFJson(json.HasKey(JsonKey.Reward) ? json[JsonKey.Reward] : null);

            JSONArray gemsNode = json[JsonKey.IsBoughtGems].AsArray;
            for (int i = 0; i < gemsNode.Count; i++) {
                if (i >= gems.Length)
                    continue;
                gems[i].IsBought = gemsNode[i].AsBool;
            }
        }
    }

    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.NormalChest, normalChest.Save2Json());
        node.Add(JsonKey.EliteChest, eliteChest.Save2Json());
        node.Add(JsonKey.DailyFree, dailyFree.Save2Json());
        node.Add(JsonKey.ChipPack, chips.Save2Json());
        node.Add(JsonKey.RerollPack, rerolls.Save2Json());
        node.Add(JsonKey.Reward, rewardAds.Save2Json());

        JSONNode gemNode = new JSONArray();
        for (int i = 0; i < gems.Length; i++) {
            gemNode.Add(gems[i].IsBought);
        }
        node.Add(JsonKey.IsBoughtGems, gemNode);

        return node;
    }

    private void ShopRewardAdsUpgradeProgress() {
        rewardAds.Upgrade();
    }

    public void ReloadData() {
        rewardAds.CheckResetData();
    }

    [System.Serializable]
    public class SaveData {
        [SerializeField] private string nc;
        [SerializeField] private string ec;
        [SerializeField] private bool[] ibg;
        [SerializeField] private string df;
        [SerializeField] private string cpi;
        [SerializeField] private string rpi;
        [SerializeField] private string ra;

        public string NormalChest { get => nc; set => nc = value; }
        public string EliteChest { get => ec; set => ec = value; }
        public bool[] IsBoughtGems { get => ibg; set => ibg = value; }
        public string DailyFree { get => df; set => df = value; }
        public string ChipPack { get => cpi; set => cpi = value; }
        public string RerollPack { get => rpi; set => rpi = value; }
        public string RewardAds { get => ra; set => ra = value; }
    }
}
