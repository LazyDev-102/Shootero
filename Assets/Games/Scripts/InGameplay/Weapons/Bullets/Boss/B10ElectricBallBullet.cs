using Gemmob;
using Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B10ElectricBallBullet : BulletBase {
    [SerializeField] private LightningLineRenderer lightningLinePrefab;
    [SerializeField] private GameObject warningPrefab;
    [SerializeField] private Area lineRandomArea;
    [SerializeField] private float minDistance;


    private float speed;
    private float accelerationSpeed = 0f;
    private Vector2 direction;
    private Vector2 targetPosition;
    bool enableMove = false;
    private float warningTime;
    private List<Vector2> positions = new List<Vector2>();
    private List<GameObject> warningObjs = new List<GameObject>();
    private List<LightningLineRenderer> lightnings = new List<LightningLineRenderer>();


    private int numberShot;
    private int numberLine;
    private float deltaShot;
    private int damageLine;



    protected override void OnEnable() {
        base.OnEnable();
        enableMove = false;
    }

    private void OnDisable() {
        StopAllCoroutines();
    }

    public void Shoot(Vector2 pointTarget, float speed, float acceleration = 0f) {
        enableMove = true;
        this.speed = speed + SpeedStat.Value;
        this.targetPosition = pointTarget;
        direction = (pointTarget - (Vector2)transform.position).normalized;
        transform.up = direction;
        this.accelerationSpeed = acceleration;
    }

    public void SetInfo(int numberShot, float deltaShot, int numberLine, float warningTime, int damageLine) {
        this.warningTime = warningTime;
        this.numberShot = numberShot;
        this.deltaShot = deltaShot;
        this.numberLine = numberLine;
        this.damageLine = damageLine;
    }


    private void FixedUpdate() {
        float deltaTime = Time.fixedDeltaTime * Time.timeScale;
        if (enableMove) {
            MyRigi.MovePosition(MyRigi.position + direction * speed * deltaTime);
            speed += accelerationSpeed * deltaTime;
            if (Vector2.Distance(MyRigi.position, targetPosition) < speed * deltaTime) {
                enableMove = false;
                if (gameObject.activeInHierarchy)
                    StartCoroutine(Shot());
            }
        }
    }


    private IEnumerator Shot() {
        for (int i = 0; i < numberShot; ++i) {
            CalculatorPosition();
            SpawnWarning();
            yield return Yielder.Wait(warningTime);
            DespawnWarning();
            SpawnLightningLine();
            yield return Yielder.Wait(0.5f);
            DespawnLightningLine();
            yield return Yielder.Wait(deltaShot);
        }
        this.Recycle();
    }


    private void CalculatorPosition() {
        positions.Clear();
        for (int i = 0; i < numberLine; ++i) {
            bool blockAdd = false;
            Vector2 newPosition;
            int count = 0;
            do {
                count++;
                blockAdd = false;
                newPosition = BorderHelper.GetWorldPointInsideArea(lineRandomArea);
                if (Vector2.Distance(newPosition, transform.position) < minDistance) {
                    blockAdd = true;
                    continue;
                }
                foreach (var p in positions) {
                    if (Vector2.Distance(p, newPosition) < minDistance) {
                        blockAdd = true;
                        break;
                    }
                }
            }
            while (blockAdd && count < 100);
            positions.Add(newPosition);
        }
    }

    private void SpawnWarning() {
        warningObjs.Clear();
        for (int i = 0; i < numberLine; ++i) {
            Vector2 position = positions[i];
            GameObject newWarning = warningPrefab.Spawn(position);
            warningObjs.Add(newWarning);
        }
    }

    private void DespawnWarning() {
        foreach (var obj in warningObjs) {
            obj.Recycle();
        }
    }

    private void SpawnLightningLine() {
        lightnings.Clear();
        for (int i = 0; i < numberLine; ++i) {
            Vector2 position = positions[i];
            //positions.Remove(position);
            LightningLineRenderer newLine = lightningLinePrefab.Spawn(transform);
            newLine.SetActive(true);
            newLine.SetInfor(damageLine, HitInfor.Causer);
            newLine.UpdatePosition(transform.position, position);
            lightnings.Add(newLine);
        }
    }

    private void DespawnLightningLine() {
        foreach (var line in lightnings) {
            line.Recycle();
        }
    }
}
