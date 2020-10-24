﻿using Kuhpik.Pooling;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kuhpik
{
    [DefaultExecutionOrder(500)]
    public class Bootstrap : MonoBehaviour
    {
        private const string saveKey = "saveKey";

        [SerializeField] private GameConfig config;

        private static PlayerData playerData;
        private static FSMProcessor<GameState> fsm;
        private static Dictionary<Type, GameSystem> systems;

        private void Start()
        {
            InitSystems();
        }

        private void Update()
        {
            if (fsm.State.IsInited)
            {
                for (int i = 0; i < fsm.State.RunningSystems.Length; i++)
                {
                    fsm.State.RunningSystems[i].OnRun();
                }
            }
        }

        public static void GameRestart(int sceneIndex)
        {
            foreach (var system in systems.Values)
            {
                (system as IGameSystem).PerformAction<IDisposing>();
            }

            SaveExtension.Save(playerData, saveKey);
            SceneManager.LoadScene(sceneIndex);
            PoolingSystem.Clear();
        }

        public static void ChangeGameState(EGamestate type)
        {
            fsm.State.Deactivate();
            fsm.ChangeState(type.GetName());
            fsm.State.Activate();
        }

        public static T GetSystem<T>() where T : class
        {
            return systems[typeof(T)] as T;
        }

        private void InitSystems()
        {
            CreatePools();
            ResolveSystems();
            LoadPlayerData();
            HandleGameStates();
            HandleInjections();
            HandleCamerasFOV();

            fsm.State.Activate();
        }

        private void ResolveSystems()
        {
            systems = FindObjectsOfType<GameSystem>().ToDictionary(system => system.GetType(), system => system);
        }

        private void HandleGameStates()
        {
            fsm = GetComponentInChildren<GameStateInstaller>().InstallGameStates();
        }

        private void LoadPlayerData()
        {
            playerData = GetComponentInChildren<PlayerDataInstaller>().InstallData(saveKey);
        }

        private void HandleInjections()
        {
            GetComponentInChildren<InjectionsInstaller>().Inject(systems.Values, config, playerData, new GameData());
        }

        private void HandleCamerasFOV()
        {
            GetComponentInChildren<CameraInstaller>().Process();
        }

        private void CreatePools()
        {
            GetComponentInChildren<PoolInstaller>().Init();
        }
    }
}