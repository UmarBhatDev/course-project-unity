using System.Linq;
using Features.Roadmap.Data;
using UnityEngine;

namespace Features.Persistence.Services
{
    public class JourneyProgress
    {
        private const string StageStatus_Key = "Stage {0}";

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
            {
                Debug.LogWarning($"[JourneyProgress] Saved activeStage is null. Applying default value:");
            }
            else
            {
                Debug.Log($"[JourneyProgress] Saved activeStage is {activeStage.Id}");
            }

            return activeStage;
        }

        private StageStatus GetStageStatus(string stageID)
        {
            var key = string.Format(StageStatus_Key, stageID);
            return (StageStatus) PlayerPrefs.GetInt(key, defaultValue: (int) StageStatus.Unvisited);
        }

        public void MarkStageVisited(string stageID)
        {
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
            var key = string.Format(StageStatus_Key, stageID);
            PlayerPrefs.SetInt(key, (int) status);
        }
    }
}