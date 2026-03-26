
using Helper;
using UnityEngine;

public class BasicLaser : Laser {


    private RaycastHit2D laserHit;

    public override void Beaming(bool isHit) {
        if (myTransform == null)
            myTransform = transform;
        Vector2 direction = myTransform.up;
        UpdateLength();
        if (laserLine) {
            laserLine.startWidth = size * percentSize;
        }
        boxSize.Set(size * percentSize, 0.1f);
        laserHit = Physics2D.BoxCast(myTransform.position, boxSize, 0, direction, curLength, layerTarget);
        Vector2 laserHitPosition = myTransform.position;
        if (muzzleEffect) {
            if (!muzzleEffect.isPlaying) {
                muzzleEffect.Play();
            }
            muzzleEffect.transform.position = myTransform.position;
            muzzleEffect.transform.Scale(size * percentSize);
        }
        if (laserHit) {
            laserHitPosition += direction * laserHit.distance;
            if (isHit) {
                IHitbox hitbox = laserHit.collider.GetComponent<IHitbox>();
                if (hitbox != null) {
                    hitbox.TakeHit(hitInfor, laserHitPosition);
                }

            }
            if (hitEffect) {
                if (!hitEffect.isPlaying) {
                    hitEffect.Play();
                }
                hitEffect.transform.Scale(size * percentSize);
                hitEffect.transform.position = laserHitPosition;
            }
#if UNITY_EDITOR
            distance = laserHit.distance;
#endif
        }
        else {
            laserHitPosition += direction * curLength;
#if UNITY_EDITOR
            distance = curLength;
#endif
            if (hitEffect) {
                if (showHitEffectEnd) {
                    hitEffect.transform.Scale(size * percentSize);
                    hitEffect.transform.position = laserHitPosition;
                    if (!hitEffect.isPlaying) {
                        hitEffect.Play();
                    }
                }
                else {
                    hitEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
            }
        }

        if (laserLine) {
            laserLine.SetPosition(0, myTransform.position);
            laserLine.SetPosition(1, laserHitPosition);
        }
    }

    public override void Beaming(bool isHit, Vector2 directionShot) {
        if (myTransform == null)
            myTransform = transform;
        UpdateLength();
        if (laserLine) {
            laserLine.startWidth = size * percentSize;
        }
        boxSize.Set(size * percentSize, 0.1f);
        laserHit = Physics2D.BoxCast(myTransform.position, boxSize, 0, directionShot, curLength, layerTarget);
        Vector2 laserHitPosition = myTransform.position;
        if (muzzleEffect) {
            if (!muzzleEffect.isPlaying) {
                muzzleEffect.Play();
            }
            muzzleEffect.transform.position = myTransform.position;
            muzzleEffect.transform.Scale(size * percentSize);
        }
        if (laserHit) {
            laserHitPosition += directionShot * laserHit.distance;
            if (isHit) {
                IHitbox hitbox = laserHit.collider.GetComponent<IHitbox>();
                if (hitbox != null) {
                    hitbox.TakeHit(hitInfor, laserHitPosition);
                }

            }
            if (hitEffect) {
                if (!hitEffect.isPlaying) {
                    hitEffect.Play();
                }
                hitEffect.transform.Scale(size * percentSize);
                hitEffect.transform.position = laserHitPosition;
            }
#if UNITY_EDITOR
            distance = laserHit.distance;
#endif
        }
        else {
            laserHitPosition += directionShot * curLength;
#if UNITY_EDITOR
            distance = curLength;
#endif
            if (hitEffect) {
                if (showHitEffectEnd) {
                    hitEffect.transform.Scale(size * percentSize);
                    hitEffect.transform.position = laserHitPosition;
                    if (!hitEffect.isPlaying) {
                        hitEffect.Play();
                    }
                }
                else {
                    hitEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
            }
        }

        if (laserLine) {
            laserLine.SetPosition(0, myTransform.position);
            laserLine.SetPosition(1, laserHitPosition);
        }
    }
}
