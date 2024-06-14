using Unicv.Streaming.Api.Data.Entities;

namespace Unicv.Streaming.Api.Infra.Extensions;

public static partial class RatingExtensions
{
    public static decimal GetAverage(this Work obj, List<Rating> workRatings)
    {
        var rating = (decimal)0;

        if (workRatings.Count() > 0)
        {
            var sum = (decimal)workRatings.Sum(y => y.UserRating);
            rating = sum / workRatings.Count;
        }

        return rating;
    }
}
