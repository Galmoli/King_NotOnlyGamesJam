using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;

    public static PlayerController Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<PlayerController>();
            return instance;
        }
    }
    
    public enum ArrowDirection
    {
        Up,
        Down,
        Right,
        Left
    }
    // Start is called before the first frame update
    [SerializeField] private Image player0;
    [SerializeField] private Image player1;
    [SerializeField] private Image player2;
    [SerializeField] private Image player3;

    [SerializeField] private SpawnerController _spawnerController;

    private Image player0CurrentArrow;
    private Image player1CurrentArrow;
    private ArrowDirection player0CurrentDir;
    private ArrowDirection player1CurrentDir;
    void Start()
    {
        if(_spawnerController.numOfArrows == 2) TwoPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Joystick1Vertical") > 0.1f) SetArrowDir(player0CurrentArrow, ArrowDirection.Up);
        if(Input.GetAxis("Joystick1Vertical") < -0.1f) SetArrowDir(player0CurrentArrow, ArrowDirection.Down);
        if(Input.GetAxis("Joystick1Horizontal") > 0.1f) SetArrowDir(player0CurrentArrow, ArrowDirection.Right);
        if(Input.GetAxis("Joystick1Horizontal") < -0.1f) SetArrowDir(player0CurrentArrow, ArrowDirection.Left);
        
        if(Input.GetAxis("Joystick2Vertical") > 0.1f) SetArrowDir(player1CurrentArrow, ArrowDirection.Up);
        if(Input.GetAxis("Joystick2Vertical") < -0.1f) SetArrowDir(player1CurrentArrow, ArrowDirection.Down);
        if(Input.GetAxis("Joystick2Horizontal") > 0.1f) SetArrowDir(player1CurrentArrow, ArrowDirection.Right);
        if(Input.GetAxis("Joystick2Horizontal") < -0.1f) SetArrowDir(player1CurrentArrow, ArrowDirection.Left);
    }
    
    private void TwoPlayers()
    {
        player0.gameObject.SetActive(false);
        player3.gameObject.SetActive(false);
        player0CurrentArrow = player1;
        player1CurrentArrow = player2;
    }

    private void SetArrowDir(Image image, ArrowDirection dir)
    {
        if (image == player0CurrentArrow) player0CurrentDir = dir;
        else player1CurrentDir = dir;
        
        switch (dir)
        {
            case ArrowDirection.Up:
            {
                image.rectTransform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            }
            case ArrowDirection.Down:
            {
                image.rectTransform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            }
            case ArrowDirection.Right:
            {
                image.rectTransform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            }
            case ArrowDirection.Left:
            {
                image.rectTransform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            }
        }
    }

    public ArrowDirection GetPlayer0Dir()
    {
        return player0CurrentDir;
    }
    
    public ArrowDirection GetPlayer1Dir()
    {
        return player1CurrentDir;
    }
    
}
