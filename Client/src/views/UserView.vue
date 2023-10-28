<template>
  <div id="user">
    <table class="table is-striped is-hoverable" v-if="petList != ''">
      <thead>
        <tr>
          <th>Name</th>
          <th>Brand/Species</th>
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
            <div class="buttons are-small">
              <button class="button is-info" v-on:click="openModal()">Edit</button>
              <button class="button is-info">R.I.P</button>
              <button class="button is-danger is-light">Delete</button>
            </div>
          </td>
        </tr>
      </tbody>
    </table>

    <div id="edit-modal" class="modal">
      <div class="modal-background"></div>
      <div class="modal-content">
        <div class="box">
          
          <div class="field">
            <label class="label">Label</label>
              <div class="control">
                <input class="input" type="text" placeholder="Text input">
              </div>
              <p class="help">This is a help text</p>
          </div>

          <div class="field">
            <label class="label">Label</label>
              <div class="control">
                <input class="input" type="text" placeholder="Text input">
              </div>
              <p class="help">This is a help text</p>
          </div>

          <div class="field is-grouped is-grouped-right">
            <p class="control">
              <a class="button is-info">Submit</a>
            </p>
            <p class="control">
              <a class="button is-light">Cancel</a>
            </p>
          </div>

        </div>
      </div>
      <button class="modal-close is-large" aria-label="close" v-on:click="closeModal()"></button>
    </div>
  </div>
</template>

<script>
import PetService from "../services/PetService.js";

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
      editPet: {
        petId: 0,
        petName: "",
        petType: "",
        brand: "",
        dateBirth: "",
        timeBirth: null,
        dateDeath: null,
        isHidden: true,
        userId: this.$store.state.user.userId,
      },
    };
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

    openModal() {
      const editModal = document.querySelector('#edit-modal');
      const list = editModal.classList;
      list.add("is-active");
    },
    closeModal() {
      const editModal = document.querySelector('#edit-modal');
      const list = editModal.classList;
      list.remove("is-active");
    },
  },
};
</script>