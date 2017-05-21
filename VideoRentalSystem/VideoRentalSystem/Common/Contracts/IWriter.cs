namespace VideoRentalSystem.Common.Contracts
{
    public interface IWriter
    {
        string Write(string message);

        string WriteLine(string message);
    }
}
