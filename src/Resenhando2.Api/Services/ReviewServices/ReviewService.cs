using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Resenhando2.Api.Data;
using Resenhando2.Api.ResultViewModels;
using Resenhando2.Api.ResultViewModels.ReviewResultViews;
using Resenhando2.Core.Entities.Review;
using Resenhando2.Core.ValueObjects.Review;

namespace Resenhando2.Api.Services.ReviewServices;

public class ReviewService(DataContext context)
{
    public async Task<ResultViewModel<Review>> ReviewCreateAsync(ReviewCreateViewModel model)
    {
        try
        {
            var reviewText = ReviewText.Create(model.ReviewTitle, model.ReviewBody);
            var review = Review.Create(model.ReviewType, model.SpotifyId, reviewText, model.UserId);
            
            await context.Reviews.AddAsync(review);
            await context.SaveChangesAsync();

            return new ResultViewModel<Review>(review);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating review: {e.Message}");

            return new ResultViewModel<Review>("REV01 - Server internal error");
        }
    }
    
    public async Task<ResultViewModel<Review>> ReviewGetOneAsync(Guid id)
    {
        try
        {
            var review = await context.Reviews.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return review != null ? new ResultViewModel<Review>(review) :
                new ResultViewModel<Review>("Not found");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error fetching review: {e.Message}");
            return new ResultViewModel<Review>("REV02 - Server internal error");
        }
    }

    public async Task<ResultViewModel<List<Review>>> ReviewGetListAsync()
    {
        try
        {
            var reviewList = await context.Reviews.AsNoTracking().ToListAsync();
            return new ResultViewModel<List<Review>>(reviewList);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error fetching review list: {e.Message}");
            return new ResultViewModel<List<Review>>("REV03 - Server internal error");
        }
    }

    public async Task<ResultViewModel<Review>> ReviewUpdate(ReviewUpdateViewModel model)
    {
        try
        {
            var review = await context.Reviews.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (review == null)
                return new ResultViewModel<Review>("Not found");

            review.UpdateReviewText(model.ReviewTitle, model.ReviewBody);
            await context.SaveChangesAsync();
            
            return new ResultViewModel<Review>(review);
            
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error updating review : {e.Message}");
            return new ResultViewModel<Review>("REV04 - Server internal error");
        }
    }

    public async Task<ResultViewModel<Review>> ReviewDelete(Guid id)
    {
        try
        {
            var result = await context.Reviews.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return new ResultViewModel<Review>("Not found");

            context.Reviews.Remove(result);
            await context.SaveChangesAsync();

            return new ResultViewModel<Review>(result);

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error fetching review list: {e.Message}");
            return new ResultViewModel<Review>("REV05 - Server internal error");
        }
    }
}