using UnityEngine;

[System.Serializable]
public class LightningBolt {
    public static readonly Vector3[] arrayVector3Empty = new Vector3[0];
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float segmentLength;
    [SerializeField] private Vector2 randomStrength;
    [SerializeField] private float startWidth;

    public LineRenderer LineRenderer => lineRenderer;
    public float StartWidth => startWidth;

    public void SetActive(bool enabled) {
        LineRenderer.enabled = enabled;
    }

    public void Clear() {
        LineRenderer.positionCount = 0;
    }

    public void SetStartWidthMultiple(float value) {
        LineRenderer.startWidth = StartWidth * value;
    }

    private void DrawLightning(Vector2 startPosition, Vector2 endPosition) {
        //Calculated amount of Segments
        float distance = Vector2.Distance(startPosition, endPosition);
        int segments = 5;
        if (distance > segmentLength) {
            segments = Mathf.FloorToInt(distance / segmentLength) + 2;
        }
        else {
            segments = 4;
        }

        // Set the amount of points to the calculated value
        LineRenderer.positionCount = segments;
        LineRenderer.SetPosition(0, startPosition);
        Vector2 lastPosition = startPosition;
        for (int j = 1; j < segments - 1; j++) {
            //Go linear from source to target
            Vector2 tmp = Vector2.Lerp(startPosition, endPosition, j / (float)segments);
            //Add randomness
            lastPosition = new Vector2(tmp.x + Random.Range(-randomStrength.x, randomStrength.x), tmp.y + Random.Range(-randomStrength.y, randomStrength.y));
            //Set the calculated position
            LineRenderer.SetPosition(j, lastPosition);
        }
        LineRenderer.SetPosition(segments - 1, endPosition);
    }

    public void DrawLightning(params Vector2[] positions) {
        if (positions.Length < 2) {
            return;
        }
        else if (positions.Length == 2) {
            DrawLightning(positions[0], positions[1]);
            return;
        }

        int totalSegments = 0;
        for (int i = 1; i < positions.Length; i++) {
            Vector2 startPosition = positions[i - 1];
            Vector2 endPosition = positions[i];
            //Calculated amount of Segments
            float distance = Vector2.Distance(startPosition, endPosition);
            int segments = 5;
            if (distance > segmentLength) {
                segments = Mathf.FloorToInt(distance / segmentLength) + 2;
            }
            else {
                segments = 4;
            }
            totalSegments += segments;
        }

        LineRenderer.positionCount = totalSegments;
        int currentPositionIndex = 0;

        for (int i = 1; i < positions.Length; i++) {
            Vector2 startPosition = positions[i - 1];
            Vector2 endPosition = positions[i];
            //Calculated amount of Segments
            float distance = Vector2.Distance(startPosition, endPosition);
            int segments = 5;
            if (distance > segmentLength) {
                segments = Mathf.FloorToInt(distance / segmentLength) + 2;
            }
            else {
                segments = 4;
            }

            // Set the amount of points to the calculated value
            LineRenderer.SetPosition(currentPositionIndex, startPosition);
            currentPositionIndex++;
            Vector2 lastPosition = startPosition;
            for (int j = 1; j < segments - 1; j++) {
                //Go linear from source to target
                Vector2 tmp = Vector2.Lerp(startPosition, endPosition, j / (float)segments);
                //Add randomness
                lastPosition = new Vector2(tmp.x + Random.Range(-randomStrength.x, randomStrength.x), tmp.y + Random.Range(-randomStrength.y, randomStrength.y));
                //Set the calculated position
                LineRenderer.SetPosition(currentPositionIndex, lastPosition);
                currentPositionIndex++;
            }
            LineRenderer.SetPosition(currentPositionIndex, endPosition);
            currentPositionIndex++;
        }
    }
}
