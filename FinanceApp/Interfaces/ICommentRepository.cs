namespace FinanceApp.Interfaces;

public interface ICommentRepository
{
     Task<List<Comment>> GetCommentsAsync();
     Task<Comment?> GetCommentByIdAsync(int id);
     Task<Comment> CreateComment(Comment comment);
     Task<Comment?> UpdateCommentAsync(int id, Comment updatedComment);
     Task<Comment?> DeleteComment(int id);

}