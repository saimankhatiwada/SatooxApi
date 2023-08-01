using Api.utils;
using BussinessLogic.IRepositories;
using Carter;
using Contracts.HttpResults;
using Data.Blogs;

namespace Api.Endpoints.Blogs;

public class BlogModule : CarterModule
{
    private readonly IRepository<Blog> _blogRepository;

    public BlogModule(IRepository<Blog> blogRepository)
    {
        _blogRepository = blogRepository;
        this.RequireAuthorization();
    }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/blog", async () => {

            IEnumerable<Blog> blogs = await _blogRepository.GetAllAsync();
            if(!blogs.Any())
            {
                Failure Errorresponse = new Failure{
                    Code = StatusCodes.Status204NoContent,
                    ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.BlogNotFound)
                };
                return Results.BadRequest(Errorresponse);
            }
            return Results.Ok(blogs);
        });

        app.MapGet("/blog/{id:Guid}/get", async (Guid id) => {

            Blog? blog = await _blogRepository.GetByIdAsync(id);
            if (blog is null)
            {
                Failure Errorresponse = new Failure{
                    Code = StatusCodes.Status400BadRequest,
                    ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.InvalidBlogId)
                };
                return Results.BadRequest(Errorresponse);
            }
            return Results.Ok(blog);
        });
    }
}
