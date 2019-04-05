using System;
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
    public class FavoritesController : TEGramController
    {
        private IFavoriteDAO favoriteDAO;
        public FavoritesController(IFavoriteDAO favoriteDAO, IUserDAO userDao) : base(userDao)
        {
            this.favoriteDAO = favoriteDAO;
        }

        // POST: api/posts/{postId}/Favorite
        [HttpPost]
        [Authorize]
        public void Favorite([FromRoute]int postId)
        {
            favoriteDAO.FavorPostByUserId(postId, CurrentUser.Id);
        }

        // DELETE: api/posts/{postId}/Favorite
        [HttpDelete]
        [Authorize]
        public void Disfavorite([FromRoute]int postId)
        {
            favoriteDAO.DisfavorPostByUserId(postId, CurrentUser.Id);
        }



    }
}
