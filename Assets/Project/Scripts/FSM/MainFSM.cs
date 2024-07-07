using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using UnityEngine;

namespace LootBox
{
    public class MainFSM : MonoBehaviourExt
    {

        [OnStart]
        private void StartThis()
        {
            Settings.Fsm = new FSM();
            Settings.Fsm.Add(new MainInitState());
            Settings.Fsm.Add(new BoxWaitingState());
            Settings.Fsm.Add(new BoxRollingState());
            Settings.Fsm.Add(new BoxGettingRewardState());
            Settings.Fsm.Start(ConsatantStrings.S_INIT_STATE);
        }

        [OnUpdate]
        private void UpdateThis()
        {
            Settings.Fsm.Update(Time.deltaTime);
            Settings.Model?.EventManager.Invoke(ConsatantStrings.E_EXECUTE_BOX_STATE);
        }

        [OnDestroy]
        private void Clear()
        {
            Model?.EventManager.Remove(ConsatantStrings.E_ON_START_ROLL_CLICK);
            Model?.EventManager.Remove(ConsatantStrings.E_ON_STOP_ROLL_CLICK);
        }
    }
}

