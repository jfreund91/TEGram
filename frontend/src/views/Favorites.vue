<!-- 
  The Favorites Page should display a list of all a given user's favorite posts by username of poster.
  NOTES:
  - Upon creation, it calls api/favorites and uses the response to extract out all unique usernames of posters.
  - The component loops through the users (ascending order) and renders a post-author and post-grid 
    for each post favorited for that user.
  DEPENDENCIES:
    PostAuthor.vue
    PostGrid.vue 
-->

<template>
  <div id="favorites" class="container">
    <div class="favorite-user" v-for="user in users" v-bind:key="user.id">
      <post-author :userName="user.userName" :userImage="user.posts[0].userImage"/>
      <post-grid :posts="user.posts"/>
    </div>
  </div>
</template>

<script>
import PostAuthor from "@/components/posts/PostAuthor.vue";
import PostGrid from "@/components/posts/PostGrid.vue";
import auth from "@/shared/auth.js";

export default {
  name: "favorites",
  components: {
    PostAuthor,
    PostGrid
  },
  data() {
    return {
      users: [],
      posts: Array
    };
  },
  created() {
    // Call the API to get the user's favorites
    fetch(`${process.env.VUE_APP_REMOTE_API}/favorites`, {
      // method: 'GET',
      headers: {
              "Authorization": 'Bearer ' + auth.getToken() 
            }, 
    })
    .then((response) => {
      return response.json();
    }).then ((json) => {
      console.log(JSON.stringify(json));      
      this.posts = json;
      this.uniqueUsers();
    })
  },
  methods: {
    findUser(userName) {
      let theUser = null;
      this.users.forEach(user => {
        if (user.userName === userName) {
          theUser = user;
        }
      });
      return theUser;
    },

    uniqueUsers()  {      
      this.posts.forEach(post => {
        let user = this.findUser(post.userName);

        if (user === null) {
          this.users.push( {
            'userName': post.userName,
            'posts': [post]
          });
        } 
        else {
          user.posts.push(post);
        }
      });
    }
  }
};
</script>

<style scoped>
.favorite-user {
  margin-top: 0.5rem;
}

.post-author {
  margin-bottom: 0.5rem;
}
</style>
