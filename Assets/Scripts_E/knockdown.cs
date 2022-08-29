using UnityEngine;

namespace Scripts_E
{
    public class knockdown : MonoBehaviour
    {
        [SerializeField] private Animator anim;
        private static readonly int Fall = Animator.StringToHash("Fall");
        private static readonly int Fall2 = Animator.StringToHash("Fall2");

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Front"))
            {
                anim.SetBool("Fall",true);
            }
            else if (other.CompareTag("Back"))
            {
                anim.SetBool("Fall2",true);
            }
        }
    }
}
