using Ninject;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Core.Contracts;

namespace VideoRentalSystem.Builder
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly IKernel kernel;

        public ServiceLocator(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public ICommand GetCommand(string commandName)
        {
            return this.kernel.Get<ICommand>(commandName);
        }
    }
}
