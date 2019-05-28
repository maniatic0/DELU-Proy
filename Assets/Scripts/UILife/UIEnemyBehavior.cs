using UnityEngine;
using UnityEngine.UI;

public class UIEnemyBehavior : MonoBehaviour
{
    public GameObject canv;
    public Text enemyLife;
    public Transform placeHolder;
    public float UIVisibleRatio = 0f;
    private Vector3 textPosition = Vector3.zero;
    private GameObject player;

    void Update()
    {
        //Check first if the player exist
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null){
            float distance = (player.transform.position - this.transform.position).magnitude;
            //Verify if the player is near to this enemy
            if (distance <= UIVisibleRatio){
                canv.SetActive(true);
            }
            else{
                canv.SetActive(false);
            }
        }

        //UI text position corresponds with placeHolder position
        textPosition = Camera.main.WorldToScreenPoint(placeHolder.position);
        enemyLife.transform.position = textPosition;
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(this.transform.position,UIVisibleRatio);
    }
}
