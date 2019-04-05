﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEGram.DAL;
using TEGram.Models;

namespace TEGramApi.Controllers
{
    [Route("api/posts/{postId:int}/[controller]")]
    [ApiController]
    public class LikesController : TEGramController
    {
        private ILikeDAO likeDAO;
        public LikesController(ILikeDAO likeDAO, IUserDAO userDao) : base(userDao)
        {
            this.likeDAO = likeDAO;
        }

        // GET: api/posts/{postId}/Likes
        [HttpGet(Name ="GetLikes")]
        [Authorize]
        public IEnumerable<User> Get([FromRoute]int postId)
        {
            return likeDAO.GetAllLikesByPostId(postId);
        }

        // POST: api/posts/{postId}/Likes
        [HttpPost]
        [Authorize]
        public int Like([FromRoute]int postId)
        {
            return likeDAO.LikePostByUserId(postId, CurrentUser.Id);
        }

        // DELETE: api/posts/{postId}/Likes
        [HttpDelete]
        [Authorize]
        public int Unlike([FromRoute]int postId)
        {
            return likeDAO.UnlikePostByUserId(postId, CurrentUser.Id);
        }



    }
}
