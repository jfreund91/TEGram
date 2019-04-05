<!-- 
  The New Comment component is used to house the UI and logic to capture a new comment for a post.
  NOTES:
  - The post the comment will be for is passed in as a prop.
  - The component emits a 'newComment' event upon successfully POSTing the comment to a post.
  - All comments require some text, therefore the button is disabled if the textbox is empty.
  DEPENDENCIES:
    None
-->

<template>
  <form class="new-comment" v-on:submit.prevent="newComment">
    <input type="text" name="comment" id="comment" v-model="comment" placeholder="Add a comment...">
    <button>Post</button>
  </form>
</template>

<script>
import auth from "@/shared/auth.js";

export default {
  name: "new-comment",
  props: {
    post: Object
  },
  data() {
    return {
      comment: ""
    };
  },
  methods: {
    newComment() {
      let comment = {
        message: this.comment
      };

      fetch(`${process.env.VUE_APP_REMOTE_API}/posts/${this.post.id}/comments`, {
        method: "POST",
        headers: {
          Authorization: "Bearer " + auth.getToken(),
          "Content-Type": "application/json"
        },
        body: JSON.stringify(comment)
        }).then(response => {
        return response.json()
        }).then((json) => {
          this.$emit('handleNewComment', json);
          this.comment = "";
        }).catch(error => {
          console.error(error)
        });
    }


  }
};
</script>

<style scoped>
</style>
