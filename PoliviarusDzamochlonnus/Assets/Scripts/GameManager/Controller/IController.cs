using UnityEngine;

[RequireComponent(typeof(IMovement))]
public abstract class IController : MonoBehaviour
{
    #region Variables
    [SerializeField] protected float speedMovement;
    [SerializeField] protected float speedRotate;

    protected IMovement _movement;
    #endregion

    #region Unity methods
    protected virtual void Awake()
    {
        _movement = GetComponent<IMovement>();
    }
    protected void Update()
    {
        Move();
    }
    #endregion

    #region Non-Unity methods
    protected abstract void Move();
    #endregion
}
