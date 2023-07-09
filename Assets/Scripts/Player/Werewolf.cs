using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Werewolf : MonoBehaviour
{
    [SerializeField] WerewolfUI _WerewolfUI;
    
    public delegate void OnHpChanged(int hp);
    public static event OnHpChanged onHpChanged;

    [SerializeField] GameObject deathOverlay;
    private PlayerCombat playerCombat;

    private int hp;

    void Start()
    {
        playerCombat = this.gameObject.GetComponent<PlayerCombat>();
        playerCombat.SetDamage(PlayerInfo.Singleton.damage);
        playerCombat.SetRange(PlayerInfo.Singleton.range);
        _WerewolfUI.SetupHealthBar(PlayerInfo.Singleton.hp);
        hp = PlayerInfo.Singleton.hp;
    }

    public void TakeDamage(int amount)
    {
        AudioManager.Singleton.Play("PlayerHit");
        // hit animation
        hp -= amount;
        if(hp <= 0)
        {
            AudioManager.Singleton.Play("Dead");
            deathOverlay.SetActive(true);
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            this.GetComponent<WerewolfMovement>().enabled = false;
            this.GetComponent<CircleCollider2D>().enabled = false;
            playerCombat.enabled = false;
            
            StartCoroutine(waitForYouDied());

            //get wrekt lmao
        }
        else if(onHpChanged != null)
        {
            onHpChanged(hp);
        }
    }
    
    IEnumerator waitForYouDied()
    {
        yield return new WaitForSeconds(2f);
        SceneLoader.Singleton.LoadScene("Menu");
    }

}
