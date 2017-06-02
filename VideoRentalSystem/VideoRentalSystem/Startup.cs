using Ninject;
using VideoRentalSystem.Builder;
using VideoRentalSystem.Core.Contracts;

namespace VideoRentalSystem
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new BuildManager());
            IEngine engine = kernel.Get<IEngine>();

            engine.Start();
        }
    }
}
