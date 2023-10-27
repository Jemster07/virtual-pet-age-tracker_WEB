<template>
  <div id="user">
    
    <table class="table is-striped is-hoverable" v-if="petList != ''">
      <thead>
        <tr>
          <th>Name</th>
          <th>Brand</th>
          <th>Type</th>
          <th>Birthday</th>
          <th>Date of Death</th>
          <th>Age</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="pet in petList" :key="pet.petId">
          <td>{{ pet.petName }}</td>
          <td>{{ pet.brand }}</td>
          <td>{{ pet.petType }}</td>
          <td>{{ pet.birthday }}</td>
          <td>{{ pet.dateDeath }}</td>
          <td>{{ pet.age }}</td>
          <td>
            <button>Edit</button>
            <button>R.I.P</button>
            <button>Delete</button>
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