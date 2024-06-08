using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePhysics : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int segmentCount;
    public float segmentLength;

    private List<Segment> segments = new List<Segment>();

    [Space(10f)]
    public Transform startTransform;

    // Reset 하면 LineRenderer 불러오기
    private void Reset()
    {
        TryGetComponent(out lineRenderer);
    }

    // 처음 위치에서 점점 아래쪽으로 내려가서 세그먼트를 만들어주기
    private void Awake()
    {
        Vector2 segmentPos = startTransform.position;
        for(int i = 0; i < segmentCount; i++)
        {
            segments.Add(new Segment(segmentPos));
            segmentPos.y -= segmentLength;
        }
    }

    // 세그먼트들을 움직여줄 함수
    private void UpdateSegments()
    {
        for(int i =0; i < segments.Count; i++)
        {

        }
    }

    public class Segment
    {
        public Vector2 previousPos;
        public Vector2 position;
        public Vector2 velocity;

        public Segment(Vector2 pos)
        {
            previousPos = pos;
            position = pos;
            velocity = Vector2.zero;
        }
    }
}
