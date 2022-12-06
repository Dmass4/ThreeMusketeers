using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    private GameObject CurrentPlacingTower;
    private Player player;
    public CustomCursor customCursor;

    // Start is called before the first frame update
    void Start()
    {
        //Get Player gameObject in order to update goldCount when towers are placed
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // Detect if currentPlacingTower is assigned (not null)
        if (CurrentPlacingTower != null)
        {
            // Mouse left click triggers placement/creation of tower
            // Update variables to deactivate customCursor and reenable default/regular cursor
            // Update goldCount associated with Player class/gameObject
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Vector2 mousePosition = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(CurrentPlacingTower, mousePosition, Quaternion.identity);

                CurrentPlacingTower = null;
                Cursor.visible = true;
                customCursor.gameObject.SetActive(false);
                player.GetComponent<Player>().subtractGold(30);

                Debug.Log("Tower Placed!");
            }
        }
    }

    public void SetTowerToPlace(GameObject tower)
    {
        CurrentPlacingTower = tower;
        // Activate custom cursor, assign tower sprite, and disable default/normal cursor
        // This method is triggered by the TowerButtonPannel's button
        // to give the player a sense of where the tower will be placed according to cursor movement
        customCursor.gameObject.SetActive(true);
        customCursor.GetComponent<SpriteRenderer>().sprite = CurrentPlacingTower.GetComponent<SpriteRenderer>().sprite;
        Cursor.visible = false;

        Debug.Log("Tower prepped!");
    }
}