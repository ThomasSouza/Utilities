using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostView : EnemyViewFather
{
    public GhostView(Enemy enemy, Slider lifebar, GameObject vfxOnDie)
    {
        _lifebar = lifebar;
        _enemy = enemy;
        _lifebar.maxValue = _enemy.maxHP;
        _lifebar.value = _enemy.maxHP;
        _vfxOnDie = vfxOnDie;
        _renderer = enemy.GetComponentInChildren<Renderer>();

        OnConstruct();
    }

    Renderer _renderer;

    const float SinAmp = 0.25f;     // Amplitud de la curva en Y
    const float SinFreq = 6;        // Frecuencia  
    const float SinOffsetY = 0.5f;  // Offset en Y para que este entre valores de 0.25 y 0.75

    public IEnumerator BlendAlphaMaxToMin(float val)
    {
        // Comienza la corutina para blendear de 1 al alpha que arranca la funcion Seno
        float preauxAlpha = _renderer.material.GetFloat("_AlphaBlend");
        while (preauxAlpha >= SinAmp * Mathf.Sin(0) + SinOffsetY)
        {
            preauxAlpha -= Time.deltaTime;
            _renderer.material.SetFloat("_AlphaBlend", preauxAlpha);
        }
        // Comienza la corutina para ir y venir con el alpha
        float auxTime = 0;
        while (auxTime <= val)
        {
            auxTime += Time.deltaTime;
            _renderer.material.SetFloat("_AlphaBlend", SinAmp * Mathf.Sin(SinFreq * auxTime) + SinOffsetY);
            yield return new WaitForEndOfFrame();
        }

        // Comienza la corutina para blendear del alpha que quedo, al alpha 1
        float auxAlpha = _renderer.material.GetFloat("_AlphaBlend");
        while (auxAlpha <= 1)
        {
            auxAlpha += Time.deltaTime;
            _renderer.material.SetFloat("_AlphaBlend", auxAlpha);
        }
        // Lo fuerzo a 1 por si se paso o se quedo antes del 1
        _renderer.material.SetFloat("_AlphaBlend", 1); 
        yield break;
    }
}
