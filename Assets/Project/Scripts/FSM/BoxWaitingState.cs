using AxGrid;
using AxGrid.FSM;

namespace LootBox
{
    [State(ConsatantStrings.S_WAITING_STATE)]
    public class BoxWaitingState : FSMState
    {
        [Enter]
        private void EnterThis()
        {
        }

        private void ChangeState()
        {
            Parent.Change(ConsatantStrings.S_ROLLING_STATE);
        }


        [Exit]
        private void ExitThis()
        {
        }
    }
}




