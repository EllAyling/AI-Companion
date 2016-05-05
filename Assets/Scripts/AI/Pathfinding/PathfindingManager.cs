using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PathfindingManager : MonoBehaviour {

    static PathfindingManager instance;
    Queue<PathRequest> queue = new Queue<PathRequest>();
    AStar pathfinding;
    PathRequest currentRequest;

    bool isProcessingPath;
    // Use this for initialization
    void Awake () {

        instance = this;
        pathfinding = GetComponent<AStar>();
    }
	
    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        instance.queue.Enqueue(newRequest);
        instance.TryProcessNext();
    }

    void TryProcessNext()
    {
        if (!isProcessingPath && queue.Count > 0)
        {
            currentRequest = queue.Dequeue();
            isProcessingPath = true;
            pathfinding.StartFindPath(currentRequest.pathStart, currentRequest.pathEnd);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        currentRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }

    struct PathRequest {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 _pathStart, Vector3 _pathEnd, Action<Vector3[], bool> _callback)
        {
            pathStart = _pathStart;
            pathEnd = _pathEnd;
            callback = _callback;
        }
    }
}
