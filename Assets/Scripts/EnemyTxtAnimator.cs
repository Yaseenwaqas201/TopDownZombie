using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTxtAnimator : MonoBehaviour
{
   public Transform damageTxtEndPos;
   public Transform damageTxtStartPos;

   public Text damageTxt;
   

   public void AnimateTxt(float damageValue,Transform parentOfDamageTxt)
   {
      // Code for Animating Damage Point
      gameObject.SetActive(true);
      gameObject.transform.SetParent(parentOfDamageTxt.parent);
      damageTxt.text = damageValue +"";
      damageTxt.transform.DOMove(damageTxtEndPos.position, 0.2f);
      damageTxt.transform.localScale=new Vector3(0.6f,0.6f,0.6f);
      damageTxt.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
      damageTxt.DOFade(.7f, .2f).OnComplete(() =>
      {
         gameObject.SetActive(false);
         damageTxt.DOFade(1f, 0);
         transform.SetParent(parentOfDamageTxt);
         damageTxt.transform.localPosition = damageTxtStartPos.localPosition;
         transform.localPosition = Vector3.zero;
         damageTxt.transform.localScale = Vector3.one;
      });

   }
}
