using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Commands.RemoveCommands
{
    public class RemoveReviewCommand : ICommand
    {
        private readonly IDatabase db;

        public RemoveReviewCommand(IDatabase db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 1)
            {
                return "Not valid number of parameters";
            }

            if (parameters.Any(x => x == string.Empty))
            {
                return "Some of the passed parameters are empty!";
            }

            int reviewId;
            var reviewIdParsed = int.TryParse(parameters[0], out reviewId);
            if (!reviewIdParsed)
            {
                return "Not Valid Review Id. Fill in numeric value!";
            }

            var review = this.db.Reviews.SingleOrDefault(e => e.Id == reviewId);
            if (review == null)
            {
                return "Review with such id doesn't exist!";
            }

            this.db.Reviews.Remove(review);

            this.db.Complete();

            return "Review removed";
        }
    }
}