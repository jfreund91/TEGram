<!-- 
  The Post Meta component is used provide additional UI detail and behavior about a post.
  This includes the number of likes, whether is liked or not, and whether it is favored or not
  NOTES:
  - The post that the component is for is passed in as a prop.
  - Upon clicking a heart or a bookmark icon an API call should be made to 
    POST or DELETE a like OR
    POST or DELETE a favorite
    Testing the branch
  - Both heart/bookmark icons are in the component but only one is visible
    at a time
  DEPENDENCIES:
    Font Awesome
-->

<template>
  <div class="post-meta">
    <div class="actions">
      <div class="like">
        <font-awesome-icon
          v-show="post.isLiked"
          v-on:click="changeLike(false)"
          v-bind:icon="['fas', 'heart']"
          size="lg"
          class="liked"
        />
        <font-awesome-icon
          v-show="!post.isLiked"
          v-on:click="changeLike(true)"
          v-bind:icon="['far', 'heart']"
          size="lg"
          class="unliked"
        />
      </div>
      <div class="favor">
        <font-awesome-icon
          v-show="post.isFavored"
          v-on:click="changeFav(false)"
          v-bind:icon="['fas', 'bookmark']"
          size="lg"
          class="favored"
        />
        <font-awesome-icon
          v-show="!post.isFavored"
          v-on:click="changeFav(true)"
          v-bind:icon="['far', 'bookmark']"
          size="lg"
          class="unfavored"
        />
      </div>
    </div>
    <p class="likes">{{ post.numberOfLikes }} likes</p>
  </div>
</template>

<script>
//Uncomment me to make API Calls
import auth from "@/shared/auth.js";

export default {
  name: "post-meta",
  props: {
    post: Object
  },
  methods: {
    changeLike(postIsLiked) {
      this.post.isLiked = !this.post.isLiked;
      this.updatePost('likes',postIsLiked);
    },
    changeFav(postIsFavorite) {
      this.post.isFavored = !this.post.isFavored;
      this.updatePost('favorites',postIsFavorite);
    },
    updatePost(likes,setTrue) {
      fetch(`https://localhost:44326/api/posts/${this.post.id}/${likes}`, {
        method:setTrue?'POST':'DELETE',
        headers: {
          "Content-Type":"application/json",
          // A Header with our authentication token.
          Authorization: "Bearer " + auth.getToken()
          },
          body: JSON.stringify(this.post)
      }).then ((response) => {
        if (likes === "likes"){
          return response.json();
        }
      }) .then((json) => {
          if (json != undefined) {
            this.post.numberOfLikes = json;
          }
      });
    }
  }
};
</script>

<style scoped>
.liked,
.unliked,
.favored,
.unfavored {
  cursor: pointer;
}

.liked {
  color: #ed4956;
}

.actions {
  display: flex;
  flex-direction: row;
  font-size: 16px;
}

.likes {
  margin-left: 0;
  font-size: 1rem;
}

.actions > * {
  flex: 1;
}

.like {
  text-align: left;
}

.favor {
  text-align: right;
}
</style>
