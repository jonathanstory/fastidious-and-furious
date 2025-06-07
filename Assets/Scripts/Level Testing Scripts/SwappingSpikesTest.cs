using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelTest
{
    public class SwappingSpikesTest : MonoBehaviour
    {
        [SerializeField] Transform child1;
        [SerializeField] Transform child2;
        [SerializeField] float delay;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(swap());
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator swap()
        {
            while (true)
            {
                bool swapVal = child2.gameObject.activeInHierarchy;
                child1.gameObject.SetActive(swapVal);
                child2.gameObject.SetActive(!swapVal);
                yield return new WaitForSeconds(delay);
            }
        }
    }
}
