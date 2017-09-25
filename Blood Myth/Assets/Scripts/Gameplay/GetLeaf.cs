using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLeaf : MonoBehaviour
    {
    public GameObject player;
    private Player _playerScript;
    private UnityEngine.UI.Text _numLeavesText;
    private int _leaves;

    private void Start()
        {
        this._playerScript = this.player.GetComponent<Player>();
        this._leaves = this._playerScript.getNumLeaves();
        this._numLeavesText = GameObject.FindWithTag("NumLeaves").GetComponent<UnityEngine.UI.Text>();
        }

    private void OnTriggerEnter2D(Collider2D inPlayer)
        {
        if (inPlayer.gameObject.tag == "Player")
            {
            this._playerScript.addLeaf();
            this._leaves = this._playerScript.getNumLeaves();
            this.gameObject.SetActive(false);
            
            this._numLeavesText.text = "x " + this._leaves.ToString();
            }
        }
    }