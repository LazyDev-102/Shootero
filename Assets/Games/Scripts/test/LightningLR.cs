using UnityEngine;

public class LightningLR : MonoBehaviour {
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private LineRenderer LR;
    [SerializeField] private float arcLength = 2.0f;
    [SerializeField] private float arcVariation = 2.0f;
    [SerializeField] private float inaccuracy = 1.0f;


    public void Active(bool isActive) {
        gameObject.SetActive(isActive);
    }

    public void UpdatePosition(Vector2 startPosition, Vector2 endPosition) {
        startPoint.position = startPosition;
        endPoint.position = endPosition;
    }

    private void Update() {
        var lastPoint = startPoint.position;
        var i = 1;
        LR.SetPosition(0, startPoint.position);//make the origin of the LR the same as the transform
        while (Vector3.Distance(endPoint.position, lastPoint) > .5) {//was the last arc not touching the target?
            //LR.SetVertexCount(i + 1);//then we need a new vertex in our line renderer
            LR.positionCount = i + 1;
            var fwd = endPoint.position - lastPoint;//gives the direction to our target from the end of the last arc
            fwd.Normalize();//makes the direction to scale
            fwd = Randomize(fwd, inaccuracy);//we don't want a straight line to the target though
            fwd *= Random.Range(arcLength * arcVariation, arcLength);//nature is never too uniform
            fwd += lastPoint;//point + distance * direction = new point. this is where our new arc ends
            LR.SetPosition(i, fwd);//this tells the line renderer where to draw to
            i++;
            lastPoint = fwd;//so we know where we are starting from for the next arc
        }
    }

    private Vector3 Randomize(Vector3 v3, float inaccuracy2) {
        v3 += new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * inaccuracy2;
        v3.Normalize();
        return v3;
    }
}
