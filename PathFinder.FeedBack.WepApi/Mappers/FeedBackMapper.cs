﻿using FeedBack.WepApi.Models;
using PathFinder.FeedBack.DAL.Model;
using System;

namespace FeedBack.WepApi.Mappers
{
    public static class FeedBackMapper
    {
        public static Comment ToFeedBack(this FeedBackModel model)
        {
            return new Comment
            {
                Message = model.Message,
                Author = model.Author,
                Email = model.Email,
                CreatedAt = DateTime.Now
            };
        }

        public static FeedBackResponseModel ToFeedBackResponseModel(this Comment comment)
        {
            return new FeedBackResponseModel()
            {
                Author = comment.Author,
                Email = comment.Email,
                Id = comment.Id,
                Message = comment.Message
            };
        }
    }
}
