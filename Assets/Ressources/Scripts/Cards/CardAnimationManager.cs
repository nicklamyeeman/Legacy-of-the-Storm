using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class CardAnimationManager : MonoBehaviour
{
    [SerializeField] private GameObject costUI;
    [SerializeField] private GameObject nameUI;
    [SerializeField] private GameObject descUI;
    [SerializeField] private GameObject cardArt;

    private Animation anim;
    private Animator animator;
    private float animationTime;

    private GameObject cardHolder;
    private CardManager cardManager;
    private PlayerManager player;

    public RuntimeAnimatorController animatorController;

    //Tmp
    [SerializeField] private Image background;

    void Awake()
    {
        DeactiveUIElementEvent();

        anim = GetComponent<Animation>();
        animator = GetComponent<Animator>();

        cardManager = GetComponent<CardManager>();
        player = GameObject.FindGameObjectWithTag("PlayerTMP").GetComponent<PlayerManager>();
    }

    public void LaunchAnimation(GameObject holder, String animationType, float newAnimationTime)
    {
        cardHolder = holder;
        animationTime = newAnimationTime;
        animator.SetTrigger(animationType);
    }

    private void CardToHandAnim()
    {
        AnimationCurve curveX;
        AnimationCurve curveY;
        AnimationClip clip = new AnimationClip();

        clip.legacy = true;
        gameObject.transform.SetParent(cardHolder.transform);

        curveX = CreateCurve(transform.localPosition.x, 0, animationTime);
        curveY = CreateCurve(transform.localPosition.y, 0, animationTime);

        clip.SetCurve("", typeof(Transform), "localPosition.x", curveX);
        clip.SetCurve("", typeof(Transform), "localPosition.y", curveY);
        clip.AddEvent(CreateEvent(animationTime, "ChangeObjectParentEvent"));

        AddAndPlayAnimation(clip, "CardToPosition");
    }

    public void CardToDiscardAnim(GameObject newHolder, float discardAnimationTime)
    {
        AnimationCurve curveX;
        AnimationCurve curveY;
        AnimationCurve curveScaleX;
        AnimationCurve curveScaleY;
        AnimationCurve curveRotZ;
        AnimationClip clip = new AnimationClip();

        cardHolder = newHolder;

        background.color = cardHolder.GetComponent<Image>().color;

        clip.legacy = true;
        gameObject.transform.SetParent(cardHolder.transform);

        curveX = CreateCurve(transform.localPosition.x, 0, discardAnimationTime);
        curveY = CreateCurve(transform.localPosition.y, 0, discardAnimationTime);
        curveScaleX = CreateCurve(transform.localScale.x, 0.82f, discardAnimationTime);
        curveScaleY = CreateCurve(transform.localScale.y, 0.82f, discardAnimationTime);
        curveRotZ = CreateCurve(transform.localRotation.z, cardHolder.transform.localRotation.z, discardAnimationTime);

        clip.SetCurve("", typeof(Transform), "localPosition.x", curveX);
        clip.SetCurve("", typeof(Transform), "localPosition.y", curveY);
        clip.SetCurve("", typeof(Transform), "localScale.x", curveScaleX);
        clip.SetCurve("", typeof(Transform), "localScale.y", curveScaleY);
        clip.SetCurve("", typeof(Transform), "localRotation.y", curveRotZ);
        //clip.AddEvent(CreateEvent(animationTime, "ChangeObjectParentEvent"));

        AddAndPlayAnimation(clip, "CardToDiscard");
        cardManager.CardState = CardManager.E_State.IN_DISCARD;
        DeactiveUIElementEvent();
    }


    private AnimationCurve CreateCurve(float initPos, float finalPos, float curveAnimationTime)
    {
        AnimationCurve curve;
        Keyframe[] keys = new Keyframe[2];

        keys[0] = new Keyframe(0.0f, initPos);
        keys[1] = new Keyframe(curveAnimationTime, finalPos);

        curve = new AnimationCurve(keys);
        return curve;
    }

    private void AddAndPlayAnimation(AnimationClip clip, String animationName)
    {
        animator.runtimeAnimatorController = null;
        anim.AddClip(clip, animationName);
        anim.Play(animationName);
    }

    /*
     * 
     * EVENTS
     * 
     */

    private AnimationEvent CreateEvent(float eventTime, String eventName)
    {
        AnimationEvent evt = new AnimationEvent();

        evt.time = eventTime;
        evt.functionName = eventName;
        return evt;
    }

    private void DeactiveUIElementEvent()
    {
        cardArt.SetActive(false);
        costUI.SetActive(false);
        nameUI.SetActive(false);
        descUI.SetActive(false);
    }

    private void ActivateUIElementEvent()
    {
        cardArt.SetActive(true);
        costUI.SetActive(true);
        nameUI.SetActive(true);
        descUI.SetActive(true);
    }

    private void ChangeObjectParentEvent()
    {
        cardManager.CardState = CardManager.E_State.IN_HAND;
        player.DrawnCards = player.DrawnCards.RemoveFront();
        player.HandCards.Add(gameObject);
    }
}
