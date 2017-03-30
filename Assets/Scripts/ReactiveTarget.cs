using UnityEngine;
using System.Collections;

public class ReactiveTarget : MonoBehaviour {

	public void ReactToHit()
    {
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        this.transform.Rotate(-75, 0, 0);

        WanderingAI behaviour = GetComponent<WanderingAI>();
        if(behaviour != null)
        {
            behaviour.SetAlive(false);
        }

        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);
    }
}
