using Gemmob;
using Helper;
using UnityEngine;

public class PierceLaser : Laser {
    private RaycastHit2D[] results = new RaycastHit2D[8];
    public override void Beaming(bool isHit) {
        try {
            if (transform == null || boxSize == null)
                return;
            if (myTransform == null)
                myTransform = transform;
            UpdateLength(isHit);
            Vector2 direction = myTransform.up;
            if (laserLine) {
                laserLine.startWidth = size * percentSize;
            }
            boxSize.Set(size * percentSize, 0.1f);
            int numberHit = Physics2D.BoxCastNonAlloc(myTransform.position, boxSize, 0, direction, results, curLength, layerTarget);
            Vector2 laserHitPosition = myTransform.position;
            if (muzzleEffect != null) {
                if (!muzzleEffect.isPlaying) {
                    muzzleEffect.Play();
                }
                muzzleEffect.transform.position = laserHitPosition;
                muzzleEffect.transform.Scale(size * percentSize);
            }

            for (int i = 0; i < numberHit; ++i) {
                IHitbox hitbox = results[i].collider.GetComponent<IHitbox>();
                if (hitbox != null) {
                    if (isHit) {
                        hitbox.TakeHit(hitInfor, results[i].point);
                    }
                    if (hitEffect != null) {
                        ParticleSystem newHit = hitEffect.Spawn();
                        newHit.transform.Scale(size * percentSize);
                        newHit.transform.position = results[i].point;
                        if (!newHit.isPlaying) {
                            newHit.Play();
                        }
                    }
                }
            }
            laserHitPosition += direction * curLength;
            if (laserLine) {
                laserLine.SetPosition(0, myTransform.position);
                laserLine.SetPosition(1, laserHitPosition);
            }
        }
        catch {

        }


#if UNITY_EDITOR
        distance = curLength;
#endif
    }

    public override void Beaming(bool isHit, Vector2 directionShot) {
        try {
            if (myTransform == null)
                myTransform = transform;
            UpdateLength(isHit);
            if (laserLine) {
                laserLine.startWidth = size * percentSize;
            }
            boxSize.Set(size * percentSize, 0.1f);
            int numberHit = Physics2D.BoxCastNonAlloc(myTransform.position, boxSize, 0, directionShot, results, curLength, layerTarget);
            Vector2 laserHitPosition = myTransform.position;
            if (muzzleEffect) {
                if (!muzzleEffect.isPlaying) {
                    muzzleEffect.Play();
                }
                muzzleEffect.transform.position = laserHitPosition;
                muzzleEffect.transform.Scale(size * percentSize);
            }

            for (int i = 0; i < numberHit; ++i) {
                IHitbox hitbox = results[i].collider.GetComponent<IHitbox>();
                if (hitbox != null) {
                    if (isHit) {
                        hitbox.TakeHit(hitInfor, results[i].point);
                    }
                    if (hitEffect) {
                        ParticleSystem newHit = hitEffect.Spawn();
                        newHit.transform.Scale(size * percentSize);
                        newHit.transform.position = results[i].point;
                        if (!newHit.isPlaying) {
                            newHit.Play();
                        }
                    }
                }
            }
            laserHitPosition += directionShot * curLength;
            if (laserLine) {
                laserLine.SetPosition(0, myTransform.position);
                laserLine.SetPosition(1, laserHitPosition);
            }
        }
        catch {

        }


#if UNITY_EDITOR
        distance = curLength;
#endif
    }
}
