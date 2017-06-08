namespace VideoRentalSystem.Data.Postgre.Contracts
{
    public interface IDatabasePostgre
    {
        ILoanRepository Loans { get; }

        ITarifRepository Tarifs { get; }

        int Complete();
    }
}
