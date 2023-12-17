using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationUIScript : MonoBehaviour
{
    public Animator bottomMenuAnimator;
    private string slideInAnimationBottom = "Bottom_Menu_Animation_SlideIn";
    private string slideOutAnimationBottom = "Bottom_Menu_Animation_SlideOut";

    public Animator leftMenuAnimator;
    private string slideInAnimationLeft = "Left_Menu_Animation_SlideIn";
    private string slideOutAnimationLeft = "Left_Menu_Animation_SlideOut";

    public Button bottomMenuButton;
    public Button leftMenuButton;

    private bool bottomMenuOpen;
    private bool leftMenuOpen;

    private void Start()
    {
        bottomMenuButton.onClick.AddListener(BottomAnimation);
        leftMenuButton.onClick.AddListener(LeftAnimation);

        bottomMenuAnimator.Play(slideOutAnimationBottom, -1, 1);
        leftMenuAnimator.Play(slideOutAnimationLeft, -1, 1);

        bottomMenuOpen = false;
        leftMenuOpen = false;
    }

    private void BottomAnimation()
    {
        if (!bottomMenuOpen)
        {
            bottomMenuAnimator.Play(slideInAnimationBottom);
            bottomMenuOpen = true;
        }
        else
        {
            bottomMenuAnimator.Play(slideOutAnimationBottom);
            bottomMenuOpen = false;
        }
    }

    private void LeftAnimation()
    {
        if (!leftMenuOpen)
        {
            leftMenuAnimator.Play(slideInAnimationLeft);
            leftMenuOpen = true;
        }
        else
        {
            leftMenuAnimator.Play(slideOutAnimationLeft);
            leftMenuOpen = false;
        }
    }
}
