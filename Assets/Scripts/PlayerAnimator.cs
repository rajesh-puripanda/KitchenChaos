using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    // because strings can be hard to work with, even if you type in a wrong string, the compiler wouldnt know, and that why we use variables


    // giving reference of our player
    [SerializeField] private Player player;


    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
       
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
