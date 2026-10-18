using System.Collections;
using UnityEngine;
using UnityEngine.VFX;
public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance;
    public GameObject vfxPrefab;
    public int poolSize = 5; // How many effects can play at the exact same time

    private Animator[] vfxPool;
    void Awake()
    {
        Instance = this;
        vfxPool = new Animator[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(vfxPrefab, transform);
            obj.SetActive(false);
            vfxPool[i] = obj.GetComponent<Animator>();
        }
    }

    // Other scripts will call this function
    public void PlayVFX(string triggerName, Vector3 position, float duration = 0.5f, Transform targetParent = null)
    {
        foreach (Animator anim in vfxPool)
        {
            if (!anim.gameObject.activeInHierarchy)
            {
                anim.gameObject.SetActive(true);
                if (targetParent != null)
                {
                    anim.transform.SetParent(targetParent);
                    anim.transform.localPosition = Vector3.zero; 
                }
                else
                {
                    anim.transform.position = position;
                }
                anim.SetTrigger(triggerName);
                StartCoroutine(HideAfter(anim.gameObject, duration));
                return;
            }
        }
        Debug.LogWarning("VFX Pool is full! Increase poolSize if you need more simultaneous effects.");
    }

    private IEnumerator HideAfter(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.transform.SetParent(transform);
        obj.SetActive(false);
    }
}
