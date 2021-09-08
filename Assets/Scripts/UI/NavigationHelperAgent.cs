using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavigationHelperAgent : MonoBehaviour
{
    public static NavigationHelperAgent Instance { get { return _instance; } }
    private static NavigationHelperAgent _instance;

    [SerializeField] private float _distancePause = 10f;
    [SerializeField] private float _distanceResume = 3f;
    [SerializeField] private float _distance = 3f;

    private NavMeshAgent _agent;
    private Transform _cameraPos;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        _agent = GetComponent<NavMeshAgent>();
        _cameraPos = Camera.main.transform;
    }

    private void Update()
    {
        _distance = Vector3.Distance(transform.position, _cameraPos.position);
        if (_distance > _distancePause)
        {
            _agent.isStopped = true;
        }
        if (_distance < _distanceResume)
        {
            _agent.isStopped = false;
        }
    }

    public void GoToPoint(Transform point)
    {
        _agent.isStopped = false;
        _agent.SetDestination(point.position);
    }
}