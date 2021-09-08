using UnityEngine;

public class TigerBehaviour : MonoBehaviour
{
    [SerializeField] private string _animationName;
    [SerializeField] private GameObject _tiger;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "MainCamera")
        {
            _tiger.SetActive(true);
            _animator.Play(_animationName);
        }
    }
}