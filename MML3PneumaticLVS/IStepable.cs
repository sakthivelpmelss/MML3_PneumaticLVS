using MML3PneumaticLVS.CustomDataType;

namespace MML3PneumaticLVS
{
    public interface IStepable
    {
        void Step(params Signal[] signals);
    }
}
