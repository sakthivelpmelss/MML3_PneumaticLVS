using MML3PneumaticLVS.CustomDataType;

namespace MML3PneumaticLVS
{
    public delegate void StepDelegate(Pipe pipe1, Pipe pipe2, Pipe pipe3, Pipe pipe4, int command);

    public static class ComponentController
    {
        // Dictionary to store the step functions for each component
        private static readonly Dictionary<string, StepDelegate> componentSteps = new Dictionary<string, StepDelegate>();

        // Method to register a component with its step function
        public static void RegisterComponent(string componentName, StepDelegate stepFunction)
        {
            if (!componentSteps.ContainsKey(componentName))
            {
                componentSteps.Add(componentName, stepFunction);
            }
        }

        // Method to execute the step function for a specific component
        public static void ExecuteStep(string componentName, Pipe pipe1, Pipe pipe2, Pipe pipe3, Pipe pipe4, int command)
        {
            if (componentSteps.ContainsKey(componentName))
            {
                componentSteps[componentName].Invoke(pipe1, pipe2, pipe3, pipe4, command);
            }
            else
            {
                Console.WriteLine($"Component {componentName} is not registered.");
            }
        }
    }

}
