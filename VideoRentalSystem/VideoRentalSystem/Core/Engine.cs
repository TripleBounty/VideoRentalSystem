using System;
using System.Text;
using VideoRentalSystem.Common.Contracts;
using VideoRentalSystem.Core.Contracts;

namespace VideoRentalSystem.Core
{
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;
        private IProcessor processor;

        public Engine(IReader reader, IWriter writer, IProcessor processor)
        {
            this.reader = reader;
            this.writer = writer;
            this.processor = processor;
        }

        public void Start()
        {
            var builder = new StringBuilder();

            while (true)
            {
                var commandLine = this.reader.ReadLine();

                if (commandLine.ToLower() == "exit")
                {
                    this.writer.Write(builder.ToString());
                    this.writer.WriteLine("Program terminated.");
                    break;
                }

                try
                {
                    var executionResult = this.processor.ProcessCommand(commandLine);
                    builder.AppendLine(executionResult);
                }
                catch (Exception ex)
                {
                    builder.AppendLine("Opps, something happened. :( /n" +
                        "" + ex.Message);
                }
            }
        }
    }
}