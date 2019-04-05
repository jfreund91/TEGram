using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEGram.DAL;
using TEGram.Models;

namespace TEGramApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : TEGramController
    {
        private IPostDAO postDAO;
        public UsersController(IPostDAO postDAO, IUserDAO userDao) : base(userDao)
        {
            this.postDAO = postDAO;
        }

        /// <summary>
        /// Gets a user's info and posts given the username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>UserPostViewModel</returns>
        // GET: api/Users/{username}
        [HttpGet("{username}", Name = "Get")]
        public UserPostsViewModel Get(string username)
        {
            User searchResult = this.userDao.GetUser(username);
            
            IList<Post> usersPosts = this.postDAO.GetAllPostsByUserId(searchResult.Id);

            UserPostsViewModel output = new UserPostsViewModel()
            {
                Username = searchResult.Username,
                Image = searchResult.Image,
                UserPosts = usersPosts
            };
            return output;
        }
    }
}
