using UnityEngine;
using System.Collections;

public class EffectManager : SingletonBehaviour<EffectManager> {

    public void EffectCreate(GameObject effect, Vector2 pos, float time)
    {
        GameObject newEffect =Instantiate(effect);
        newEffect.transform.position = pos;
        StartCoroutine(EffectPlay(effect, time));
    }

    public IEnumerator EffectPlay(GameObject effect, float time)
    {

        while (time > 0)
        {
            time -= GameTime.deltaTime;
            yield return null;
        }

        Destroy(effect);

        yield return null;
    }
}
