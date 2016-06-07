using FeedBack.WepApi.Models;
using PathFinder.FeedBack.DAL.Model;
using System;

namespace FeedBack.WepApi.Mappers
{
    public static class FeedBackMapper
    {
        public static Comment CommentMapper(this CommentModel model)
        {
            return new Comment
            {
                Message = model.Message,
                Author = model.Author,
                Email = model.Email,
                CreatedAt = DateTime.Now
            };
        }
    }
}
