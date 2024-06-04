using System.Collections.Generic;
using System.Linq;
using Features.Roadmap.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace Features.Persistence.Services
{
    public class JourneyProgress
    {
        private const string STAGES_STATUS_KEY = "StagesStatusKey";

        private readonly RoadmapRegistry _roadmapRegistry;

        public JourneyProgress(RoadmapRegistry roadmapRegistry)
        {
            _roadmapRegistry = roadmapRegistry;
        }

        public Stage GetActiveStage()
        {
            var roadmap = _roadmapRegistry.Roadmap;

            var activeStage =
                roadmap.Stages.FirstOrDefault(
                    stage => GetStageStatus(stage.Id) == StageStatus.Active) ??
                roadmap.Stages.FirstOrDefault(
                    stage => GetStageStatus(stage.Id) == StageStatus.Unvisited);

            if (activeStage == null)
                Debug.LogWarning($"[JourneyProgress] Saved activeStage is null. Applying default value:");
            else Debug.Log($"[JourneyProgress] Saved activeStage is {activeStage.Id}");

            return activeStage;
        }

        public Dictionary<string, StageStatus> GetStageStatuses()
        {
            var stageStatusesJson = PlayerPrefs.GetString(STAGES_STATUS_KEY, "{}");
            var stageStatuses = JsonConvert.DeserializeObject<Dictionary<string, StageStatus>>(stageStatusesJson);

            return stageStatuses ?? new Dictionary<string, StageStatus>();
        }

        public void MarkStageVisited(string stageID)
        {
            var isAlreadyVisited = GetStageStatus(stageID) == StageStatus.Visited;
            if (isAlreadyVisited) return;
            
            var roadmap = _roadmapRegistry.Roadmap;

            for (var index = 0; index < roadmap.Stages.Count; index++)
            {
                var currentStageId = roadmap.Stages[index].Id;

                if (currentStageId == stageID)
                {
                    SetStageStatus(currentStageId, StageStatus.Visited);

                    if (roadmap.Stages.Count > index + 1)
                        SetStageStatus(roadmap.Stages[index + 1].Id, StageStatus.Active);
                }
            }
        }

        public void SetStageStatus(string stageID, StageStatus status)
        {
            var stageStatuses = GetStageStatuses();

            stageStatuses[stageID] = status;

            SaveStageStatuses(stageStatuses);   
        }

        public StageStatus GetStageStatus(string stageID)
        {
            var stageStatuses = GetStageStatuses();

            return stageStatuses.ContainsKey(stageID) ? stageStatuses[stageID] : StageStatus.Unvisited;
        }

        private static void SaveStageStatuses(Dictionary<string, StageStatus> stageStatuses)
        {
            var stageStatusesJson = JsonConvert.SerializeObject(stageStatuses);
            PlayerPrefs.SetString(STAGES_STATUS_KEY, stageStatusesJson);
        }
    }
}