using UnityEngine;
using System.Collections;

public class WanderingAI : MonoBehaviour {

    private GameObject _player;
    private NavMeshAgent _agent;
    private Vector3 _prevousPosition;
    private bool _alive;

    [SerializeField]
    private GameObject fireballPrefab;
    private GameObject _fireball;

    private enum AIState
    {
        wandering,
        rotating, 
        shooting
    };
    AIState _AIstate;

    void Start()
    {
        _alive = true;
        _agent = GetComponent<NavMeshAgent>();
        FPSInput behaviour = FindObjectOfType<FPSInput>();
        if(behaviour != null)
        {
            _player = behaviour.gameObject;
        }
        _AIstate = AIState.wandering;
    }

    void Update()
    {
        if (!_alive) return;

        if (_AIstate == AIState.wandering)
        {
            if (_agent.destination != _player.transform.position)
            {
                _agent.destination = _player.transform.position;
            }
        }

        if(_AIstate == AIState.rotating)
        {
            _slerp_k += Time.deltaTime * _agent.angularSpeed;
            if(_slerp_k > 1.0f)
            {
                _slerp_k = 1.0f;
                _AIstate = AIState.shooting;
            }
            transform.rotation = Quaternion.Slerp(_startRotation, _endRotation, _slerp_k);
        }

        CheckPlayerOnFire();
    }

    public void SetAlive(bool isAlive)
    {
        _alive = isAlive;
        if(!isAlive)
        {
            _agent.enabled = false;
        }
    }

    private Quaternion _startRotation;
    private Quaternion _endRotation;
    private float _slerp_k;

    private void CheckPlayerOnFire()
    {
        Vector3 fireLine = _player.transform.position - transform.position;
        float distance = fireLine.magnitude;
        RaycastHit hit;
        
        if(Physics.Raycast(transform.position, fireLine, out hit, distance))
        {
            if(hit.collider.gameObject == _player)
            {
                _agent.Stop();

                _AIstate = AIState.rotating;
                fireLine.y = 0.0f;
                _startRotation = transform.rotation;
                _endRotation = Quaternion.LookRotation(fireLine);
                _slerp_k = 0.0f;
            }
            else
            {
                _AIstate = AIState.wandering;
                _agent.destination = _player.transform.position;
                _agent.Resume();
            }
        }else
        {
            return;
        }

        if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
        {
            if (hit.collider.gameObject == _player)
            {
                if(_fireball == null)
                {
                    _fireball = Instantiate(fireballPrefab) as GameObject;
                    _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                    _fireball.transform.rotation = transform.rotation;
                }
            }
        }
    }
}
