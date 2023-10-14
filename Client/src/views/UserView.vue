<template>
  <div id="user">
    
    <table>
      <thead>
        <tr>
          <td>

          </td>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td>

          </td>
        </tr>
      </tbody>
    </table>
  
  </div>
</template>

<script>
import PetService from '../services/PetService.js';
import authService from '../services/AuthService.js';

export default {
  data() {
    return {
      currentUser: {},
      username: this.$store.state.user.username,
      petList: [],
      newPet: {
        petName: "",
        petType: "",
        brand: "",
        dateBirth: "",
        timeBirth: "",
        userId: 0,
      },
    }
  },

  async mounted() {
    this.fetchUser();
  },

  methods: {
    async fetchUser() {
      let response = await authService.getUserByUsername(this.username);

			if (response == null) {
				console.log("There was an error fetching the current user.");
			} else {
        this.currentUser = response.data;
        this.listPets();
			}
    },

    async listPets() {
      let response = await PetService.listPets(this.currentUser);

			if (response == null) {
				console.log("There was an error loading your pet list.");
			} else {
				this.petList = response.data;
			}
    },
  },
}
</script>

<style>

</style>