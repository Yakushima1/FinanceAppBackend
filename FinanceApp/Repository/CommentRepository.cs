using FinanceApp.Data;
using FinanceApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDBContext _context;

    public CommentRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<List<Comment>> GetCommentsAsync()
    {
        return await _context.Comments.Include(c=>c.AppUser).ToListAsync();
    }

    public async Task<Comment?> GetCommentByIdAsync(int id)
    {
        return await _context.Comments.Include(c => c.AppUser)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
            
    public async Task<Comment> CreateComment(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> UpdateCommentAsync(int id, Comment updatedComment)
    {
        var comment = await _context.Comments.FindAsync(id);
        comment.Content = updatedComment.Content;
        comment.Title = updatedComment.Title;
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> DeleteComment(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        return comment;
    }
}