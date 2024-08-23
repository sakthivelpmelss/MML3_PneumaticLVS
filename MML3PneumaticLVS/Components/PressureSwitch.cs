using MML3PneumaticLVS.CustomDataType;

namespace MML3PneumaticLVS.Components
{
    public class PressureSwitch : ControlValve
    {
        public override void Step(Pipe pipe1, Pipe pipe2, Pipe pipe3, Pipe pipe4, int command)
        {
            // Calculate pressure for each pipe considering the leak
            pipe1.CalculatePressure();
            pipe2.CalculatePressure();
            pipe3.CalculatePressure();
            pipe4.CalculatePressure();

            float reductionRate = Math.Min(pipe1.pressureReductionRate, Math.Min(pipe2.pressureReductionRate, Math.Min(pipe3.pressureReductionRate, pipe4.pressureReductionRate)));

            if (command == 0)
            {
                pipe3.pressureValue = Math.Max(pipe1.pressureValue, pipe2.pressureValue) - reductionRate;
                pipe3.pressureValue = Math.Max(0, pipe3.pressureValue); // Ensures no negative pressure
            }
            else if (command == 1)
            {
                pipe4.pressureValue = Math.Max(pipe1.pressureValue, pipe3.pressureValue) - reductionRate;
                pipe4.pressureValue = Math.Max(0, pipe4.pressureValue); // Ensures no negative pressure
            }
        }

        //public override void Step(Pipe pNO, Pipe pNC, Pipe pCOMMON, Signal sCOMMAND)
        //{
        //    float nNO = pNO.pressureValue;
        //    float nNC = pNC.pressureValue;
        //    float nCOMMON = pCOMMON.pressureValue;

        //    if (sCOMMAND.Etat == 1)
        //    {

        //        if (nNO > nNC)
        //        {
        //            if (nNO > nCOMMON)
        //            {
        //                pCOMMON.pressureValue = nNO;
        //                pCOMMON.PreState = PipeState.Leak;
        //                pCOMMON.State = PipeState.Good;
        //                pNO.PreState = pCOMMON.State;
        //                pNO.State = pCOMMON.State;
        //            }
        //            else
        //            {
        //                pNO.pressureValue = nCOMMON;
        //                pNO.PreState = PipeState.Leak;
        //                pNO.State = PipeState.Good;
        //                pCOMMON.PreState = pNO.State;
        //                pCOMMON.State = pNO.State;
        //            }
        //        }
        //        else
        //        {
        //            if (nNC > nCOMMON)
        //            {
        //                pCOMMON.pressureValue = nNC;
        //            }
        //            else
        //            {
        //                pNC.pressureValue = nCOMMON;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        // Handle the command == 0 case, e.g., do nothing or reset pressures

        //        pCOMMON.State = pCOMMON.PreState;
        //        pCOMMON.pressureValue = 0;
        //    }
        //}
    }
}
