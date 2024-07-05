using FinanceApp.Dtos.Comment;

namespace FinanceApp.Mappers;

public static class CommentMappers
{
    public static CommentDto ToCommentDto(this Comment commentModel)
    {
        return new CommentDto
        {
            Id = commentModel.Id,
            Title = commentModel.Title,
            Content = commentModel.Content,
            CreatedBy = commentModel.AppUser.UserName,
            CreatedOn = commentModel.CreatedOn,
            StockId = commentModel.StockId
        };
    }

    public static Comment ToCommentFromCreateCommentDto(this CreateCommentDto commentModel, int stockId)
    {
        return new Comment()
        {
            Content = commentModel.Content,
            Title = commentModel.Title,
            StockId = stockId
        };
    }

    public static Comment ToCommentFromUpdateCommentDto(this UpdateCommentRequestDto commentModel, int stockId)
    {
        return new Comment()
        {
            Content = commentModel.Content,
            Title = commentModel.Title,
            StockId = stockId
        };
    }
    
}