using System.Collections;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    private const int _lifeTime = 10;

    void Start()
    {
        StartCoroutine(TimeCount(_lifeTime));
    }

    IEnumerator TimeCount(int lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }
}
