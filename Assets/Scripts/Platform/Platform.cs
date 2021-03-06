﻿using System;
using System.Collections.Generic;
using Character.Al;
using UnityEngine;
using Random = UnityEngine.Random;
using Type = Upgrades.Type;

namespace Platform
{
	public class Platform : MonoBehaviour
	{
		[SerializeField] 
		private Zombie zombiePrefab;

		[SerializeField] 
		private Vector2 size;

		public Transform powerUpSpawnPoint;
		
		//settings
		private int baseZombieCount = 3;

		private int randomZombieMaxCount = 4;
		private Type powerUpType = Type.None;
		private readonly List<Zombie> zombies = new List<Zombie>();

		public bool HavePowerUp => powerUpType != Type.None;


		private void SpawnZombie()
		{
			Vector3 myPosition = transform.position;
			zombies.Add(Instantiate(zombiePrefab,
				new Vector3(myPosition.x + Random.Range(-size.x, size.x), myPosition.y + 1,
					myPosition.z + Random.Range(-size.y, size.y)), Quaternion.identity));
		}

		#region Collision

		private void OnCollisionEnter(Collision other)
		{
			if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
			{
				if (zombies.Count < 5)
				{
					SpawnZombie();
				}

				foreach (var zombie in zombies)
				{
					zombie.Attack(other.gameObject.transform);
				}
			}
		}

		private void OnCollisionExit(Collision other)
		{
			if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
			{
				foreach (var zombie in zombies)
				{
					zombie.LoseFocus();
				}
			}
		}
		
		#endregion

		public void PowerUp(Type type)
		{
			powerUpType = type;
		}
	}
}