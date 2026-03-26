using Gemmob;
using System.Collections;
using UnityEngine;

public class ReflectiveShieldManager : MonoBehaviour {
    [SerializeField] private LightningLineBolt lightningLineBolt;
    [SerializeField] private float durationTime = 0.1f;

    private HitInfor hit;
    public bool CanReflex;
    private float percentDamage = 0.2f;
    private EnemyBase infor;
    private Transform target;
    private void Awake() {
        EventDispatcher.Instance.AddListener<EventKey.OnShieldHitDamage>(OnProtectShieldHitDamage);
        EventDispatcher.Instance.AddListener<EventKey.OnEnergyShieldHitDamage>(OnEnergyShieldHitDamage);
        hit = new HitInfor();
    }
    private void OnDestroy() {
        EventDispatcher.Instance.RemoveListener<EventKey.OnShieldHitDamage>(OnProtectShieldHitDamage);
        EventDispatcher.Instance.RemoveListener<EventKey.OnEnergyShieldHitDamage>(OnEnergyShieldHitDamage);
    }
    private void OnProtectShieldHitDamage(EventKey.OnShieldHitDamage infor) {
        if (gameObject == null || !gameObject.activeInHierarchy)
            return;
        if (!CanReflex)
            return;
        if (infor.Target != target)
            return;
        if (infor.Causer == null || infor.Causer.GetComponent<EnemyBase>() == null)
            return;
        this.infor = (EnemyBase)infor.Causer;
        SpawnBullet(gameObject, this.infor);
    }
    private void OnEnergyShieldHitDamage(EventKey.OnEnergyShieldHitDamage infor) {
        if (!gameObject.activeInHierarchy)
            return;
        if (!CanReflex)
            return;
        if (infor.Target != target)
            return;
        if (infor.Causer == null || infor.Causer.GetComponent<EnemyBase>() == null)
            return;
        this.infor = (EnemyBase)infor.Causer;
        SpawnBullet(gameObject, this.infor);
    }
    public void SpawnBullet(GameObject source, EnemyBase destination) {
        var clone = lightningLineBolt.Spawn(GameManager.Instance.GameLoader.transform);
        hit.SetInfor((int)(infor.EnemyStat.Atk.Value * percentDamage), null, null);
        destination.EnemyHitbox.TakeHitDamage(hit, transform.position);
        SetPosition(clone, source.transform, destination.transform);
        if (gameObject.activeInHierarchy)
            StartCoroutine(Survival(clone));
    }

    private void SetPosition(LightningLineBolt item, Transform source, Transform destination) {
        item.SetStartPoint(source);
        item.SetEndPoint(destination);
    }
    IEnumerator Survival(LightningLineBolt item) {
        item.gameObject.SetActive(true);
        float time = 0f;
        while (time < durationTime) {
            time += Time.deltaTime;
            item.Draw();
            yield return null;
        }
        //yield return Yielder.Wait(durationTime);
        item.Recycle();
    }
    public void EnableShield(bool canFlex, float percentDamage, Transform target) {
        this.percentDamage = percentDamage;
        this.target = target;
        CanReflex = canFlex;
        gameObject.SetActive(true);
    }
    public void DisableShield() {
        gameObject.SetActive(false);
    }

}
