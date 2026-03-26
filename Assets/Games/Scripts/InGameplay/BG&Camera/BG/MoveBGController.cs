
using Helper;
using UnityEngine;

public class MoveBGController : MonoBehaviour {
    [SerializeField] private MoveBG[] moveBGs;
    [SerializeField] private Vector2 startPoint;
    [SerializeField] private Vector2 endPoint;
    [SerializeField] private float timeSpawn;
    [SerializeField] private float delaySpawn;

    private Countdowner timeSpawnCountdowner = new Countdowner();
    private bool isStarted;

    private void Start() {
        this.DelayWait(delaySpawn, () => {
            isStarted = true;
            timeSpawnCountdowner.StartCountdown(timeSpawn);
            MoveBG choose = ChooseMoveBG();
            StartMoveBG(choose);
        });
    }


    private void Update() {
        if (isStarted) {
            timeSpawnCountdowner.Countdowning(Time.deltaTime);
            if (timeSpawnCountdowner.IsTimeOut()) {
                timeSpawnCountdowner.StartCountdown(timeSpawn);
                MoveBG choose = ChooseMoveBG();
                StartMoveBG(choose);
            }
        }
    }

    private void StartMoveBG(MoveBG moveBG) {
        if (moveBG != null) {
            moveBG.transform.position = startPoint;
            moveBG.gameObject.SetActive(true);
            moveBG.StartMove(endPoint);
        }

    }


    private MoveBG ChooseMoveBG() {
        MoveBG chooseMove = null;
        int chooseCounter = 0;
        do {
            chooseMove = RandomHelper.RandomInCollection(moveBGs);
            chooseCounter++;
        }
        while (chooseMove.IsMoving() && chooseCounter < 20);
        return chooseMove;
    }
}
