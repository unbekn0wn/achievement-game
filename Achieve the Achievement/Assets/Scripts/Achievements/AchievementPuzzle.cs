using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementPuzzle : AchievementBase
{
    public const Type AchievementType = Type.Major;

    public List<Button> Buttons;
    public Sprite OpenButtonSprite;
    public Sprite ClosedButtonSprite;
    public List<Sprite> NumberSpriteList;

    public int currentButton;

    void Start()
    {
        //Add Listeners to the ButtonPress functions of the Buttons
        Button.ButtonPressed += OnButtonPressed;
        Button.ButtonReleased += OnButtonReleased;

        //Current button is the first in the list and its active
        currentButton = 0;
        //Buttons[currentButton].ActiveButton = true;

        //Shuffle the button list
        for (int i = 0; i < Buttons.Count; i++)
        {
            Button temp = Buttons[i];
            int randomIndex = Random.Range(i, Buttons.Count);
            Buttons[i] = Buttons[randomIndex];
            Buttons[randomIndex] = temp;
        }

        //Put the correct numbers on the buttons.
        for (int i = 0; i < Buttons.Count; i++)
        {
            Buttons[i].Number.sprite = NumberSpriteList[i];
        }
    }

    public void OnButtonPressed(Button baseobj)
    {
        baseobj.GetComponent<SpriteRenderer>().sprite = ClosedButtonSprite;

        //If the button pressed equals the current button, set it on activated and select next currentbutton
        if (baseobj == Buttons[currentButton])
        {
            baseobj.ActiveButton = true;
            currentButton++;
        }
        //If its not the same, reset current button and reset all other buttons except the one you are standing on.
        else
        {
            currentButton = 0;
            foreach (Button btn in Buttons)
                if(btn.ActiveButton)
                {
                    btn.ActiveButton = false;
                    btn.GetComponent<SpriteRenderer>().sprite = OpenButtonSprite;
                }
        }
        
        //Check if all buttons are active, if not return.
        foreach(Button btn in Buttons)
            if(!btn.ActiveButton)
                return;

        //If the achievement isnt already completed, complete it.
        if (!Completed)
            OnComplete();
    }

    //On release of the button reset the sprite if it is not correctly pressed
    public void OnButtonReleased(Button baseobj)
    {
        //Check if the button is active, if not open the button again.
        if(!baseobj.ActiveButton)
            baseobj.GetComponent<SpriteRenderer>().sprite = OpenButtonSprite;
    }

    public override void OnComplete()
    {
        base.OnComplete();
        Button.ButtonPressed -= OnButtonPressed;
        Button.ButtonReleased -= OnButtonReleased;
    }
}
