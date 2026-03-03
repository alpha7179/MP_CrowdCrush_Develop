using UnityEngine;

namespace MapAndRadarSystem
{
    public class DoorScript : MonoBehaviour
    {
        [SerializeField] private Animation doorAnim;
        [SerializeField] private AudioClip audioOpen;
        [SerializeField] private AudioClip audioClose;

        private AudioSource audioSource;
        private float lastTimeInteracted = 0f;

        void Start()
        {
            // 컴포넌트 자동 연결
            if (doorAnim == null)
                doorAnim = GetComponent<Animation>();

            audioSource = GetComponent<AudioSource>();
        }

        public void DoorInteraction(bool open)
        {
            if (Time.time > lastTimeInteracted + 0.5f)
            {
                lastTimeInteracted = Time.time;

                if (open)
                {
                    doorAnim.Play("Open");
                    audioSource.PlayOneShot(audioOpen);
                }
                else
                {
                    doorAnim.Play("Close");
                    audioSource.PlayOneShot(audioClose);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("NPC"))
            {
                DoorInteraction(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("NPC"))
            {
                DoorInteraction(false);
            }
        }
    }
}