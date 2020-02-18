using System.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	// 单例
	public static GameManager main;

	// 球预制体
	public GameObject ballPrefab;

	// x边界
	public float xBound = 3f;
	// y边界
	public float yBound = 3f;
	// 球速度
	public float ballSpeed = 3f;
	// 重新生成延迟
	public float respawnDelay = 2f;
	// 玩家分数
	public int[] playerScores;

	// 主要文本
	public Text mainText;
	// 玩家文本
	public Text[] playerTexts;

	// 球实体预制体
	Entity ballEntityPrefab;
	// 实体管理者
	EntityManager manager;

	// 延迟一秒
	WaitForSeconds oneSecond;
	// 延迟
	WaitForSeconds delay;

	private void Awake()
	{
		if (main != null && main != this)
		{
			Destroy(gameObject);
			return;
		}

		main = this;
		playerScores = new int[2];

		manager = World.DefaultGameObjectInjectionWorld.EntityManager;
		GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
		ballEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(ballPrefab, settings);

		oneSecond = new WaitForSeconds(1f);
		delay = new WaitForSeconds(respawnDelay);

		StartCoroutine(CountdownAndSpawnBall());
	}

	/// <summary>
	/// 玩家得分
	/// </summary>
	/// <param name="playerID"></param>
	public void PlayerScored(int playerID)
	{
		playerScores[playerID]++;
		for (int i = 0; i < playerScores.Length && i < playerTexts.Length; i++)
			playerTexts[i].text = playerScores[i].ToString();

		StartCoroutine(CountdownAndSpawnBall());
	}

	/// <summary>
	/// 计数，并开始生成球
	/// </summary>
	IEnumerator CountdownAndSpawnBall()
	{
		mainText.text = "Get Ready";
		yield return delay;

		mainText.text = "3";
		yield return oneSecond;

		mainText.text = "2";
		yield return oneSecond;

		mainText.text = "1";
		yield return oneSecond;

		mainText.text = "";

		SpawnBall();
	}

	void SpawnBall()
	{
		Entity ball = manager.Instantiate(ballEntityPrefab);
		Vector3 dir = new Vector3(UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1, UnityEngine.Random.Range(-0.5f, 0.5f), 0f).normalized;
		Vector3 speed = dir * ballSpeed;
		PhysicsVelocity velocity = new PhysicsVelocity()
		{
			Linear = speed,
			Angular = float3.zero
		};
		manager.AddComponentData(ball, velocity);
	}
}

