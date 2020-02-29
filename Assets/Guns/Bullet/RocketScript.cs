using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
   public GameObject projectile;
   public int nbOfProj=100;
   public GameObject Ply;

   public GameObject Ply1
   {
      get => Ply;
      set => Ply = value;
   }

   public void Start()
   {
      for (int i = 0; i < nbOfProj; i++)
      {
         GameObject tempBullet=Instantiate(projectile, transform.position, Quaternion.identity);
         tempBullet.GetComponent<BulletScript>().Ply = Ply;
      }
   }
}
