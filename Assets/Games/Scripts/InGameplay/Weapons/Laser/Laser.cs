using System.Collections.Generic;
using UnityEngine;

public abstract class Laser : MonoBehaviour {
    [SerializeField] protected CharacterBase characterBase;
    [SerializeField] protected LineRenderer laserLine;
    [SerializeField] protected LayerMask layerTarget;
    [SerializeField] protected float size;
    [SerializeField] protected int maxLength = 20;
    [SerializeField] protected float speed;
    [SerializeField] protected ParticleSystem hitEffect;
    [SerializeField] protected ParticleSystem muzzleEffect;
    [SerializeField] protected bool showHitEffectEnd;

    protected HitInfor hitInfor;
    protected Vector2 boxSize;
    protected Transform myTransform;
    protected float percentSize;
    protected float curLength;
    protected float maxSize = 5;


    private void Awake() {
        myTransform = transform;
        maxSize = 5;
    }
    public void SetCharacterBase(CharacterBase characterBase) {
        this.characterBase = characterBase;
    }
    public void SetRadiusSize(float size, bool reset = true) {
        if (reset)
            this.size = size;
        else {
            if (this.size > maxSize) {
                this.size = maxSize;
            }
            else
                this.size += size;
        }

    }

    public void SetMaxSize(float value) {
        maxSize = value;
    }

    public float GetRadiusSize() {
        return this.size;
    }

    public void SetInfor(int damage, List<IEffectAttackModable> effects) {
        if (hitInfor == null) {
            hitInfor = new HitInfor();
        }
        hitInfor.SetInfor(damage, effects, characterBase);
    }
    public void SetInfor(int damage, List<IEffectAttackModable> effects, ObjectBase causer, int critChance = 0, float critDamage = 0) {
        if (hitInfor == null) {
            hitInfor = new HitInfor();
        }
        hitInfor.SetInfor(damage, effects, causer, critChance, critDamage);
    }

    public void SetInfor(HitInfor hitInfor) {
        this.hitInfor = hitInfor;
    }

    public void SetPercentSize(float percentSize) {
        percentSize = percentSize < 0 ? 0 : percentSize;
        this.percentSize = percentSize;
    }

    public void SetAlphaLaser(bool active, float minValue = 0, float maxValue = 1) {
        var temp = laserLine.startColor;
        temp.a = active ? maxValue : minValue;
        laserLine.startColor = temp;
        laserLine.endColor = temp;
    }
    public void SetAlphaLaser(float value) {
        var temp = laserLine.startColor;
        temp.a = value;
        laserLine.startColor = temp;
        laserLine.endColor = temp;
    }

    protected void UpdateLength(bool isMax = false) {
        if (isMax) {
            curLength = maxLength;
            return;
        }
        if (curLength < 0) {
            curLength = maxLength;
            return;
        }
        if (curLength < maxLength) {
            curLength += speed * Time.deltaTime;
        }
    }

    public void StartBeam() {
        if (laserLine) {
            laserLine.startWidth = 0;
        }
        curLength = 0;
        percentSize = 1;
    }

    public void EndBeam() {
        if (laserLine) {
            laserLine.startWidth = 0;
        }
        curLength = 0;
    }

    public abstract void Beaming(bool isHit);
    public abstract void Beaming(bool isHit, Vector2 directionShot);
    public void SetMaxLength(int length) {
        maxLength = length;
    }

#if UNITY_EDITOR
    [SerializeField, ColorUsage(true)] private Color color;
    protected float distance;

    private void OnDrawGizmos() {
        Gizmos.color = color;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * distance);
    }
#endif
}
