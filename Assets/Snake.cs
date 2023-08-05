using System.Collections.Generic;
using UnityEngine;


public class Snake : MonoBehaviour
{
    private List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;
    public Vector2 _direction = Vector2.right;
    private Vector2 input;
    public int initialSize = 4;

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            _direction = Vector2.up;
        }else if(Input.GetKeyDown(KeyCode.DownArrow)){
            _direction = Vector2.down;
        }else if(Input.GetKeyDown(KeyCode.RightArrow)){
            _direction = Vector2.right;
        }else if(Input.GetKeyDown(KeyCode.LeftArrow)){
            _direction = Vector2.left;
        }
    }

    private void FixedUpdate()
    {
        if (input != Vector2.zero) {
            _direction = input;
        }

        for (int i = segments.Count - 1; i > 0; i--) {
            segments[i].position = segments[i - 1].position;
        }

        float x = Mathf.Round(transform.position.x) + _direction.x;
        float y = Mathf.Round(transform.position.y) + _direction.y;

        transform.position = new Vector2(x, y);
    }

    public void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    public void ResetState()
    {
        _direction = Vector2.right;
        transform.position = Vector3.zero;
        for (int i = 1; i < segments.Count; i++) {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(transform);
        for (int i = 0; i < initialSize - 1; i++) {
            Grow();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food")) {
            Grow();
        } else if (other.gameObject.CompareTag("Obstacle")) {
            ResetState();
        }
    }

}