using UnityEngine;

namespace Scripts_E
{
    public class knockdown : MonoBehaviour
    {
        [SerializeField] private Animator anim, playeranim;
        private static readonly int Fall = Animator.StringToHash("Fall");
        private static readonly int Fall2 = Animator.StringToHash("Fall2");
        private int rand;

        private void Start()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Front"))
            {
                rand = Random.Range(0, 2);
                anim.SetBool("Fall",true);
            }
            else if (other.CompareTag("Back"))
            {
                anim.SetBool("Fall2",true);
            }
        }
    }
}
