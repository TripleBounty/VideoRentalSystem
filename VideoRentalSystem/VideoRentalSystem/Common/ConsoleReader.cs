using System;
using VideoRentalSystem.Common.Contracts;

namespace VideoRentalSystem.Common
{
    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
