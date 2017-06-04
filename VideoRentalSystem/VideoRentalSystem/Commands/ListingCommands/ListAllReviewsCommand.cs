using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Commands.ListingCommands
{
    public class ListAllReviewsCommand : ICommand
    {
        private readonly IDatabase db;

        public ListAllReviewsCommand(IDatabase db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            var reviews = this.db.Reviews.GetAll();

            return string.Join("\n", reviews);
        }
    }
}