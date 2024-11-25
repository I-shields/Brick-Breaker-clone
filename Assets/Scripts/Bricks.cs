//============================================================
// Author: Isaac Shields
// Date  : 11-24-2024
// Desc  : Controls brick settings
//============================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Bricks : MonoBehaviour
{
    public int brickType;
    public GameMaster gm;
    private int brickHealth = 1;

    //type1 = coin dropper - drops coin
    //type2 = reinforced - drops heart
    //type3 = ice dropper - drops ice
    //other = base

    private void Awake() {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMaster>();
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.gameObject.tag == "BallObject")
        {

            if(brickType == 2 && brickHealth == 0)
            {
                gm.blocks.RemoveAt(gm.blocks.Count - 1);
                startEffects();
                Destroy(gameObject);
            }
            else if(brickType == 2 && brickHealth != 0)
            {
                brickHealth--;
            }

            else if(brickType == 1)
            {
                gm.blocks.RemoveAt(gm.blocks.Count - 1);
                startEffects();
                Destroy(gameObject);
            }

            else if(brickType == 3)
            {
                gm.blocks.RemoveAt(gm.blocks.Count - 1);
                startEffects();
                Destroy(gameObject);

                int randnum = Random.Range(0, gm.blocks.Count);
                if(randnum < gm.blocks.Count && gm.blocks[randnum] != null)
                {
                    Destroy(gm.blocks[randnum]);
                    gm.blocks.RemoveAt(gm.blocks.Count - 1);
                }
            }

            else
            {
                gm.blocks.RemoveAt(gm.blocks.Count - 1);
                startEffects();
                Destroy(gameObject);
            }

        }
    }

    //some random stuff that happens
    private void startEffects()
    {
        if(brickType == 2)
        {
            gm.spawnHealth(gameObject.transform.position);
            gm.playerScore += 500;
        }
        else if(brickType == 3)
        {
            gm.spawnIce(gameObject.transform.position);
            gm.playerScore += 100;
        }
        else if(brickType == 1)
        {
            gm.spawnPoints(gameObject.transform.position);
            gm.playerScore += 750;
        }
        else
        {
            gm.playerScore += 250;
        }
    }

    //update sprite
    public void updateSprite()
    {
        if(brickType == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("type3");
        }
        else if(brickType == 2)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("type1");
        }
        else if(brickType == 3)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("type2");
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("BaseType");
        }
    }
}
