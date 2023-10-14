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

export default {
  data() {
    return {
      currentUserId: this.$store.state.user.userId,
      petList: [],
      newPet: {
        petName: "",
        petType: "",
        brand: "",
        dateBirth: "",
        timeBirth: "",
        userId: this.$store.state.user.userId,
      },
    }
  },

  async mounted() {
    this.listPets();
  },

  methods: {
    async listPets() {
      let response = await PetService.listPets(this.currentUserId);

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