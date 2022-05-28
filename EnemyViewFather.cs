using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyViewFather : IView
{
    protected Enemy _enemy;
    protected Slider _lifebar;
    protected GameObject _vfxOnDie;
    protected int fireIndex;
    protected int iceIndex;
    protected bool iceAct;
    protected bool fireAct;

    public virtual void OnConstruct()
    {
        _enemy.OnLifeChange += OnLifeChange;
        _enemy.OnDie += OnDieParticles;
        _enemy.OnFireDebuff += OnAddFireDebuff;
        _enemy.OnEndFireDebuff += OnEndFireDebuff;
        _enemy.OnIceDebuff += OnAddIceDebuff;
        iceAct = false;
        fireAct = false;

        // Reiniciamos los sprites por si quedaron activos
        for (int i = 0; i < _enemy.debuffSprites.Count; i++)
        {
            _enemy.debuffSprites[i].gameObject.SetActive(false);
        }
    }

    public void OnLifeChange(float newLife)
    {
        _lifebar.value = newLife;
    }

    public void OnDieParticles()
    {
        TextFactory.Instance.SpawnText(_enemy);
        var newParticle = GameObject.Instantiate(_vfxOnDie, _enemy.transform.position, Quaternion.identity);
        GameObject.Destroy(newParticle, newParticle.GetComponent<ParticleSystem>().main.duration);
    }

    public void OnAddIceDebuff()
    {
        if (!iceAct)
        {
            for (int i = 0; i < _enemy.debuffSprites.Count; i++)
            {
                if (_enemy.debuffSprites[i].gameObject.activeSelf == false)
                {
                    _enemy.debuffSprites[i].gameObject.SetActive(true);
                    _enemy.debuffSprites[i].sprite = UIManager.Instance.iceSprite;
                    _enemy.debuffSprites[i].GetComponent<Animator>().SetTrigger("Alpha");
                    iceIndex = i;
                    iceAct = true;
                    return;
                }
            }
        }
    }

    public void OnAddFireDebuff()
    {
        if (!fireAct)
        {
            for (int i = 0; i < _enemy.debuffSprites.Count; i++)
            {
                if (_enemy.debuffSprites[i].gameObject.activeSelf == false)
                {
                    _enemy.debuffSprites[i].gameObject.SetActive(true);
                    _enemy.debuffSprites[i].sprite = UIManager.Instance.fireSprite;
                    _enemy.debuffSprites[i].GetComponent<Animator>().SetTrigger("Alpha");
                    fireIndex = i;
                    fireAct = true;
                    return;
                }
            }            
        }
    }
    public void OnEndFireDebuff()
    {
        fireAct = false;
        _enemy.debuffSprites[fireIndex].gameObject.SetActive(false);
        //if(fireIndex == 0)
        //{
        //    if (_enemy.debuffSprites[1].gameObject.activeSelf)
        //    {
        //        iceIndex = 0;
        //        _enemy.debuffSprites[0].sprite = UIManager.Instance.iceSprite;
        //        _enemy.debuffSprites[0].gameObject.SetActive(true);
        //    }
        //}
    }

    public void OnEndIceDebuff()
    {
        _enemy.debuffSprites[iceIndex].gameObject.SetActive(false);
        iceAct = false;
        if (iceIndex == 0)
        {
            if (_enemy.debuffSprites[1].gameObject.activeSelf)
            {
                fireIndex = 0;
                _enemy.debuffSprites[0].sprite = UIManager.Instance.fireSprite;
                _enemy.debuffSprites[0].gameObject.SetActive(true);

            }
        }
    }
}
