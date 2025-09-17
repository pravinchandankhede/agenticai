namespace Common;

using Microsoft.SemanticKernel;

public class KernelHelper
{
    public static Kernel GetKernel()
    {
        IKernelBuilder builder = Kernel.CreateBuilder();
        builder.AddAzureOpenAIChatCompletion(
            SharedLibrary.AppSetting.DeploymentName,
            SharedLibrary.AppSetting.Endpoint,
            SharedLibrary.AppSetting.Key);
        Kernel kernel = builder.Build();
        return kernel;
    }
}
